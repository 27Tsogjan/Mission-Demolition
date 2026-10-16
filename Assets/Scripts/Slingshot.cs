using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public AudioSource audioSource;
    public AudioClip launchSound;

    public float velocityMult = 10f;
    public GameObject projLinePrefab;

    [Header("Rubber Band")]
    public LineRenderer rubberBand;
    public Transform leftArm;
    public Transform rightArm;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    void Awake() {
        Transform launchPointTrans = transform.Find("LaunchPoint");

        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

        // Rubber band is relaxed on start
        rubberBand.positionCount = 3;
        UpdateBand(launchPos);
    }

    void UpdateBand(Vector3 middle) {
        rubberBand.SetPosition(0, leftArm.position);
        rubberBand.SetPosition(1, middle);
        rubberBand.SetPosition(2, rightArm.position);
    }

    void Update() {
        if (!aimingMode) {
            return;
        }

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        // Strech rubberband while aiming
        UpdateBand(projPos);

        if (Input.GetMouseButtonUp(0)) {
            aimingMode = false;

            if (audioSource != null && launchSound != null) {
                audioSource.PlayOneShot(launchSound); // Play projectile sound
            }

            projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;

            FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);
            FollowCam.POI = projectile;
            Instantiate<GameObject>(projLinePrefab, projectile.transform);

            MissionDemolition.SHOT_FIRED();
            
            projectile = null;

            // Rubberband snaps back at rest
            UpdateBand(launchPos);
        }
    }

    void OnMouseEnter() {
        launchPoint.SetActive(true);
    }

    void OnMouseExit() {
       launchPoint.SetActive(false); 
    }

    void OnMouseDown() {
        // Game over
        if (GameManager.Instance != null && GameManager.Instance.GameIsOver) {
            return;
        }

        // Out of shots, game over
        if (MissionDemolition.OUT_OF_SHOTS()) {
            return;
        }

        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }
}
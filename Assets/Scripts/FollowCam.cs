using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static private FollowCam S;
    static public GameObject POI;

    public enum eView {none, slingshot, castle, both};

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    public GameObject viewBothGO;

    [Header("Set Dynamically")]
    public float camZ;
    public eView nextView = eView.slingshot;

    void Awake() {
        S = this;
        camZ = this.transform.position.z;
    }

    void FixedUpdate() {
        if (POI == null) {
            return;
        }

        Vector3 destination = POI.transform.position;
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        this.transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }

    public void SwitchView(eView newView) {
        if (newView == eView.none) {
            newView = nextView;
        }
        switch (newView) {
            case eView.slingshot:
                POI = null;
                nextView = eView.castle;
                break;
            case eView.castle:
                POI = MissionDemolition.GET_CASTLE();
                nextView = eView.both;
                break;
            case eView.both:
                POI = viewBothGO;
                nextView = eView.slingshot;
                break;
        }
    }

    public void SwitchView() {
        SwitchView(eView.none);
    }

    static public void SWITCH_VIEW(eView newView) {
        S.SwitchView(newView);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour {
    static private MissionDemolition S;

    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;
    public Vector3 castlePos;
    public GameObject[] castles;
    public int maxShots = 5;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    private bool lossCheckScheduled = false;

    void Start() {
        S = this;

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel() {
        // Prevents old loss timer from firing in new level
        CancelInvoke("CheckLoss");

        if (castle != null) {
            Destroy(castle);
        }

        Projectile.DESTROY_PROJECTILES();

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        // Reset each level
        shotsTaken = 0;
        lossCheckScheduled = false;
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }

    void UpdateGUI() {
        uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
        uitShots.text = "Shots Taken: "+shotsTaken+" of "+maxShots;
    }

    void Update() {
        UpdateGUI();

        // Win level
        if ((mode == GameMode.playing) && Goal.goalMet) {
            mode = GameMode.levelEnd;
            FollowCam.SWITCH_VIEW(FollowCam.eView.both);
            Invoke("NextLevel", 2f);
            return;
        }

        // Lose level
        if (mode == GameMode.playing && shotsTaken >= maxShots && !lossCheckScheduled) {
            lossCheckScheduled = true;
            Invoke("CheckLoss", 4f);
        }
    }

    void CheckLoss() {
        if (mode == GameMode.playing && !Goal.goalMet) {
            GameManager.Instance.GameOver();
        }
    }

    void NextLevel() {
        level++;

        // Player won all rounds
        if (level == levelMax) {
            GameManager.Instance.WinGame();
            return;
        }

        // Load next level
        StartLevel();
    }

    static public void SHOT_FIRED() {
        S.shotsTaken++;
    }

    static public bool OUT_OF_SHOTS() {
        return S.shotsTaken >= S.maxShots;
    }

    static public GameObject GET_CASTLE() {
        return S.castle;
    }
}
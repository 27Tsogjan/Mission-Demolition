// WinScreen.cs 
// Player has successfully completed all levels.
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {
    // Start at level 1
    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }
}
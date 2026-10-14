// WinScreen.cs 
// Player has successfully completed all levels.
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {
    public void GoToMenu() {
        SceneManager.LoadScene(0);
    }

    // Start at level 1
    public void PlayAgain() {
        SceneManager.LoadScene(1);
    }
}
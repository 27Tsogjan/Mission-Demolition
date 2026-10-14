// GameOverScreen.cs 
// Player failed to complete all levels.
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {
    public void GoToMenu() {
        SceneManager.LoadScene(0);
    }

    // Try again at level 1
    public void PlayAgain() {
        SceneManager.LoadScene(1);
    }

}
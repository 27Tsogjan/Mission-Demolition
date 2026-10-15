// GameOverScreen.cs 
// Player failed to complete all levels.
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {
    // Try again at level 1
    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }

}
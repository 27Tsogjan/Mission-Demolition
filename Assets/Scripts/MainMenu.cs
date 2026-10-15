// MainMenu.cs 
// Starting screen for Mission Demolition.
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }

}
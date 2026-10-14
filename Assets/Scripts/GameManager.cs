using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    // Stops game when player loses or completes all levels
    private bool _gameIsOver = false;
    public bool GameIsOver => _gameIsOver;

    private void Awake() {
        Instance = this;
    }

    public void GameOver() {
        if (_gameIsOver) {
            return;
        }
        _gameIsOver = true;
        SceneManager.LoadScene(2);
    }
    public void WinGame() {
        if (_gameIsOver) {
            return;
        }
        _gameIsOver = true;
        SceneManager.LoadScene(3);
    }
}
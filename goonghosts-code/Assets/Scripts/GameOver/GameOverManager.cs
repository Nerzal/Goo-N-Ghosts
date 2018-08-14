using UnityEngine;
using UnityEngine.SceneManagement;
using Variables;

namespace GameOver {
  public class GameOverManager : MonoBehaviour {

    [SerializeField] private IntVariable _currentSceneIndex;
    [SerializeField] private BoolVariable _isNewGame;

    public void Retry() {
      this._isNewGame.SetValue(true);
      SceneManager.LoadSceneAsync(this._currentSceneIndex.Value, LoadSceneMode.Single);
    }

    public void LoadMenu() {
      SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
    }
  }
}

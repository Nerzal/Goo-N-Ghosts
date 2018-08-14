using System.Collections;
using Player.Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;
using Variables;

namespace GameInformation {
  public class GameManager : MonoBehaviour {

    [SerializeField] private BoolVariable _isNewGame;
    [SerializeField] private IntVariable _currentSceneIndex;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private float _loadSceneDelay = .5f;
    [SerializeField] private string[] _allScenesToPlay;

    private void Start() {
      if (this._isNewGame) {
        this._inventory.Initialize();
        this._currentSceneIndex.SetValue(0);
      }
    }

    private void Update() {
      if (Input.GetKey(KeyCode.Escape)) {
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
      }
    }

    public void OnPlayerDied() {
      this._inventory.Clear();
      LoadSceneAfterDelay("GameOverScene");
    }

    public void LevelCompleted() {
      this._currentSceneIndex.SetValue(this._currentSceneIndex.Value + 1);
      LoadSceneAfterDelay(this._allScenesToPlay[this._currentSceneIndex.Value]);
    }

    private void LoadSceneAfterDelay(string sceneName) {
      StartCoroutine(nameof(InvokeDelayed), sceneName);
    }

    private IEnumerator InvokeDelayed(string value) {
      yield return new WaitForSeconds(this._loadSceneDelay);
      SceneManager.LoadSceneAsync(value, LoadSceneMode.Single);
    }
  }
}

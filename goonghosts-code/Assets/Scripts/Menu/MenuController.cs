using UnityEngine;
using UnityEngine.SceneManagement;
using Variables;

namespace Menu {
  public class MenuController : MonoBehaviour {

    [SerializeField] private BoolVariable _isNewGame;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _secondaryCanvas;
    [SerializeField] private FloatVariable _gooSpeedModifier;

    public void StartGame() {
      this._mainCanvas.enabled = false;
      this._secondaryCanvas.enabled = true;


    }

    private void LoadScene() {
      this._isNewGame.SetValue(true);
      SceneManager.LoadSceneAsync("Levels/Level_0", LoadSceneMode.Single);
    }

    public void ShowCredits() {
      SceneManager.LoadSceneAsync("CreditsScene", LoadSceneMode.Single);
    }

    public void ExitGame() {
      Application.Quit();
    }

    public void Easy() {
      this._gooSpeedModifier.Value = 0.5f;
      LoadScene();
    }

    public void Medium() {
      this._gooSpeedModifier.Value = 0.75f;
      LoadScene();
    }

    public void Hard() {
      this._gooSpeedModifier.Value = 1f;
      LoadScene();
    }

    public void Doom() {
      this._gooSpeedModifier.Value = 1.25f;
      LoadScene();
    }
  }
}

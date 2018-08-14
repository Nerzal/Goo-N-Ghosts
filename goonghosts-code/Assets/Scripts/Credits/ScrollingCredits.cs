using UnityEngine;
using UnityEngine.SceneManagement;

namespace Credits {
  public class ScrollingCredits : MonoBehaviour {

    [SerializeField] private Transform _creditsText;
    [SerializeField] private float _scrollSpeed = 1;
    [SerializeField] private float _sceneLoadingThreshold = 900;
    private bool _allreadyLoading = false;

    // Update is called once per frame
    void Update() {
      this._creditsText.Translate(0, this._scrollSpeed, 0);

      if (this._creditsText.transform.position.y > this._sceneLoadingThreshold) {
        LoadMenu();
      }

      if (Input.GetKeyDown(KeyCode.Escape)) {
        LoadMenu();
      }
    }

    private void LoadMenu() {
      if (this._allreadyLoading) {
        return;
      }

      this._allreadyLoading = true;
      SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
  }
}

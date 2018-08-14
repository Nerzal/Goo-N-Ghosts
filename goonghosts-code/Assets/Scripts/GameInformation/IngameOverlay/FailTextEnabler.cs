using UnityEngine;

namespace GameInformation.IngameOverlay {
  public class FailTextEnabler : MonoBehaviour {

    [SerializeField] private GameObject _failText;

    public void ActivateFailText() {
      this._failText.SetActive(true);
    }

    public void DeactivateFailText() {
      this._failText.SetActive(false);
    }
  }
}

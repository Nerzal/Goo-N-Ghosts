using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameInformation.IngameOverlay {
  public class HintImageController : MonoBehaviour {

    [SerializeField] private Sprite[] _hintSprites;
    [SerializeField] private Image _image;

    public void ShowImage(string hintDescription) {
      Sprite sprite = null;
      if (hintDescription.Contains("Yellow")) {
        sprite = this._hintSprites[0];
      } else if (hintDescription.Contains("Red")) {
        sprite = this._hintSprites[2];
      }
    
      StartCoroutine(nameof(SetUnsetHintImage), sprite);
    }

    public void ShowGhostHint() {
      StartCoroutine(nameof(SetUnsetHintImage), this._hintSprites[3]);
    }

    public void ShowGeneratorHint() {
      StartCoroutine(nameof(SetUnsetHintImage), this._hintSprites[1]);
    }

    public IEnumerator SetUnsetHintImage(Sprite sprite) {
      this._image.enabled = true;
      this._image.sprite = sprite;
      yield return new WaitForSeconds(3f);
      this._image.sprite = null;
      this._image.enabled = false;
    }

  }
}

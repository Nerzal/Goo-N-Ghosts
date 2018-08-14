using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace Goo.Scripts {
  [RequireComponent(typeof(RawImage))]
  public class RenderTextureSetter : MonoBehaviour {
    [SerializeField] private TextureVariable _texture;
    [SerializeField] private RawImage _Image;

    // Use this for initialization
    void Start () {
      if (this._texture.Value != null) {
        this._Image.texture = this._texture.Value;
      }

      this._texture.ValueChanged += ValueChanged;
    }

    private void ValueChanged(Texture obj) {
      if (obj != null) {
        this._Image.texture = this._texture.Value;
      }
    }
  }
}

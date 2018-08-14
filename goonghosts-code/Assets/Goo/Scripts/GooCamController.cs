using UnityEngine;
using Variables;

namespace Goo.Scripts {
  [RequireComponent(typeof(Camera))]
  public class GooCamController : MonoBehaviour {
    [SerializeField] private float DistanceToGoo = 13.78f;
    [SerializeField] private TextureVariable _texture;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2Variable _currentGooPosition;

    private void Start() {
      this._texture.ValueChanged += ValueChanged;
      SetTargetTexture(this._texture.Value);
    }

    private void Update() {
      float newYPosition = this._currentGooPosition.Value.y + this.DistanceToGoo;
      this.transform.position = new Vector3(this.transform.position.x, newYPosition, this.transform.position.z);
    }

    private void SetTargetTexture(Texture texture) {
      if (this._texture.Value != null) {
        this._camera.targetTexture = (RenderTexture)texture;
      }
    }

    private void ValueChanged(Texture obj) {
      SetTargetTexture(obj);
    }

    public void GooClimbing() {
      this.transform.Translate(0, 1, 0);
    }

    public void GooFalling() {
      this.transform.Translate(0, -1, 0);
    }
  }
}

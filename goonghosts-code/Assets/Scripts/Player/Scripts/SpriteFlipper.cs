using UnityEngine;
using Variables;

namespace Player.Scripts {
  [RequireComponent(typeof(SpriteRenderer))]
  public class SpriteFlipper : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Vector2Variable _movementSpeed;
    [SerializeField] private bool _flipX;
    [SerializeField] private bool _flipY;

    // Use this for initialization
    void Start () {
      this._movementSpeed.ValueChanged += ValueChanged;
      this._movementSpeed.XChanged += XChanged;
      this._spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void XChanged(float obj) {
      if (this._spriteRenderer == null) {
        return;
      }

      FlipX(obj);
    }

    private void ValueChanged(Vector2 obj) {
      if (this._spriteRenderer == null) {
        return;
      }

      FlipX(obj.x);
      FlipY(obj.x);
    }

    private void FlipY(float obj) {
      if (!this._flipY) {
        return;
      }

      if (obj > 0) {
        this._spriteRenderer.flipY = false;
      } else if (obj < 0) {
        this._spriteRenderer.flipY = true;
      }
    }

    private void FlipX(float obj) {
      if (!this._flipX) {
        return;
      }
    
      if (obj > 0) {
        this._spriteRenderer.flipX = false;
      } else if (obj < 0) {
        this._spriteRenderer.flipX = true;
      }
    }
  }
}

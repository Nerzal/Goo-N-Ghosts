using UnityEngine;

namespace Player.Inventory {
  [RequireComponent(typeof(SpriteRenderer))]
  public class Pickupable : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;

    public Item Item;

    private void Start() {
      this._spriteRenderer = GetComponent<SpriteRenderer>();
      this._spriteRenderer.sprite = this.Item.Sprite;
    }
  }
}
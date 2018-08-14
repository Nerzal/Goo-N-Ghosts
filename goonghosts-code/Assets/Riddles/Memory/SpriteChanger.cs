using Events;
using Riddles.Memory.Scripts;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Riddles.Memory {
  public class SpriteChanger : MonoBehaviour {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteChangerGameEvent _spriteChangerClicked;
    [SerializeField] private GameEvent _cardHovered;
    [SerializeField] private Sprite _cover;
    [SerializeField] private GameEvent _onClickEvent;

    public Card Card;

    public bool Flipped { get; private set; }

    public void Flip() {
      this._onClickEvent.Raise();
      if (!this.Flipped) {
        this._spriteRenderer.sprite = this.Card.Sprite;
        this.Flipped = true;
      } else {
        this._spriteRenderer.sprite = this._cover;
        this.Flipped = false;
      }
    }

    private void OnMouseEnter() {
      this._cardHovered.Raise();
    }

    private void OnMouseOver() {
      if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse)) {
        this._spriteChangerClicked?.Raise(this);
      }
    }
  }
}

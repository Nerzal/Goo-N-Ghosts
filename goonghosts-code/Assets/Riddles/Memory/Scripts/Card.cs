using UnityEngine;

namespace Riddles.Memory.Scripts {
  [CreateAssetMenu()]
  public class Card : ScriptableObject {
    public CardTypes CardType;

    public Sprite Sprite;

    public Card(CardTypes cardType) {
      this.CardType = cardType;
    }

    public override string ToString() {
      return string.Format(this.CardType.ToString());
    }
  }
}
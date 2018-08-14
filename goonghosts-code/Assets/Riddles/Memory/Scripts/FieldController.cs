using System.Collections.Generic;
using Events;
using UnityEngine;
using Random = System.Random;

namespace Riddles.Memory.Scripts {
  public class FieldController : MonoBehaviour {
    public GameObject Original;
    public Transform SpawnLocationCenter;
    public int Height;
    public int Width;
    public float XMargin;
    public float YMargin;
    private List<SpriteChanger> _spawnedCards;

    [SerializeField] private Card[] _cardItems;
    [SerializeField] private GameEvent _riddleSolved;
    [SerializeField] private Camera _camera;
    public SpriteChanger FirstCardClicked { get; set; }
    public SpriteChanger SecondCardClicked { get; set; }

    void Start() {
      this._camera.enabled = true;
      Renderer mesh = this.Original.GetComponentInChildren<Renderer>();
      this._spawnedCards = new List<SpriteChanger>(16);

      float itemWidth = mesh.bounds.size.x;
      float width = this.Width * itemWidth + this.YMargin * this.Width;
      float xOffset = width / 2 - 0.5f * itemWidth;

      float itemHeight = mesh.bounds.size.z;
      float height = this.Height * itemHeight + this.XMargin * this.Height;
      float yOffset = height / 2 - 0.5f * itemHeight;

      List<Card> availableCards = new List<Card>(this._cardItems.Length * 2);
      availableCards.AddRange(this._cardItems);
      availableCards.AddRange(this._cardItems);
      
      SpawnCards(yOffset, itemHeight, xOffset, itemWidth, availableCards);
    }

    private void SpawnCards(float yOffset, float itemHeight, float xOffset, float itemWidth, List<Card> availableCards) {
      System.Random random = new System.Random();
      for (int y = 0; y < this.Height; y++) {
        float yMargin = 0.5f * this.YMargin * y;
        float yPosition = yOffset - yMargin - itemHeight * y - itemHeight / 2 - this.YMargin / 2;
        for (int x = 0; x < this.Width; x++) {
          float xPosition = xOffset - itemWidth * x - 0.5f * this.XMargin * x - itemWidth / 2 - this.XMargin / 2;

          GameObject instantiate = Instantiate(this.Original, new Vector3(xPosition, yPosition, 0),
            Quaternion.Euler(Vector3.zero), this.SpawnLocationCenter.transform);
          instantiate.layer = 11;
          SpriteChanger spriteChanger = instantiate.GetComponentInChildren<SpriteChanger>();
          this._spawnedCards.Add(spriteChanger);
          int index = random.Next(0, availableCards.Count);
          Card card = availableCards[index];
          spriteChanger.Card = card;
          availableCards.RemoveAt(index);
        }
      }
    }
    
    public void CardClicked(SpriteChanger card) {
      if (card.Flipped) {
        return;
      }

      if (this.SecondCardClicked != null) {
        this.FirstCardClicked.Flip();
        this.SecondCardClicked.Flip();
        this.FirstCardClicked = null;
        this.SecondCardClicked = null;
      }

      card.Flip();

      if (this.FirstCardClicked == null) {
        this.FirstCardClicked = card;
        return;
      }

      this.SecondCardClicked = card;

      if (this.FirstCardClicked.Card.CardType != card.Card.CardType) {
        return;
      }


      this.FirstCardClicked.transform.Rotate(Vector3.forward * 90);
      card.transform.Rotate(Vector3.forward * 90);
      this.FirstCardClicked = null;
      this.SecondCardClicked = null;

      CheckWin();
    }

    private void CheckWin() {
      foreach (SpriteChanger cardItem in this._spawnedCards) {
        if (!cardItem.Flipped) {
          return;
        }
      }
      
      this._riddleSolved.Raise();
      this._camera.enabled = false;
    }
  }
}
using System.Linq;
using Events;
using Sets;
using UnityEngine;
using Variables;

namespace Goo.Scripts {
  public class PermanentColliderController : MonoBehaviour {
    [SerializeField] private GameObject _colliderHolderPrefab;
    [SerializeField] private ColliderHolder _permanentColliderHolder;
    [SerializeField] private GameEvent _deleteTemporaryColliders;
    [SerializeField] private Vector2Variable _currentGooSpeed;
    [SerializeField] private TemporaryColliderHolder _tempColliderHolder;

    private void Start() {
      InitialzeColliderHolder();
    }

    private void InitialzeColliderHolder() {
      this._permanentColliderHolder.Initialize();
    }

    public void HandleWallCollission() {
      this._currentGooSpeed.Value.x *= -1;
      HandleDirectionChange();
    }

    public void HandleDirectionChange() {
      if (!this._tempColliderHolder.Any()) {
        return;
      }

      Vector2 firstCollider = this._tempColliderHolder[0].transform.position;
      Vector2 lastCollider = this._tempColliderHolder.Last().transform.position;
      Vector2 newColliderPosition = firstCollider + (lastCollider - firstCollider) / 2;

      bool horizontal = Mathf.Abs(firstCollider.y - lastCollider.y) <= 1.5;
      if (horizontal) {
        float colliderWidth = firstCollider.x - lastCollider.x;
        if (colliderWidth < 0) {
          colliderWidth *= -1;
        }
        colliderWidth += 1f; //cause we lose that, caused by the pivot
        if (colliderWidth >= .3f) {
          CreatePermanentCollider(colliderWidth, 1f, newColliderPosition);
        }
      }

      this._deleteTemporaryColliders.Raise();
    }

    private void CreatePermanentCollider(float colliderWidth, float colliderHeight, Vector2 position) {
      GameObject newHolder = Instantiate(this._colliderHolderPrefab, position, Quaternion.identity);
      BoxCollider2D newCollider = newHolder.AddComponent<BoxCollider2D>();
      newCollider.size = new Vector2(colliderWidth, colliderHeight);
      newCollider.offset = new Vector2(0f, 0f);
      this._permanentColliderHolder.Add(newCollider);

      if (this._permanentColliderHolder.Count() > 50) {
        BoxCollider2D firstCollider = this._permanentColliderHolder.First();
        this._permanentColliderHolder.Remove(firstCollider);
        Destroy(firstCollider.gameObject);
      }
    }
  }
}
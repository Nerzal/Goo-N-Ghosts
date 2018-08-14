using Sets;
using UnityEngine;
using Variables;

namespace Goo.Scripts {
  public class TemporaryColliderSpawner : MonoBehaviour {
    [SerializeField] private Vector2Variable _currentGooSpeed;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _leftSpawner;
    [SerializeField] private Transform _rightSpawner;
    [SerializeField] private TemporaryColliderHolder _tempColliderHolder;

    private void Start() {
      this._tempColliderHolder.Initialize();
    }

    public void CreateTemporaryBoxCollider() {
      Vector3 spawnPosition = this._currentGooSpeed.Value.x > 0
        ? this._leftSpawner.position
        : this._rightSpawner.position;

      GameObject newObject = Instantiate(this._prefab, spawnPosition, Quaternion.identity);
      this._tempColliderHolder.Add(newObject);
    }

    public void DeleteAllColliders() {
      this._tempColliderHolder.Clear();
    }
  }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostEnabler : MonoBehaviour {
  private bool _active;
  private Transform _playerTransform;
  [SerializeField]
  private GameObject _playerObject;


  [SerializeField] private GameObject _ghostPrefab;
  [SerializeField] private TextMeshProUGUI _text;
  [SerializeField] private List<string> _sentences;
  [SerializeField] private GameObject _itemToSpawn;
  private bool _movetowards;


  public void GhostTriggerTriggered() {
    if (this._active) {
      return;
    }


    this._active = true;
  }

  private void Start() {
    this._active = false;
    this._playerTransform = this._playerObject.gameObject.transform;
  }

  private void Update() {
    if (!_movetowards) {
      return;
    }
    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(this._playerTransform.position.x, this._playerTransform.position.y), 3 * Time.deltaTime);
  }
}

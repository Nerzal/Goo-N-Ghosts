
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;

public class SpookyDoorController : MonoBehaviour {

  [SerializeField] private Sprite _closedDoor;
  [SerializeField] private SpriteRenderer _renderer;
  [SerializeField] private GameObject _ghostPrefab;
  [SerializeField] private TextMeshProUGUI _text;
  [SerializeField] private List<string> _sentences;
  [SerializeField] private GameObject _itemToSpawn;
  [SerializeField] private GameEvent _showGhostHint;
  [SerializeField] private bool _randomizeKeydrop;
  [SerializeField] private bool _followPlayer;
  [Range(0.0f, 100.0f)]
  [SerializeField] private float _keydropChance;
  [SerializeField] private GameObject _followTarget;

  private Transform _followTargetTransform;
  private bool _hasPlayed = false;
  private bool _followActive;
  private GameObject _holder;
  private Vector2 _keySpawnPosition;

  private void OnTriggerEnter2D(Collider2D collision) {
    if (this._hasPlayed) {
      return;
    }

    if (!collision.transform.CompareTag("Player")) {
      return;
    }

    this._hasPlayed = true;
    StartCoroutine(nameof(Spook));
  }

  private void Start() {
    if (this._followTarget != null) {
      _followTargetTransform = this._followTarget.transform;
    }
  }

  private void Update() {
    if (!this._followPlayer || !this._followActive) {
      return;
    }

    this._keySpawnPosition = Vector2.MoveTowards(new Vector2(_holder.transform.position.x, _holder.transform.position.y), new Vector2(_followTargetTransform.position.x, _followTargetTransform.position.y + 2), Time.deltaTime);
    _holder.transform.position = this._keySpawnPosition;
  }

  public IEnumerator Spook() {
    this._text.color = Color.red;
    _holder = Instantiate(this._ghostPrefab, this.transform.position, Quaternion.identity);
    _keySpawnPosition = this._holder.transform.position;
    Animator animator = _holder.GetComponentInChildren<Animator>();
    yield return new WaitForSeconds(2f);
    this._followActive = true;
    foreach (string sentence in this._sentences) {
      this._text.text = sentence;
      yield return new WaitForSeconds(2.5f);
    }

    animator.SetTrigger("IsAngry");
    yield return new WaitForSeconds(1.4f);
    this._renderer.sprite = this._closedDoor;
    animator.SetTrigger("Disappear");
    this._followActive = false;
    Destroy(_holder.gameObject, 1.6f);
    yield return new WaitForSeconds(2f);
    this._text.text = "";
    yield return new WaitForSeconds(1f);

    if (!this._randomizeKeydrop) {
      SpawnKey();
    } else {
      if (Random.Range(0, 100) > 100 - this._keydropChance) {
        SpawnKey();
      }
    }

    if (this._showGhostHint == null) {
      yield break;
    }
    this._showGhostHint.Raise();
  }

  private void SpawnKey() {
    Instantiate(this._itemToSpawn, _keySpawnPosition, Quaternion.identity);
  }
}
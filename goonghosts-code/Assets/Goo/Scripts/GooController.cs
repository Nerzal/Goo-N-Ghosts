using System.Collections;
using Events;
using UnityEngine;
using Variables;

namespace Goo.Scripts {
  public class GooController : MonoBehaviour {

    private TrailRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private bool _stop = false;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private FloatVariable _speedModifier;
    [SerializeField] private Vector2Variable _currentGooSpeed;
    [SerializeField] private Vector2Variable _currentPosition;

    [SerializeField] private GameEvent _gooCollidedWithWall;
    [SerializeField] private GameEvent _gooClimbing;
    [SerializeField] private GameEvent _gooFalling;
    [SerializeField] private GameEvent _gooReachWaypoint;
    private float _deltaSinceLastGooSound;

    // Use this for initialization
    private void Start() {
      InitializeRenderer();
      this._speed *= this._speedModifier.Value;
      this._currentGooSpeed.SetValue(new Vector2(this._speed, 0));
      this._rigidbody = GetComponent<Rigidbody2D>();

      StartCoroutine(nameof(AddPositions));
      this._rigidbody.velocity = this._currentGooSpeed.Value;
    }

    private void Update() {
      this._currentPosition.SetValue(this.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
      if (this._stop) {
        return;
      }

      if (collision.transform.CompareTag("Wall")) {
        HandleWallCollision();
      }

      if (collision.transform.CompareTag("Floor")) {
        this._rigidbody.velocity = new Vector2(this._speed, 0);
      }

      this._currentGooSpeed.SetValue(this._rigidbody.velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (this._stop) {
        return;
      }

      Transform collisionTransform = collision.transform;
      bool fallDown = collisionTransform.CompareTag("GooFallDown");
      if (fallDown) {
        HandleGooFallDown();
      }

      bool goRight = collisionTransform.CompareTag("GooGoRight");
      if (goRight) {
        HandleGooGoRight();
      }

      bool goLeft = collisionTransform.CompareTag("GoGoLeft");
      if (goLeft) {
        HandleGooGoLeft();
      }

      bool goUp = collisionTransform.CompareTag("GooGoUp");
      if (goUp) {
        HandleGooGoUp();
      }

      bool goStop = collisionTransform.CompareTag("GooStopper");
      if (goStop) {
        HandleGooStopper();
      }

      if (fallDown || goRight || goLeft || goUp || goStop) {
        collision.enabled = false;
        this._gooReachWaypoint.Raise();
      }

      this._currentGooSpeed.SetValue(this._rigidbody.velocity);
    }

    private void HandleGooStopper() {
      this._rigidbody.velocity = Vector2.zero;
      StopAllCoroutines();
      this._stop = true;
    }

    private void HandleGooGoUp() {
      float translation = GetTranslationX();
      this.transform.Translate(new Vector2(translation, 0));
      this._rigidbody.velocity = new Vector2(0, this._speed);
    }

    private void HandleGooGoLeft() {
      float translation = GetTranslationY();
      this.transform.Translate(new Vector2(0, translation));
      this._rigidbody.velocity = new Vector2(-this._speed, 0);
    }

    private void HandleGooGoRight() {
      float translation = GetTranslationY();
      this.transform.Translate(new Vector2(0, translation));
      this._rigidbody.velocity = new Vector2(this._speed, 0);
    }

    private void HandleGooFallDown() {
      this._gooFalling.Raise();
      float translation = GetTranslationX();

      this.transform.Translate(new Vector2(translation, 0));
      this._rigidbody.velocity = new Vector2(0, -this._speed);
    }

    private float GetTranslationY() {
      float translation = 0;
      if (this._currentGooSpeed.Value.y > 0) {
        translation = .5f;
      } else if (this._currentGooSpeed.Value.y < 0) {
        translation = -.5f;
      }

      return translation;
    }

    private float GetTranslationX() {
      float translation = 0;
      if (this._currentGooSpeed.Value.x > 0) {
        translation = .5f;
      } else if (this._currentGooSpeed.Value.x < 0) {
        translation = -.5f;
      }

      return translation;
    }

    private void HandleWallCollision() {
      this.transform.Translate(0, 1, 0);
      PlayGooSound();
      this._rigidbody.velocity = -this._currentGooSpeed.Value;
      this._gooCollidedWithWall.Raise();
      this._gooClimbing.Raise();
    }

    private void PlayGooSound() {
      this._deltaSinceLastGooSound += Time.deltaTime;
      if (this._deltaSinceLastGooSound > Random.Range(5f, 10f)) {
        this._gooCollidedWithWall.Raise();
      }
    }

    private IEnumerator AddPositions() {
      while (true) {
        this._renderer.AddPosition(this.transform.position);
        yield return new WaitForSeconds(1f);
      }
    }

    private void InitializeRenderer() {
      this._renderer = GetComponent<TrailRenderer>();
      this._renderer.numCapVertices = 90;
      this._renderer.numCornerVertices = 90;
    }
  }
}
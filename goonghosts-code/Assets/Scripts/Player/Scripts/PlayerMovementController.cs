using System;
using System.Collections;
using Events;
using UnityEngine;
using Variables;

namespace Player.Scripts {
  [RequireComponent(typeof(Rigidbody2D))]
  [RequireComponent(typeof(BoxCollider2D))]
  public class PlayerMovementController : MonoBehaviour {
    private const string Platform = "Platform";
    private const string Floor = "Floor";

    private Rigidbody2D _rigidBody;

    private bool _isGrounded;
    private bool _doubleJumpAvailable;

    [SerializeField] private Vector2Variable _currentSpeed;
    [SerializeField] private Vector2Variable _currentPosition;
    [SerializeField] private GameEvent _playerJumped;

    [SerializeField] private FloatReference _playerSpeed;
    [SerializeField] private FloatReference _jumpPower;
    [SerializeField] private FloatVariable _playerMaxSpeed;

    // Use this for initialization
    private void Start() {
      this._rigidBody = GetComponent<Rigidbody2D>();
      this._doubleJumpAvailable = true;
    }

    // Update is called once per frame
    private void FixedUpdate() {
      GroundCheck();
      Move();
      Jump();
      SetVariables();
    }

    private void GroundCheck() {
      if (Math.Abs(this._rigidBody.velocity.y) > 0.01f) {
        this._isGrounded = false;
      } else {
        this._isGrounded = true;
        this._doubleJumpAvailable = true;
      }
    }

    private void SetVariables() {
      RestrictToMaxSpeed();
      this._currentSpeed.SetValue(this._rigidBody.velocity);
      this._currentPosition.SetValue(this.transform.position);
    }

    private void RestrictToMaxSpeed() {
      if (this._rigidBody.velocity.x > this._playerMaxSpeed.Value) {
        this._rigidBody.velocity = new Vector2(this._playerMaxSpeed.Value, this._rigidBody.velocity.y);
      } else if (this._rigidBody.velocity.x < -this._playerMaxSpeed.Value) {
        this._rigidBody.velocity = new Vector2(-this._playerMaxSpeed.Value, this._rigidBody.velocity.y);
      }
    }

    private void Move() {
      float horizontal = Input.GetAxis("Horizontal");
      if (Math.Abs(horizontal) > 0.01f) {
        this._rigidBody.AddForce(new Vector2(horizontal * this._playerSpeed, 0), ForceMode2D.Impulse);
      } else {
        this._rigidBody.velocity = new Vector2(0, this._rigidBody.velocity.y);
      }
    }

    private void Jump() {
      bool jump = Input.GetKeyDown(KeyCode.Space);
      if (!jump) {
        return;
      }

      if (!this._isGrounded) {
        if (!this._doubleJumpAvailable) {
          return;
        }

        PerformJump(this._jumpPower * 0.75f);
        this._doubleJumpAvailable = false;
        return;
      }

      PerformJump(this._jumpPower);
    }

    private void PerformJump(float floatReference) {
      this._rigidBody.velocity = new Vector2(this._rigidBody.velocity.x, 0);
      this._rigidBody.AddRelativeForce(new Vector2(0, floatReference), ForceMode2D.Impulse);
      this._isGrounded = false;
      this._playerJumped.Raise();
    }

    private void OnCollisionStay2D(Collision2D collision) {
      if (!CheckCollisionTag(collision, Platform)) {
        return;
      }

      if (Input.GetAxis("Vertical") < 0f) {
        StartCoroutine(nameof(EnablePlatform), collision.collider);
        collision.collider.enabled = false;
      }
    }

    private IEnumerator EnablePlatform(Collider2D collision) {
      yield return new WaitForSeconds(0.5f);
      collision.enabled = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
      bool wasOnPlatform = CheckCollisionTag(collision, Platform);
      if (!wasOnPlatform && !CheckCollisionTag(collision, Floor)) {
        return;
      }
    }

    private bool CheckCollisionTag(Collision2D collision, string tag) {
      return collision.transform.CompareTag(tag);
    }
  }
}
using System;
using UnityEngine;
using Variables;

namespace Audio.Scripts {
  public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioSource _playerSounds;
    [SerializeField] private AudioSource _playerFootStepsSounds;
    [SerializeField] private AudioSource _otherSounds;

    [SerializeField] private Vector2Variable _playerCurrentSpeed;

    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _doorTriggeredSound;
    [SerializeField] private AudioClip[] _footSteps;
    [SerializeField] private float _footStepInterval = 0.5f;
    private int _footStepSoundIndex = 0;
    private float _timeSinceLastFootStep = 0;

    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _buttonHover;
    [SerializeField] private AudioClip _successJingle;
    [SerializeField] private AudioClip _failJingle;
    [SerializeField] private AudioClip _cardFlip;

    // Use this for initialization
    void Start () {
      DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
      PlayFootSteps();
    }

    public void PlayButtonClickSound() {
      this._otherSounds.PlayOneShot(this._buttonClick);
    }

    public void PlayButtonHoverSound() {
      this._otherSounds.PlayOneShot(this._buttonHover);
    }

    public void PlaySuccessSound() {
      this._otherSounds.PlayOneShot(this._successJingle);
    }

    public void PlayFailSound() {
      this._otherSounds.PlayOneShot(this._failJingle);
    }

    public void PlayCardFlipSound() {
      this._otherSounds.PlayOneShot(this._cardFlip);
    }

    private void PlayFootSteps() {
      if (this._timeSinceLastFootStep < this._footStepInterval) {
        this._timeSinceLastFootStep += Time.deltaTime;
        return;
      }

      this._timeSinceLastFootStep = 0;

      if (Math.Abs(this._playerCurrentSpeed.Value.x) > 0.01f && Math.Abs(this._playerCurrentSpeed.Value.y) < 0.01f) {
        this._playerFootStepsSounds.PlayOneShot(this._footSteps[this._footStepSoundIndex++]);
      }

      if (this._footStepSoundIndex >= this._footSteps.Length) {
        this._footStepSoundIndex = 0;
      }
    }

    public void PlayJumpSound() {
      this._playerSounds.PlayOneShot(this._jumpSound);
    }

    public void PlayGameOverSound() {
      this._playerSounds.PlayOneShot(this._gameOverSound);
    }

    public void PlayDoorTriggeredSound() {
      this._playerSounds.PlayOneShot(this._doorTriggeredSound);
    }
  
  }
}

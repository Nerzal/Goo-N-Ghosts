using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(AudioListener))]
public class CameraManager : MonoBehaviour {
  private Camera _camera;
  private AudioListener _audioListener;

  private void Start() {
    this._camera = GetComponent<Camera>();
    this._audioListener = GetComponent<AudioListener>();
  }
  
  public void OnRiddleStarted() {
    this._camera.enabled = false;
    this._audioListener.enabled = false;
  }

  public void OnRiddleFinished() {
    this._camera.enabled = true;
    this._audioListener.enabled = true;
  }
}

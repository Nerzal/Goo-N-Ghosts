using UnityEngine;

namespace GameInformation.IngameOverlay {
  [RequireComponent(typeof(Canvas))]
  public class UiCameraSetter : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
      Canvas canvas = GetComponent<Canvas>();
      canvas.worldCamera = Camera.main;
    }
  }
}

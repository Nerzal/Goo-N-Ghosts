using System.Linq;
using Events;
using Sets.PleaseKillMe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
  private const string RiddleSceneBasePath = "Riddles/Scenes/";

  private DoorTrigger _currentDoor;
  private bool _riddleCurrentlyRunning;

  [SerializeField] private RuntimeSetOfStrings _allSceneNames;
  [SerializeField] private GameEvent _riddleStarted;
  [SerializeField] private Camera _gooCam;

  private string _lastLoadedRiddleName;

  private void Start() {
    this._riddleCurrentlyRunning = false;
  }
  
  public void OnRiddleRequest(DoorTrigger doorTrigger) {
    if (this._riddleCurrentlyRunning) {
      return;
    }

    this._currentDoor = doorTrigger;
    StartRiddle();
    this._gooCam.enabled = true;
  }

  public void OnPuzzleFinished() {
    this._gooCam.enabled = false;
    this._currentDoor.OpenDoor();
    this._riddleCurrentlyRunning = false;
    SceneManager.UnloadSceneAsync(this._lastLoadedRiddleName);
  }

  public void StartRiddle() {
    if (this._riddleCurrentlyRunning) {
      return;
    }

    this._riddleStarted.Raise();
    string sceneName = this._allSceneNames[UnityEngine.Random.Range(0, this._allSceneNames.Count())];
    this._lastLoadedRiddleName = sceneName;
    this._riddleCurrentlyRunning = true;
    SceneManager.LoadSceneAsync(RiddleSceneBasePath + sceneName, LoadSceneMode.Additive);
  }
}
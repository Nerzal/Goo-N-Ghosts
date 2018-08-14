using UnityEngine;

namespace Audio.Scripts {
  public class MusicManager : MonoBehaviour {

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _songs;

    private int _currentSongId = 0;

    // Use this for initialization
    void Start() {
      this._source.clip = this._songs[0];
      this._source.Play();
      DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update() {
      if (this._source.isPlaying) {
        return;
      }

      this._currentSongId = GetNextSongId();
      this._source.clip = this._songs[this._currentSongId];
    }

    private int GetNextSongId() {
      int nextSongId = this._currentSongId;
      while (nextSongId == this._currentSongId) {
        nextSongId = Random.Range(0, this._songs.Length);
      }

      return nextSongId;
    }
  }
}
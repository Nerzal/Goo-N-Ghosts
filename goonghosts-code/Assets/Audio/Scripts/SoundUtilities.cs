using UnityEngine;

namespace Audio.Scripts {
  public class SoundUtilities {
    /// <summary>
    /// Author ProjectMakers
    /// Spielt einen Sound mit rnd Pitch/Vol als Shot ab..
    /// <para>|source| => Audio Quelle</para>
    /// <para>|clip| => Audio Clip</para>
    /// <para>|minPitch| => Minimaler Pitch</para>
    /// <para>|maxPitch| => Maximaler Pitch</para>
    /// <para>|minVol| => Minimale Lautstärke</para>
    /// <para>|maxVol| => Maximale Lautstärke</para>
    /// </summary>
    public static void SoundIndi(AudioSource source, AudioClip clip, float minPitch, float maxPitch, float minVol, float maxVol) {
      float rndPitch = Random.Range(minPitch, maxPitch);
      float rndVol = Random.Range(minVol, maxVol);

      source.volume = rndVol;
      source.pitch = rndPitch;

      source.PlayOneShot(clip);
    }
  }
}

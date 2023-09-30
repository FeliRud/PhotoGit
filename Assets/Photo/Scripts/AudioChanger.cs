using UnityEngine;
using UnityEngine.Audio;

namespace Photo
{
  public class AudioChanger : MonoBehaviour
  {
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioMixer _masterMixer;

    public void SetMusic(AudioClip audio)
    {
      if (_audioSource.clip == audio)
        return;

      _audioSource.clip = audio;
      _audioSource.Play();
    }

    public void ChangeMusicValue(float value)
    {
      float volume = Mathf.Lerp(-80, 0, value);
      _masterMixer.SetFloat("Music", volume);
    }
  }
}
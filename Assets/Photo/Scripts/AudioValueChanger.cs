using UnityEngine;
using UnityEngine.Audio;

namespace Photo
{
    public class AudioValueChanger : MonoBehaviour
    {
        [SerializeField] private AudioMixer _masterMixer;

        public void ChangeMusicValue(float value)
        {
            float volume = Mathf.Lerp(-80, 0, value);
            _masterMixer.SetFloat("Music", volume);
        }
    }
}
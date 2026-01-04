using UnityEngine;
using UnityEngine.UI;

public class NPCVoiceController : MonoBehaviour
{
    public static NPCVoiceController Instance;
    private AudioSource voiceAudioSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            voiceAudioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayVoice(AudioClip audioClip, float pitch = 1f)
    {
        if (voiceAudioSource != null)
        {
            voiceAudioSource.pitch = pitch;
            voiceAudioSource.PlayOneShot(audioClip);
        }
    }
    
    public void SetVoiceVolume(float volume)
    {
        if (voiceAudioSource != null)
        {
            voiceAudioSource.volume = volume;
        }
    }
}
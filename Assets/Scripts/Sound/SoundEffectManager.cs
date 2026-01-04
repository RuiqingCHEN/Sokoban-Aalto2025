using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager Instance;
    private static AudioSource audioSource;
    // private static AudioSource randomPitchAudioSource;
    // private static AudioSource voiceAudioSource;
    private static SoundEffectLibrary soundEffectLibrary;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Button toggleButton;
    [SerializeField] private Sprite musicOnIcon;
    [SerializeField] private Sprite musicOffIcon;
    [SerializeField] private AudioSource backgroundMusicSource;
    private bool isMusicOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSource = audioSources[0];
            // randomPitchAudioSource = audioSources[1];
            // voiceAudioSource = audioSources[1];
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            // DontDestroyOnLoad(gameObject);用的时候需要把soundeffectmanager移到底层
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName, bool randomPitch = false)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);
        if (audioClip != null)
        {
            // if (randomPitch)
            // {
            //     randomPitchAudioSource.pitch = Random.Range(1f, 1.5f);
            //     randomPitchAudioSource.PlayOneShot(audioClip);
            // }
            // else
            // {
                audioSource.PlayOneShot(audioClip);
            // }
        }
    }

    // public static void PlayVoice(AudioClip audioClip, float pitch = 1f)
    // {
    //     voiceAudioSource.pitch = pitch;
    //     voiceAudioSource.PlayOneShot(audioClip);
    // }

    void Start()
    {
        isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        AudioListener.volume = isMusicOn ? 1.0f : 0.0f;
        toggleButton.GetComponent<Image>().sprite = isMusicOn ? musicOnIcon : musicOffIcon;
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });

        //按钮
        toggleButton.onClick.AddListener(ToggleMusic);
    }
    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
        // randomPitchAudioSource.volume = volume;
        // voiceAudioSource.volume = volume;
        if (NPCVoiceController.Instance != null)
        {
            NPCVoiceController.Instance.SetVoiceVolume(volume);
        }
        if (Instance.backgroundMusicSource != null)
        {
            Instance.backgroundMusicSource.volume = volume;
        }
    }
    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }


    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        AudioListener.volume = isMusicOn ? 1.0f : 0.0f;
        toggleButton.GetComponent<Image>().sprite = isMusicOn ? musicOnIcon : musicOffIcon;

        PlayerPrefs.SetInt("MusicEnabled", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}

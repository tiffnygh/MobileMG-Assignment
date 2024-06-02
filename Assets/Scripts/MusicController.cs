using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;

    private void Start()
    {
        // Initialize sliders with current volume levels
        musicSlider.value = SoundManager.Instance.musicAudioSource.volume;
        soundSlider.value = AudioListener.volume;

        // Add listeners to handle slider value changes
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);

        // Load settings
        SoundManager.Instance.LoadSettings();
    }

    private void SetMusicVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
    }

    private void SetSoundVolume(float volume)
    {
        SoundManager.Instance.SetSoundVolume(volume);
    }

    public void OpenSettings()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void SaveSettings()
    {
        SoundManager.Instance.SaveSettings();
    }

    public void BackToMainMenu()
    {
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}

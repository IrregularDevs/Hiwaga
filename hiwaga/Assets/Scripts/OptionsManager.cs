using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private GameObject btnPause;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private Slider masterVol, musicVol, sfxVol;
    [SerializeField] private AudioMixer mainAudioMixer;

    private void Start()
    {
        mainAudioMixer.SetFloat("Vol_Master", Mathf.Clamp(Mathf.Log10(PlayerPrefs.GetFloat("Vol_Master", 1.0f)) * 20.0f, -80.0f, 20.0f));
        mainAudioMixer.SetFloat("Vol_Music", Mathf.Clamp(Mathf.Log10(PlayerPrefs.GetFloat("Vol_Music", 1.0f)) * 20.0f, -80.0f, 20.0f));
        mainAudioMixer.SetFloat("Vol_SFX", Mathf.Clamp(Mathf.Log10(PlayerPrefs.GetFloat("Vol_SFX", 1.0f)) * 20.0f, -80.0f, 20.0f));
    }

    public void ShowSettings()
    {
        panelSettings.SetActive(true);
        btnPause.SetActive(false);
    }

    public void HideSettings()
    {
        panelSettings.SetActive(false);
        btnPause.SetActive(true);
    }

    public void ChangeMasterVol()
    {
        mainAudioMixer.SetFloat("Vol_Master", Mathf.Clamp(Mathf.Log10(masterVol.value) * 20.0f, -80.0f, 20.0f));
        PlayerPrefs.SetFloat("Vol_Master", masterVol.value);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVol()
    {
        mainAudioMixer.SetFloat("Vol_Music", Mathf.Clamp(Mathf.Log10(musicVol.value) * 20.0f, -80.0f, 20.0f));
        PlayerPrefs.SetFloat("Vol_Music", musicVol.value);
        PlayerPrefs.Save();
    }

    public void ChangeSFXVol()
    {
        mainAudioMixer.SetFloat("Vol_SFX", Mathf.Clamp(Mathf.Log10(sfxVol.value) * 20.0f, -80.0f, 20.0f));
        PlayerPrefs.SetFloat("Vol_SFX", sfxVol.value);
        PlayerPrefs.Save();
    }
}

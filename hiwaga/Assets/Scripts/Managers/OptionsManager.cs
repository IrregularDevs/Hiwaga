using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class OptionsManager : MonoBehaviour
{
    private static OptionsManager instance;
    public static OptionsManager Instance => instance;

    [SerializeField] private GameObject inGameMenu, buttonPause;
    [SerializeField] private Slider masterVolMain, masterVolInGame, musicVolMain, musicVolInGame, sfxVolMain, sfxVolInGame;
    [SerializeField] private AudioMixer mainAudioMixer;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        mainAudioMixer.SetFloat("Vol_Master", ClampLog(PlayerPrefs.GetFloat("Vol_Master", 1.0f)));
        mainAudioMixer.SetFloat("Vol_Music", ClampLog(PlayerPrefs.GetFloat("Vol_Music", 1.0f)));
        mainAudioMixer.SetFloat("Vol_SFX", ClampLog(PlayerPrefs.GetFloat("Vol_SFX", 1.0f)));
        masterVolMain.value = PlayerPrefs.GetFloat("Vol_Master", 1.0f);
        masterVolInGame.value = PlayerPrefs.GetFloat("Vol_Master", 1.0f);
        musicVolMain.value = PlayerPrefs.GetFloat("Vol_Music", 1.0f);
        musicVolInGame.value = PlayerPrefs.GetFloat("Vol_Music", 1.0f);
        sfxVolMain.value = PlayerPrefs.GetFloat("Vol_SFX", 1.0f);
        sfxVolInGame.value = PlayerPrefs.GetFloat("Vol_SFX", 1.0f);

        yield return null;
    }

    private float ClampLog(float vol)
    {
        return Mathf.Clamp(Mathf.Log10(vol) * 20.0f, -80.0f, 20.0f);
    }

    public void ShowGameObject(GameObject element)
    {
        element.SetActive(true);
    }

    public void HideGameObject(GameObject element)
    {
        element.SetActive(false);
    }

    public void ChangeMasterVolMain(VolType volType)
    {
        mainAudioMixer.SetFloat("Vol_Master", ClampLog(masterVolMain.value));
        PlayerPrefs.SetFloat("Vol_Master", masterVolMain.value);
        PlayerPrefs.Save();
        masterVolInGame.value = PlayerPrefs.GetFloat("Vol_Master", 1.0f);
    }

    public void ChangeMasterVolInGame()
    {
        mainAudioMixer.SetFloat("Vol_Master", ClampLog(masterVolInGame.value));
        PlayerPrefs.SetFloat("Vol_Master", masterVolInGame.value);
        PlayerPrefs.Save();
        masterVolMain.value = PlayerPrefs.GetFloat("Vol_Master", 1.0f);
    }

    public void ChangeMusicVolMain()
    {
        mainAudioMixer.SetFloat("Vol_Music", ClampLog(musicVolMain.value));
        PlayerPrefs.SetFloat("Vol_Music", musicVolMain.value);
        PlayerPrefs.Save();
        musicVolInGame.value = PlayerPrefs.GetFloat("Vol_Music", 1.0f);
    }

    public void ChangeMusicVolInGame()
    {
        mainAudioMixer.SetFloat("Vol_Music", ClampLog(musicVolInGame.value));
        PlayerPrefs.SetFloat("Vol_Music", musicVolInGame.value);
        PlayerPrefs.Save();
        musicVolMain.value = PlayerPrefs.GetFloat("Vol_Music", 1.0f);
    }

    public void ChangeSFXVolMain()
    {
        mainAudioMixer.SetFloat("Vol_SFX", ClampLog(sfxVolMain.value));
        PlayerPrefs.SetFloat("Vol_SFX", sfxVolMain.value);
        PlayerPrefs.Save();
        sfxVolInGame.value = PlayerPrefs.GetFloat("Vol_SFX", 1.0f);
    }

    public void ChangeSFXVolInGame()
    {
        mainAudioMixer.SetFloat("Vol_SFX", ClampLog(sfxVolInGame.value));
        PlayerPrefs.SetFloat("Vol_SFX", sfxVolInGame.value);
        PlayerPrefs.Save();
        sfxVolMain.value = PlayerPrefs.GetFloat("Vol_SFX", 1.0f);
    }

    public void OpenCloseMenu(bool state)
    {
        inGameMenu.SetActive(state);
        buttonPause.SetActive(!state);
        QuestManager.Instance.ShowHidePanel(!state);
    }

    public bool GetMenuStateInHierarchy()
    {
        return inGameMenu.activeInHierarchy;
    }

    public bool GetMenuStateSelf()
    {
        return inGameMenu.activeSelf;
    }

    private float TestFunc()
    {
        return 0f;
    }
}

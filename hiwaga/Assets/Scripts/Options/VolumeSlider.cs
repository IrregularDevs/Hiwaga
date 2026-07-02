using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private string type;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(OnChangeValue);
        slider.value = PlayerPrefs.GetFloat(type, 1.0f);
        Debug.Log($"{type} value is {slider.value}.");
    }

    private void OnDisable()
    {
        //slider.onValueChanged.RemoveListener(OnChangeValue);
    }

    private void OnChangeValue(float value)
    {
        OptionsManager.Instance.ChangeVol(type, value);
    }
}

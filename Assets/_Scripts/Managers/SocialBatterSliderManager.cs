using System;
using UnityEngine;
using UnityEngine.UI;

public class SocialBatterSliderManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void OnEnable()
    {
        SocialBatteryManager.OnAddSocialBattery += UpdateSlider;
        SocialBatteryManager.OnSubtractSocialBattery += UpdateSlider;
    }

    void OnDisable()
    {
        SocialBatteryManager.OnAddSocialBattery -= UpdateSlider;
        SocialBatteryManager.OnSubtractSocialBattery -= UpdateSlider;
    }

    private void Start()
    {
        slider.maxValue = SocialBatteryManager.instance.MaxSocialBattery;
        slider.value = SocialBatteryManager.instance.MaxSocialBattery;
    }

    private void UpdateSlider()
    {
        slider.value = SocialBatteryManager.instance.CurrentSocialBattery;
    }
}

using System;
using UnityEngine;

public class SocialBatteryManager : MonoBehaviour
{
    public static SocialBatteryManager instance;

    public static event Action OnAddSocialBattery;
    public static event Action OnSubtractSocialBattery; 

    public int MaxSocialBattery = 2;
    public int CurrentSocialBattery {get; private set;}

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    void Start()
    {
        CurrentSocialBattery = MaxSocialBattery;
    }

    public void AddSocialBattery(int amount)
    {
        CurrentSocialBattery += amount;
        if (CurrentSocialBattery > MaxSocialBattery)
        {
            CurrentSocialBattery = MaxSocialBattery;
        }

        OnAddSocialBattery?.Invoke();
    }

    public void SubtractSocialBattery(int amount)
    {
        CurrentSocialBattery -= amount;
        if (CurrentSocialBattery < 0)
        {
            CurrentSocialBattery = 0;
        }

        OnSubtractSocialBattery?.Invoke();
    }
}

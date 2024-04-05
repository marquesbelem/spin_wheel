using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    [SerializeField] private int _currentCountReward;

    [SerializeField] private int _limitReward;
    
    [SerializeField] private bool _canCheckReward;

    private int _isGain;

    public bool IsGain => Convert.ToBoolean(_isGain); 

    public bool CanCheckReward => _canCheckReward; 

    public static RewardsController Instance { get; private set; }
    public int LimitReward => _limitReward;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;


            DontDestroyOnLoad(this);
        }
    }

    public bool IsLimitReward()
    {
        return _currentCountReward == _limitReward;
    }

    public void IncreaseCurrentReward()
    {
        _currentCountReward += 1;
    }

    public void SetLimitReward(int value)
    {
        _limitReward = value;
        _currentCountReward = 0;
    }

    public void SetCanCheckReward(bool value)
    {
        _canCheckReward = value;
    }

    public void SetIsGain(int value)
    {
        _isGain = value;
    }

    public int GetCountRewardToGain()
    {
        return _limitReward - _currentCountReward;
    }
}

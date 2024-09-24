using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    [SerializeField] private int _currentCountReward;

    [SerializeField] private int _limitReward;

    [SerializeField] private bool _canCheckReward;


    public bool IsGain()
    {
        if (_currentReward == null) return false;
        else return Convert.ToBoolean(_currentReward.Type);
    }


    public bool CanCheckReward => _canCheckReward;

    public static RewardsController Instance { get; private set; }
    public int LimitReward => _limitReward;

    [SerializeField] private ConfigReward[] _configRewards;
    private Reward _currentReward;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _configRewards = FindObjectsByType<ConfigReward>(FindObjectsSortMode.None);
    }

    public void SetLimitByConfigReward()
    {
        var limit = 0;

        for (int i = 0; i < _configRewards.Length; i++)
        {
            limit += _configRewards[i].RewardCount;
            if (_configRewards[i].RewardCount > 0 &&
                _dicRewardGain.ContainsKey(_configRewards[i].RewardName) == false)
            {
                _dicRewardGain.Add(_configRewards[i].RewardName, 0);
            }
        }

        SetLimitReward(limit);
    }

    private Dictionary<string, int> _dicRewardGain = new Dictionary<string, int>();
    public bool CheckIsLose()
    {
        var config = GetConfig();

        if (config == null) return true;

        if (_dicRewardGain.ContainsKey(_currentReward.Name) == false ||
            config.RewardCount == _dicRewardGain[config.RewardName])
        {
            if (_currentReward.CountName > 1)
            {
                config = GetConfig();
            }

            return true;
        }

        return false;
    }

    public string GetRewardName()
    {
        var config = GetConfig();

        if (config == null) return "";

        if (_dicRewardGain[config.RewardName] < config.RewardCount)
        {
            _dicRewardGain[config.RewardName]++;
        }

        return $"{config.Description}";
    }

    public string GetRewardCount()
    {
        var config = GetConfig();

        if (config == null) return "";

        return $"<size=70>R{_dicRewardGain[config.RewardName]}/{config.RewardCount}</size>";
    }

    private ConfigReward GetConfig()
    {
        if (_configRewards.Length == 0 || _currentReward == null) return null;

        var config = _configRewards.First(c => c.RewardName == _currentReward.Name);
        return config;
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

    public void SetCurrentReward(Reward rw)
    {
        _currentReward = rw;
    }

    public int GetCountRewardToGain()
    {
        return _limitReward - _currentCountReward;
    }
}

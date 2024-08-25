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

    private int _isGain;

    public bool IsGain => Convert.ToBoolean(_isGain);

    public bool CanCheckReward => _canCheckReward;

    public static RewardsController Instance { get; private set; }
    public int LimitReward => _limitReward;

    [SerializeField] private ConfigReward[] _configRewards;
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
    public string GetRewardName()
    {
        var randomIndex = 0;
        if (_configRewards.Length != 1)
            randomIndex = UnityEngine.Random.Range(0, _configRewards.Length - 1);

        var config = _configRewards[randomIndex];

        if (_dicRewardGain.ContainsKey(config.RewardName) == false ||
            config.RewardCount == _dicRewardGain[config.RewardName])
        {

            return GetRewardName();
        }

        _dicRewardGain[config.RewardName]++;

        return config.RewardName;
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

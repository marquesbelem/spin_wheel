using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RewardsController : MonoBehaviour
{
    [SerializeField] private int _currentCountReward;

    [SerializeField] private int _limitReward;

    [SerializeField] private bool _canCheckReward;

    [SerializeField] private List<Reward> _rewardList;
    public bool IsGain()
    {
        if (_currentReward == null) return false;
        else return Convert.ToBoolean(_currentReward.Type);
    }


    public bool CanCheckReward => _canCheckReward;

    public static RewardsController Instance { get; private set; }
    public int LimitReward => _limitReward;

    [SerializeField] private ConfigReward[] _configRewards;
    [SerializeField] private Reward[] _rewards;
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

    //Chamando no botão
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
        var config = _configRewards.First(c => c.RewardName == _currentReward.Name);

        if (config == null) return true;

        if (_dicRewardGain.ContainsKey(_currentReward.Name) == false ||
            config.RewardCount == _dicRewardGain[config.RewardName])
        {
            if (_currentReward.CountName > 1)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    int randomIndex = 0; 
    public string GetRandomRewardName()
    {
        if (randomIndex > _configRewards.Length  - 1 )
        {
            randomIndex = 0;
        }

        var config = _configRewards[randomIndex];

        if (_dicRewardGain.ContainsKey(config.RewardName) == false ||
            config.RewardCount == _dicRewardGain[config.RewardName])
        {
            randomIndex++;
            return GetRandomRewardName();
        }

        return GetDescription(config);
    }

    private string GetDescription(ConfigReward config)
    {
        if (config == null) return "";

        if (_dicRewardGain[config.RewardName] < config.RewardCount)
        {
            _dicRewardGain[config.RewardName]++;
        }

        return $"{config.Description}";
    }

    public string GetRewardName()
    {
        var config = _configRewards.First(c => c.RewardName == _currentReward.Name);

        return GetDescription(config);
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

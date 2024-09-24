using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Newtonsoft.Json.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _painelGetReward;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private ValidatorReward _validatorReward;
    [SerializeField] private TMP_Text _textCode;

    [SerializeField] private List<string> _messageYoLose = new List<string>();
    public static GameController Instance { get; private set; }
    [SerializeField] private Roulette _roulette;

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

        _painelGetReward.gameObject.SetActive(false);
        _painelGetReward.DOFade(Convert.ToInt32(false), 1.5f);

        _textCode.text = $"R{RewardsController.Instance.LimitReward}";
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void SetActivePainel(bool value, bool isGain = false)
    {
        _painelGetReward.gameObject.SetActive(value);
        _painelGetReward.DOFade(Convert.ToInt32(value), 1.5f);

        if (RewardsController.Instance.GetCountRewardToGain() <= 0)
        {
            _messageText.text = $"Acabou os prêmios";
            return;
        }
        if (isGain == false) return;

        if (RewardsController.Instance.CheckIsLose())
        {
            _messageText.text = $"{RewardsController.Instance.GetRandomRewardName()}";
        }

        else
        {
            _messageText.text = $"{RewardsController.Instance.GetRewardName()}";
        }


        RewardsController.Instance.IncreaseCurrentReward();
        _textCode.text = $"R{RewardsController.Instance.GetCountRewardToGain()}";
    }

    public void GetReward()
    {
        SetActivePainel(true, RewardsController.Instance.IsGain());
    }

    public void EnabledValidatorReward(bool value)
    {
        _validatorReward.EnabledCollider(value);
    }

    public void UpdateTextCode()
    {
        _textCode.text = $"R{RewardsController.Instance.LimitReward}";
    }
}

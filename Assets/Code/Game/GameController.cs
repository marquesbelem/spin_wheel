using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _painelGetReward;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private ValidatorReward _validatorReward;
    [SerializeField] private TMP_Text _textCode;

    [SerializeField] private List<string> _messageYoLose = new List<string>();
    public static GameController Instance { get; private set; }

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
        SetActivePainel(false, false);

        _textCode.text = $"R{RewardsController.Instance.LimitReward}";
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void SetActivePainel(bool value, bool isGain=false)
    {
        _painelGetReward.gameObject.SetActive(value);
        _painelGetReward.DOFade(Convert.ToInt32(value), 1.5f);

        if (isGain)
        {
            _messageText.text = $" Parabéns! \n\n Você ganhou {RewardsController.Instance.GetRewardName()}!";
        }
        else
        {
            var index = UnityEngine.Random.Range(0, _messageYoLose.Count - 1);
            _messageText.text = $"Não foi dessa vez! \n\n {_messageYoLose[index]}";
        }
    }

    public void GetReward()
    {
        SetActivePainel(true, RewardsController.Instance.IsGain);

        if (RewardsController.Instance.IsGain)
        {
            RewardsController.Instance.IncreaseCurrentReward();
        }

        _textCode.text = $"R{RewardsController.Instance.GetCountRewardToGain()}";
    }

    public void EnabledValidatorReward()
    {
        _validatorReward.EnabledCollider();
    }

    public void UpdateTextCode()
    {
        _textCode.text = $"R{RewardsController.Instance.LimitReward}";
    }
}

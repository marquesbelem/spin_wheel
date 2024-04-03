using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _panelLimitReward;
    [SerializeField] private Button _btnBackMenu;
    [SerializeField] private CanvasGroup _painelGetReward;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private ValidatorReward _validatorReward;

    private List<string> _messageYoLose = new List<string>();

    private void OnEnable()
    {
        _btnBackMenu.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _btnBackMenu.onClick.RemoveListener(LoadScene);
    }

    private void Start()
    {
        _painelGetReward.gameObject.SetActive(false);
        _painelGetReward.DOFade(0,0);

        SetActivePainel(false);

        _messageYoLose.Add("Mas n�o deixe de aproveitar todas as vantagens de ser uma ag�ncia parceira Sakura.");
        _messageYoLose.Add("Mas vamos te dar uma nova chance. Tente outra vez!");
        _messageYoLose.Add("Mas vamos te dar uma nova chance. Tente�outra�vez!");
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SetActivePainel(bool value)
    {
        _panelLimitReward.gameObject.SetActive(value);
        _panelLimitReward.DOFade(Convert.ToInt32(value), 1.5f);
    }

    public void GetReward()
    {
        _painelGetReward.gameObject.SetActive(true);
        _painelGetReward.DOFade(1, 1.5f);

        if (RewardsController.Instance.IsGain)
        {
            RewardsController.Instance.IncreaseCurrentReward();
            _messageText.text = " Parab�ns! \n Pegue seu brinde e pinte o turismo de rosa com a gente!";
        }
        else
        {
            var index = UnityEngine.Random.Range(0, _messageYoLose.Count - 1);
            _messageText.text = $"N�o foi dessa vez! \n {_messageYoLose[index]}";
        }
    }

    public void EnabledValidatorReward()
    {
        _validatorReward.EnabledCollider();
    }
}

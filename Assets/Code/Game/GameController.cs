using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _panelLimitReward;
    [SerializeField] private Button _btnBackMenu;
    [SerializeField] private GameObject _painelGetReward;
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
        _painelGetReward.SetActive(false);
        SetActivePainel(false);

        _messageYoLose.Add("Mas não deixe de aproveitar todas as vantagens de ser uma agência parceira Sakura.");
        _messageYoLose.Add("Mas vamos te dar uma nova chance. Tente outra vez!");
        _messageYoLose.Add("Mas vamos te dar uma nova chance. Tente outra vez!");
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SetActivePainel(bool value)
    {
        _panelLimitReward.SetActive(value);
    }

    public void GetReward()
    {
        _painelGetReward.SetActive(true);

        if (RewardsController.Instance.IsGain)
        {
            RewardsController.Instance.IncreaseCurrentReward();
            _messageText.text = " Parabéns! \n Pegue seu brinde e pinte o turismo de rosa com a gente!";
        }
        else
        {
            var index = Random.Range(0, _messageYoLose.Count - 1);
            _messageText.text = $"Não foi dessa vez! \n {_messageYoLose[index]}";
        }
    }

    public void EnabledValidatorReward()
    {
        _validatorReward.EnabledCollider();
    }
}

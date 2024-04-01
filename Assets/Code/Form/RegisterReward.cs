using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class RegisterReward : MonoBehaviour
{
    private List<RewardData> _rewardsData = new List<RewardData>();

    [SerializeField] private TMP_InputField _input;
    [SerializeField] Button _btnFinish;
    [SerializeField] Button _btnAdd;
    [SerializeField] Button _btnReset;
    [SerializeField] TMP_Text _listRewardsText;
    [SerializeField] TMP_Text _inputPlaceholderText;

    private void OnEnable()
    {
        _btnFinish.onClick.AddListener(Finish);
        _btnAdd.onClick.AddListener(AddFormData);
        _btnReset.onClick.AddListener(ResetList);
        _btnFinish.gameObject.SetActive(false);
        _btnReset.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _btnFinish.onClick.RemoveListener(Finish);
        _btnAdd.onClick.RemoveListener(AddFormData);
        _btnReset.onClick.RemoveListener(ResetList);
    }

    private void AddFormData()
    {
        if (_input.text.Length == 0)
        {
            _inputPlaceholderText.text = "nome invalido";
            return;
        }

        var rewardData = new RewardData
        {
            Name = _input.text
        };

        _rewardsData.Add(rewardData);
        _btnFinish.gameObject.SetActive(true);
        _btnReset.gameObject.SetActive(true);

        ShowListReward();

        _inputPlaceholderText.text = "digite o nome do prêmio";
        _input.text = string.Empty;
    }

    private void Finish()
    {
        var json = JsonConvert.SerializeObject(_rewardsData);

        var filePath = Path.Combine(Application.dataPath, "lista_de_premios.json");

        File.WriteAllText(filePath, json);

        Debug.Log("Arquivo JSON criado e salvo em: " + filePath);
    }

    private void ResetList()
    {
        _rewardsData.Clear();
        ShowListReward();
    }

    private void ShowListReward()
    {
        _listRewardsText.text = string.Empty;

        for (int i = 0; i < _rewardsData.Count; i++)
        {
            _listRewardsText.text += _rewardsData[i].Name + "\n";
        }
    }
}
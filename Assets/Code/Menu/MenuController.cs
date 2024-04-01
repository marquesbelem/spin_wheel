using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _btnGame;
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private TMP_Text _inputPlaceholderText;

    private void OnEnable()
    {
        _btnGame.onClick.AddListener(CheckInput);
    }

    private void OnDisable()
    {
        _btnGame.onClick.RemoveListener(CheckInput);
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void CheckInput()
    {
        if(_input.text.Length == 0)
        {
            _inputPlaceholderText.text = "VALOR INVALIDO, DIGITE UMA QUANTIDADE DE PRÊMIOS";
            return;
        }

        if(Int32.TryParse(_input.text, out var number))
        {
            RewardsController.Instance.SetLimitReward(number);
            LoadScene(1);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigReward : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_RewardNameText;
    [SerializeField] private TMPro.TMP_InputField m_InputField;

    [SerializeField] private int m_RewardCount;

    public int RewardCount => m_RewardCount;
    public string RewardName => m_RewardNameText.text;

    private void Awake()
    {
        m_InputField.onValueChanged.AddListener(UpdateCount);
    }

    private void UpdateCount(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            m_RewardCount = 0;
            return;
        }
        m_RewardCount = Convert.ToInt32(value);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigReward : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_RewardNameText;
    [SerializeField] private TMPro.TMP_Text m_RewardCountText;
    [SerializeField] private Button m_AddCountReward; 
    [SerializeField] private Button m_RemoveCountReward;

    [SerializeField] private int m_RewardCount;

    public int RewardCount => m_RewardCount;
    public string RewardName => m_RewardNameText.text;

    private void Awake()
    {
        m_AddCountReward.onClick.AddListener(Add);
        m_RemoveCountReward.onClick.AddListener(Remove);
    }

    private void Add()
    {
        m_RewardCount++;
        UpdateRewardCountText();
    }

    private void Remove()
    {
        if (m_RewardCount == 0) return; 
        m_RewardCount--;

        UpdateRewardCountText();
    }

    private void UpdateRewardCountText()
    {
        m_RewardCountText.text = m_RewardCount.ToString();
    }

}

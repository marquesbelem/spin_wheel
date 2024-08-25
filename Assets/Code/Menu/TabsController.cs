using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabsController : MonoBehaviour
{
    [SerializeField] private Button m_SpinTabBtn;
    [SerializeField] private Button m_EditTabBtn;

    [SerializeField] private GameObject m_SpinTab;
    [SerializeField] private GameObject m_EditTab;

    [SerializeField] private Color m_ActiveColor;
    [SerializeField] private Color m_DesactiveColor;

    private void Awake()
    {
        m_SpinTabBtn.onClick.AddListener(ShowSpin);
        m_EditTabBtn.onClick.AddListener(ShowEdit);

        ShowEdit();
    }

    private void ShowEdit() 
    {
        m_SpinTab.SetActive(false);
        SetDesactiveColor(m_SpinTabBtn);

        m_EditTab.SetActive(true);
        SetActiveColor(m_EditTabBtn);
    }

    private void ShowSpin()
    {
        m_SpinTab.SetActive(true);
        SetActiveColor(m_SpinTabBtn);

        m_EditTab.SetActive(false);
        SetDesactiveColor(m_EditTabBtn);
    }

    private void SetDesactiveColor(Button btn)
    {
        var colors = btn.colors;
        colors.normalColor = m_DesactiveColor;
        btn.colors = colors;
    }

    private void SetActiveColor(Button btn)
    {
        var colors = btn.colors;
        colors.normalColor = m_ActiveColor;
        btn.colors = colors;
    }
}

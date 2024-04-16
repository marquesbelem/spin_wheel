using UnityEngine;
using DG.Tweening;

public class DisabledAuto : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    void OnEnable()
    {
        Invoke("InvokeDisable", 5f);
    }

    public void InvokeDisable()
    {
        _canvasGroup.DOFade(0, 1.5f).onComplete = SetDisable;
    }

    void SetDisable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}

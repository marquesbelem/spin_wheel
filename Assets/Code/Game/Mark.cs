using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Mark : MonoBehaviour
{
    [SerializeField] RectTransform _rect;
    [SerializeField] private Button _btnSpin;
    [SerializeField] private float _durationShake;

    private void OnEnable()
    {
        //_btnSpin.onClick.AddListener(Shake);
    }

    private void OnDisable()
    {
        //_btnSpin.onClick.RemoveListener(Shake);
    }

    public void Shake()
    {
        _rect.DOShakeAnchorPos(_durationShake, new Vector2(10, 0));
    }
}

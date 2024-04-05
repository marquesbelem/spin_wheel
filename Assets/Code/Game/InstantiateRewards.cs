using UnityEngine;

public class InstantiateRewards : MonoBehaviour
{
    [SerializeField] private Reward _rewardPrefab;
    [SerializeField] private int _countReward = 10;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _offsetAngle = 0f;
    [SerializeField] private Transform _parent;
    [SerializeField] private RectTransform _parentRecTransform;
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        var anguloIncremental = 360f / _countReward;
        for (int i = 0; i < _countReward; i++)
        {
            var anguloAtual = i * anguloIncremental + _offsetAngle;
            var posX = Mathf.Cos(anguloAtual * Mathf.Deg2Rad) * _radius;
            var posY = Mathf.Sin(anguloAtual * Mathf.Deg2Rad) * _radius;
            var posicaoCanvas = new Vector2(_parentRecTransform.rect.width / 2 + posX, _parentRecTransform.rect.height / 2 + posY);

            var go = Instantiate(_rewardPrefab.gameObject, posicaoCanvas, Quaternion.identity, _parent).GetComponent<Reward>();
           // go.Setup(i);
        }
    }
}

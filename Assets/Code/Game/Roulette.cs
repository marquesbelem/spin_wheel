using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopSpeed;
    [SerializeField] private float _timeToStop;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Button _btnSpin;
    [SerializeField] private GameController _gameController;
    
    private float _time;

    private bool _canSpin = false;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _routineStop;

    private void OnEnable()
    {
        _btnSpin.onClick.AddListener(Spin);
    }

    private void OnDisable()
    {
        _btnSpin.onClick.RemoveListener(Spin);
    }

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_timeToStop * 1.5f);
        _stopSpeed = _speed;
    }

    private void FixedUpdate()
    {
        if (_canSpin == false) return;

        _time += 1 * Time.deltaTime;

        if (_time > _timeToStop)
        {
            _time = 0;
            _routineStop = StartCoroutine(Routine());
        }
    }

    private IEnumerator Routine()
    {
        _canSpin = false;

        yield return _waitForSeconds;

        if (_stopSpeed <= 0 && _routineStop != null)
        {
            RewardsController.Instance.SetCanCheckReward(true);
            
            yield return _waitForSeconds;
            _gameController.GetReward();
            
            StopCoroutine(_routineStop);
            _routineStop = null;

            yield return null;
        }

        _stopSpeed -= 100;
        _rigidbody2D.angularVelocity = Mathf.Clamp(_rigidbody2D.angularVelocity, 0, _stopSpeed);
        _routineStop = StartCoroutine(Routine());

    }

    private void Spin()
    {
        RewardsController.Instance.SetCanCheckReward(false);

        if (RewardsController.Instance.IsLimitReward())
        {
            _gameController.SetActivePainel(true);
            return;
        }

        if (_time != 0) return;

        _gameController.EnabledValidatorReward();
        _canSpin = true;
        _stopSpeed = _speed;
        _rigidbody2D.AddTorque(_speed);
    }
}

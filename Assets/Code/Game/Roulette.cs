using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopSpeed;
    [SerializeField] private float _timeToStop;

    [SerializeField] private Button _btnSpin;
    [SerializeField] private GameController _gameController;
    [SerializeField] private FlasherController _flasherController;
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private GameObject _audioBackground;

    private float _time;

    private bool _canSpin = false;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _routineStop;
    private Coroutine _routineFlasher;

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
        _waitForSeconds = new WaitForSeconds(1.5f);
        _stopSpeed = _speed;

        _audio.pitch = -3;
    }

    private void Update()
    {
        if (_canSpin == false) return;

        if (_time >= _timeToStop)
        {
            if (_stopSpeed > 0)
            {
                var z = _rectTransform.localEulerAngles.z + _stopSpeed * Time.deltaTime;
                _rectTransform.localEulerAngles = new Vector3(_rectTransform.localEulerAngles.x, _rectTransform.localEulerAngles.y, z);
                _stopSpeed -= 1;
                _audio.pitch = _audio.pitch + 0.6f * Time.deltaTime;
            }
            else
            {
                _time = 0;
                _audio.gameObject.SetActive(false);
                _routineStop = StartCoroutine(Routine());
            }

            return;
        }

        _time += 1 * Time.deltaTime;

        var newZRotation = _rectTransform.localEulerAngles.z + Random.Range(0,_speed) + _speed * Time.deltaTime;
        _rectTransform.localEulerAngles = new Vector3(_rectTransform.localEulerAngles.x, _rectTransform.localEulerAngles.y, newZRotation);
    }

    private IEnumerator Routine()
    {
        _canSpin = false;

        RewardsController.Instance.SetCanCheckReward(true);

        yield return _waitForSeconds;

        _gameController.GetReward();

        StopCoroutine(_routineFlasher);

        _routineFlasher = null;
        _flasherController.StopInternalCoroutine();
        _btnSpin.interactable = true;

        _stopSpeed = _speed;
        _audio.pitch = -3;
        _audioBackground.SetActive(false);
    }

    private void Spin()
    {
        RewardsController.Instance.SetCanCheckReward(false);

        if (RewardsController.Instance.IsLimitReward())
        {
            _gameController.SetActivePainel(true);
            _btnSpin.interactable = true;
            return;
        }

        if (_time != 0) return;

        _gameController.EnabledValidatorReward();
        _canSpin = true;

        _routineFlasher = StartCoroutine(_flasherController.Routine());
        _btnSpin.interactable = false;
        _audio.gameObject.SetActive(true);
        _audioBackground.gameObject.SetActive(true);
    }
}

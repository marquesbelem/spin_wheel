using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlasherController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _flashers = new List<GameObject>();

    [SerializeField] private float _timeToWaitForSeconds = 0.1f;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        for (int i = 0; i < _flashers.Count; i++)
        {
            _flashers[i].SetActive(false);
        }

        _waitForSeconds = new WaitForSeconds(_timeToWaitForSeconds);
    }

    private Coroutine _routine; 
    public IEnumerator Routine()
    {

        for (int i = 0; i < _flashers.Count; i++)
        {
            _flashers[i].SetActive(false);
        }

        for (int i = 0; i < _flashers.Count; i++)
        {
            _flashers[i].SetActive(true);
            yield return _waitForSeconds;
            _flashers[i].SetActive(false);
        }

        _routine= StartCoroutine(Routine());
    }

    public void StopInternalCoroutine()
    {
       StopCoroutine(_routine);
        _routine = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmSignal : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSignal;
    [Header("Шаг изменения значения")]
    [SerializeField] private float _duration;

    private float _requiredValue;
    private float _tempVolumeValue;
    private bool _inHouse;
    private Coroutine _coroutine;

    private void Start()
    {
        _alarmSignal.volume = 0;
        _tempVolumeValue = _duration * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(_inHouse == false)
            StartCoroutine(ChangeSoundVolume());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inHouse = true;
        _alarmSignal.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        while (_alarmSignal.volume != _requiredValue)
            _coroutine = StartCoroutine(ChangeSoundVolume());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        StopCoroutine(_coroutine);
        _inHouse = false;
    }

    private IEnumerator ChangeSoundVolume()
    {
        if (_inHouse)
        {
            _requiredValue = 1;
        }
        else
        {
            _requiredValue = 0;
        }

        while (_alarmSignal.volume != _requiredValue)
        {
            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _requiredValue, _tempVolumeValue);
        }

        yield return null;
    }
}

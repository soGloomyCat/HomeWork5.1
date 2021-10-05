using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmSignal : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSignal;
    [Header("Шаг изменения значения. От 0,1 до 0,7")]
    [SerializeField] private float _duration;

    private float _requiredValue;
    private float _minValue;
    private float _maxValue;
    private float _tempVolumeValue;
    private bool _inHouse;

    private void Start()
    {
        _alarmSignal.volume = 0;
        _minValue = 0.1f;
        _maxValue = 0.7f;
        _tempVolumeValue = _duration * Time.deltaTime;

        if (_duration < _minValue)
        {
            _duration = 0.1f;
        }
        else if (_duration > _maxValue)
        {
            _duration = 0.7f;
        }
    }

    private void FixedUpdate()
    {
        if(_inHouse == false)
            StartCoroutine("SoundReduction");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inHouse = true;
        _alarmSignal.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine("SoundReduction");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _inHouse = false;
    }

    private IEnumerator SoundReduction()
    {
        if (_inHouse)
        {
            _requiredValue = 1;

            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _requiredValue, _tempVolumeValue);
        }
        else
        {
            _requiredValue = 0;

            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _requiredValue, _tempVolumeValue);
        }
        
        yield return null;
    }
}

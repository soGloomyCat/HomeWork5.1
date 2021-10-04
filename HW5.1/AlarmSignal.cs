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

    private void Start()
    {
        if (_duration < 0.1f)
        {
            _duration = 0.1f;
        }
        else if (_duration > 0.7f)
        {
            _duration = 0.7f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _alarmSignal.volume = 0;

        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _alarmSignal.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float tempVolumeValue = _duration * Time.deltaTime;

        if (_alarmSignal.volume == 1)
        {
            _requiredValue = 0;
        }
        else if (_alarmSignal.volume == 0)
        {
            _requiredValue = 1;
        }

        _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _requiredValue, tempVolumeValue);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _alarmSignal.Stop();
        }
    }
}

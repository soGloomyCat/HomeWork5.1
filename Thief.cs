using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-(_speed * Time.deltaTime), 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(0, _jumpForce * (_speed * Time.deltaTime), 0);
        }
    }
}

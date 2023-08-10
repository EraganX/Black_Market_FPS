using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{/*
    [SerializeField] private float _sensetivity = 3.5f;
    private float _mouseX;

    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") ;
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y + _mouseX *_sensetivity * Time.deltaTime * 30f,
            transform.localEulerAngles.z
            );
    }*/
    [SerializeField] private float _sensitivity = 2f;
    private float _mouseX;

    private void LateUpdate()
    {
        if (Time.timeScale >0)
        {
            _mouseX = Input.GetAxis("Mouse X");

            float rotationAmount = _mouseX * _sensitivity * Time.deltaTime * 30f;
            rotationAmount = Mathf.Clamp(rotationAmount, -30f, 30f); // Adjust the values as needed

            transform.Rotate(Vector3.up, rotationAmount); // Rotate around the Y-axis
        }
       
    }
}

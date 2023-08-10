using UnityEngine;

public class LookY : MonoBehaviour
{/*
    [SerializeField]private float 
        _sensetivity = 3.5f, 
        _lookUpMax=45f,
        _lookUpMin=-45f;
    private float _mouseY;
    private Vector3 _look;
    [SerializeField]private Camera fpsCamera;
    private bool _isZoom = false;

    private void Awake()
    {
       _look = transform.localEulerAngles;
    }
    private void LateUpdate()
   {
        _mouseY = Input.GetAxis("Mouse Y");
       _look.x += -(_mouseY) * _sensetivity * Time.deltaTime * 30f;
       _look.x = Mathf.Clamp(_look.x, _lookUpMin, _lookUpMax);
       transform.localEulerAngles = _look;

        if (Input.GetButtonDown("Fire2"))
        {
            _isZoom = !_isZoom;

            if (_isZoom == true)
            {
                fpsCamera.fieldOfView=25;
            }
            else
            {
                Camera.main.fieldOfView = 60;
            }
        }
    }*/

    [SerializeField] private float _sensitivity = 3.5f;
    [SerializeField] private float _lookUpMax = 45f;
    [SerializeField] private float _lookUpMin = -45f;

    private float _mouseY;
    private Vector3 _look;
    [SerializeField] private Camera fpsCamera;
    private bool _isZoom = false;

    private void Awake()
    {
        _look = transform.localEulerAngles;
    }

    private void LateUpdate()
    {
        if (Time.timeScale>0)
        {
            _mouseY = Input.GetAxis("Mouse Y");
            _look.x -= _mouseY * _sensitivity * Time.deltaTime * 30f; // Invert the axis for natural-looking movement
            _look.x = Mathf.Clamp(_look.x, _lookUpMin, _lookUpMax);
            transform.localEulerAngles = _look;

            if (Input.GetButtonDown("Fire2"))
            {
                _isZoom = !_isZoom;

                if (_isZoom)
                {
                    fpsCamera.fieldOfView = 25f;
                }
                else
                {
                    fpsCamera.fieldOfView = 60f;
                }
            }
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController _characterController;
    private float _gravity = 9.81f;

    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private AudioSource _bgMusic;
    [SerializeField] private GameObject _inventoryManager;
    [SerializeField] private GameObject _uiManager;
    [SerializeField] private GameObject _pauseScreen;

    public bool hasCoin = false;
    public bool pause;
    InventoryScript inventoryScript;
    UIManager uiManagerScript;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        inventoryScript = _inventoryManager.GetComponent<InventoryScript>();
        uiManagerScript = _uiManager.GetComponent<UIManager>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        ToggleBackgroundMusic(true);
        pause = false;
        _pauseScreen.SetActive(false);
    }

    private void Update()
    {
        PlayerMove();
        CursorMode();

       if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

            if (pause)
            {
                Time.timeScale = 0.0f;
                _pauseScreen.SetActive(true);
                ToggleBackgroundMusic(false);
            }
            else
            {
                Time.timeScale = 1.0f;
                _pauseScreen.SetActive(false);
                ToggleBackgroundMusic(true);
            }
        }

       
    }

    private void ToggleBackgroundMusic(bool play)
    {
        if (play)
        {
            _bgMusic.Play();
        }
        else
        {
            _bgMusic.Pause();
        }
    }

    private void CursorMode()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None; 
                _bgMusic.Pause();
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Locked;
                _bgMusic.UnPause();
            }
        }
    }//cursor lock and Unlock using escape key and music control

    private void PlayerMove()
    {
        Vector3 _direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 _velocity = _direction * _moveSpeed;
        _velocity.y -= _gravity;
        _velocity = transform.transform.TransformDirection(_velocity);
        _characterController.Move(_velocity * Time.deltaTime);
    } // player movement

    private void OnTriggerStay(Collider target)
    {
        if (target.CompareTag("shop") && hasCoin)
        {
            uiManagerScript.ShopHintText(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventoryScript.isWeaponActive == false)
                {
                    inventoryScript.WeaponActivate(true);
                    inventoryScript.updateCoin(-50);
                }
                else
                {
                    inventoryScript.updateAmmo(+450);
                    inventoryScript.updateCoin(-25);
                }

            }
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.CompareTag("shop"))
        {
            uiManagerScript.ShopHintText(false);
        }
    }
}

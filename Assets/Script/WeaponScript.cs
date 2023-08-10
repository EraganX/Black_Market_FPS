using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]private ParticleSystem _muzzelSplash;
    [SerializeField]private GameObject hitMarker;
    [SerializeField]private GameObject _inventoryManage;
    [SerializeField]private Transform _fpsCam;
    [SerializeField]private Transform _weapon;
    [SerializeField]private float _shootRange = 100f;
    [SerializeField]private int _fireRatePerSecond = 10;
    [SerializeField]private float _reloadTime = 10f;
    [SerializeField]private AudioClip _audioClip;
    [SerializeField]private GameObject box;

    private bool _isFiring;
    private bool _isReloading;

    private AudioSource _audioSource;
    private int _maxAmmoInMag = 50;
    private int _CurrentAmmo;
    private float _lastTimeShoot = 0;

    private UIManager _UIManager;
    private InventoryScript _inventoryScript;
    private PlayerScript playerScript;



    private void Awake()
    {
        _isFiring = false;
        _muzzelSplash.Stop();
        _CurrentAmmo = _maxAmmoInMag;
        _isReloading = false;
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _inventoryScript = _inventoryManage.GetComponent<InventoryScript>();
        
    }

    private void Update()
    {
        if (Time.timeScale>0)
        {
            FireInput();
            _UIManager.CurrentAmmoText(_CurrentAmmo, _inventoryScript.invenAmmo);
            WeaponShooting();
            ReloadInputs();
        }
        
    }

    private void WeaponShooting()
    {

        if (_isFiring && !_isReloading && (_CurrentAmmo > 0) && (Time.time - _lastTimeShoot >= 1f / _fireRatePerSecond))
        {
            if (!_audioSource.isPlaying)
            {
                AudioPlaying();
            }
            _muzzelSplash.Play();
            WeaponShoot();
            _lastTimeShoot = Time.time;
        }
        else
        {
            _muzzelSplash.Stop();
        }
    }

    private void ReloadInputs()
    {
        if (Input.GetKeyDown(KeyCode.R) && (_CurrentAmmo != _maxAmmoInMag))
        {
            if (_inventoryScript.invenAmmo > 0)
            {
                _inventoryScript.invenAmmo += _CurrentAmmo;
                _CurrentAmmo = 0;
                _isReloading = true;
                StartCoroutine(WeaponReload());
            }
        }

        if (_CurrentAmmo < 10)
        {
            _UIManager.AmmoReloadHintText(true);
        }
        else
        {
            _UIManager.AmmoReloadHintText(false);
        }
    }

    private void AudioPlaying()
    {
        _audioSource.time = 0;
        _audioSource.PlayOneShot(_audioClip);
        Invoke("AudioStop",0.4f);
    }

    private void AudioStop()
    {
        _audioSource.Stop();    
    }

    private IEnumerator WeaponReload()
    {
        _weapon.localRotation = Quaternion.Euler(10,0,45);
        yield return new WaitForSeconds(_reloadTime);
        _CurrentAmmo = _maxAmmoInMag;
        _inventoryScript.invenAmmo -= _maxAmmoInMag;
        _isReloading=false;
        _weapon.localRotation = Quaternion.Euler(0, 0, 90);
    }

    private void FireInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _isFiring = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _isFiring = false;
        }

        if (_isFiring && !_isReloading)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1.02f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1f);
        }
    }//FireInput

    private void WeaponShoot()
    {
        _CurrentAmmo--;
        RaycastHit hitInfo;
        if (Physics.Raycast(_fpsCam.position,_fpsCam.forward,out hitInfo, _shootRange))
        {
            ProjectHitMarker(hitInfo);
        }
        else
        {
            return;
        }
    }//WeaponShoot

    private void ProjectHitMarker(RaycastHit hitInfo)
    {
        GameObject _hitMark = Instantiate(hitMarker,hitInfo.point,Quaternion.LookRotation(hitInfo.normal));
        Destroy(_hitMark,1f);

        if (hitInfo.transform.tag == "box")
        {
            Vector3 loc= hitInfo.transform.position;
            Destroy(hitInfo.transform.gameObject);
            Instantiate(box,loc,Quaternion.identity);
        }
    }//ProjectHitMarker
}

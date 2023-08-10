using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private GameObject _weaponOB;
    [SerializeField] private GameObject _weaponIcon;
    [SerializeField] private AudioClip _pickup;
    private int currentCoins = 0;
    public int invenAmmo = 0;
    private UIManager _uiManager;

    public bool isWeaponActive;

    private void Awake()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.CoinText(currentCoins);
        isWeaponActive = false;
        _weaponIcon.SetActive(false);
        _weaponOB.SetActive(false); 
    }
    private void Update()
    {
        _uiManager.CoinText(currentCoins);
    }
    public void updateCoin(int coin)
    {
        if ((currentCoins+coin)<0)
        {
            print("No money to purchase");
        }
        else
        {
            this.currentCoins += coin;
        }
    }

    public void WeaponActivate(bool isActivate)
    {
        if (isActivate)
        {
            Vector3 market = new Vector3(3.52816f,0, -3.118f);
            AudioSource.PlayClipAtPoint(_pickup, market, 0.5f);
            _weaponOB.SetActive(isActivate);
            _weaponIcon.SetActive(isActivate);
            isWeaponActive = true;
        }
    }

    public void updateAmmo(int ammo)
    {
        if ((invenAmmo + ammo) < 0)
        {
            print("Buy Ammo");
        }
        else
        {
            if (currentCoins>=25) {
                Vector3 market = new Vector3(3.52816f, 0, -3.118f);
                AudioSource.PlayClipAtPoint(_pickup, market, 0.5f);
                this.invenAmmo += ammo;
                print(invenAmmo);
            }
            else
            {
                print("you havent money");
            }
        }
    }


}

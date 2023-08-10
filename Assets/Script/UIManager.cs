using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private GameObject _CoinPickupHint;
    [SerializeField] private GameObject _ammoReloadHint;
    [SerializeField] private GameObject _purchaseHint;

    public void CurrentAmmoText(int currentAmmo, int invenAmmo)
    {
        _ammoText.text = currentAmmo.ToString()+"/"+invenAmmo.ToString();
    }

    public void CoinText(int currentCoin)
    {
        _coinText.text = "Coins : " + currentCoin.ToString();
    }

    public void CoinPickUpText(bool activate)
    {
       _CoinPickupHint.SetActive(activate);
    }

    public void AmmoReloadHintText(bool activate)
    {
        _ammoReloadHint.SetActive(activate);
    }
    public void ShopHintText(bool activate)
    {
        _purchaseHint.SetActive(activate);
    }

}

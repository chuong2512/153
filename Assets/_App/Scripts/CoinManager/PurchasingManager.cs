using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
    public void OnPressDown(int i)
    {
        switch (i)
        {
            case 1:
                PlayerPrefs.SetInt("TotalEarning", PlayerPrefs.GetInt("TotalEarning") + 100);
                IAPManager.Instance.BuyProductID(IAPKey.PACK1);
                break;
            case 2:
                PlayerPrefs.SetInt("TotalEarning", PlayerPrefs.GetInt("TotalEarning") + 300);
                IAPManager.Instance.BuyProductID(IAPKey.PACK2);
                break;
            case 3:
                PlayerPrefs.SetInt("TotalEarning", PlayerPrefs.GetInt("TotalEarning") + 500);
                IAPManager.Instance.BuyProductID(IAPKey.PACK3);
                break;
            case 4:
                PlayerPrefs.SetInt("TotalEarning", PlayerPrefs.GetInt("TotalEarning") + 1000);
                IAPManager.Instance.BuyProductID(IAPKey.PACK4);
                break;
        }
    }

    public void Sub(int i)
    {
        GameDataManager.Instance.playerData.SubDiamond(i);
    }
}
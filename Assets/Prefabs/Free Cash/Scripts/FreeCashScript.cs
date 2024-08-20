using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FreeCashScript : MonoBehaviour {

    public DateTime oldTime;
    public DateTime newTime;
    public DateTime currentTime;
    public TimeSpan diffenrce;

    public GameObject FreeCashDialog;
    public GameObject FreeCashButton;
    public Text RemainingTime;
    public static bool isTimeStarted = false;

    long temp;
	// Use this for initialization
	void Start () {
        //AddControllerScript.VehicleSelectionAds();
        //if (PlayerPrefs.GetInt("ButtonEnabled") == 0)
        //{
        //    FreeCashButton.SetActive(true);
        //    isTimeStarted = false;
        //}
        //else if (PlayerPrefs.GetInt("ButtonEnabled") == 1)
        //{
        //    Debug.Log("slslsl");
        //    FreeCashButton.SetActive(false);
        //}
        //Debug.Log(PlayerPrefs.GetString("Time"));
        //if (PlayerPrefs.GetString("Time") == null)
        //{
        //    PlayerPrefs.SetString("Time", DateTime.Now.ToBinary().ToString());
        //}
        //temp = Convert.ToInt64(PlayerPrefs.GetString("Time"));
        //Debug.Log(DateTime.FromBinary(temp));
        //temp = Convert.ToInt64(PlayerPrefs.GetString("Time"));
        //PlayerPrefs.DeleteAll();
      
     

	}
	
	// Update is called once per frame
	void Update () {


        //if (PlayerPrefs.GetInt("ButtonEnabled") == 1)
        //{
        //    calculateTime();
        //}

        //if (PlayerPrefs.GetInt("ButtonEnabled") == 0)
        //{
        //    FreeCashButton.SetActive(true);
        //}
	}

    public void crossButton()
    {
        FreeCashDialog.SetActive(false);
    }
    public void showDialogButton()
    {
        FreeCashDialog.SetActive(true);
    }

    public void WatchNowButton()
    {
        
        FreeCashDialog.SetActive(false);
        showAd();
    }

    public void calculateTime()
    
    {
        TimeSpan totalTime = new TimeSpan(0, 3, 0);
        TimeSpan remaingTime = totalTime.Subtract(diffenrce);
       
        oldTime = DateTime.FromBinary(temp);
       // Debug.Log("Old Date = " + oldTime);
        currentTime = DateTime.Now;
        diffenrce = currentTime.Subtract(oldTime);
       Debug.Log("Time Diffeence" + diffenrce);

        if (diffenrce > TimeSpan.FromMinutes(3f))
        {
         
            PlayerPrefs.SetInt("ButtonEnabled", 0);
            FreeCashButton.SetActive(false);
             RemainingTime.text = "";
             isTimeStarted = false;
             PlayerPrefs.SetString("Time", DateTime.Now.ToBinary().ToString());
        }
        else
        {
            
            RemainingTime.text = "Remaing Time For Free cash " +remaingTime.ToString().Substring(0, 8);
        }
      

    }


    void showAd()
    {
        //AddControllerScript.ShowChartBoost();
        PlayerPrefs.SetInt("TotalEarning", PlayerPrefs.GetInt("TotalEarning") + 100);
       // StartTimer();
       
    }

    void StartTimer()
    {
        isTimeStarted = true;
        FreeCashButton.SetActive(false);
        PlayerPrefs.SetInt("ButtonEnabled", 1);
        oldTime = DateTime.Now;
        PlayerPrefs.SetString("Time", oldTime.ToBinary().ToString());
        temp = Convert.ToInt64(PlayerPrefs.GetString("Time"));
    }

}

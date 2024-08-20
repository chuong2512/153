using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SkipLevelController : MonoBehaviour {

    public GameObject skipLevelDialog;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void skipLevelButton()
    {
        Time.timeScale = 0;
        skipLevelDialog.SetActive(true);
    }
    public void skipLevelDialogYesButton()
    {
        Time.timeScale = 1;
        //AddControllerScript.ShowChartBoost();
        if (PlayerPrefs.GetInt("UnlockedLevel") < 20 && 
            PlayerPrefs.GetInt("UnlockedLevel") == PlayerPrefs.GetInt("Level"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel") + 1);
        }
        PlayerPrefs.SetString("GameOver", "LevelSkipped");
        SceneManager.LoadScene("Game Over");
        
    }

    public void skipLevelDialogNoButton()
    {
        Time.timeScale = 1;
        skipLevelDialog.SetActive(false);
    }

}

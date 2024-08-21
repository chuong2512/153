using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{

	public GameObject crash;
	public GameObject timeUp;
	public GameObject levelComplete;
	
	public GameObject nextButton;
	public GameObject loading;
	
	public Text earningTxt;
	public Text timeTxt;

    public GameObject levelSkippedDialog;
    public Text skippedLevelText;
    public GameObject NextButtonSkip;


	public AudioClip levelCompleteClip;
	public AudioClip gameOverClip;
	
	private AudioSource mAudioSource1;
	private AudioSource mAudioSource2;
    public GameObject RateUsDialog;
	public int totalEarning;

    void Awake()
    {
    }
	// Use this for initialization
	void Start () {
        //AddControllerScript.GameOverAds();
        if (PlayerPrefs.GetString("GameOver") == "TimeUp")
        {
            mAudioSource1 = this.gameObject.AddComponent<AudioSource>();
            mAudioSource1.clip = gameOverClip;
            mAudioSource1.Play();
            timeUp.SetActive(true);
        }
        if (PlayerPrefs.GetString("GameOver") ==  "LevelSkipped")
        {

            mAudioSource1 = this.gameObject.AddComponent<AudioSource>();
            mAudioSource1.clip = levelCompleteClip;
            mAudioSource1.Play();
            skippedLevelText.text = "You Have Skip Level No " + PlayerPrefs.GetInt("Level");
            levelSkippedDialog.SetActive(true);
            NextButtonSkip.SetActive(true);
        }
        if (PlayerPrefs.GetString("GameOver") == "Crash"){
            mAudioSource1 = this.gameObject.AddComponent<AudioSource>();
            mAudioSource1.clip = gameOverClip;
            //mAudioSource1.loop = true;
            mAudioSource1.Play();
            crash.SetActive(true);
        }
        if (PlayerPrefs.GetString("GameOver") == "LevelComplete")
        {
            if (PlayerPrefs.GetInt("UnlockedLevel") == PlayerPrefs.GetInt("Level"))
            {
                PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel") + 1);
            }
            mAudioSource2 = this.gameObject.AddComponent<AudioSource>();
            mAudioSource2.clip = levelCompleteClip;
            mAudioSource2.Play();

            if (PlayerPrefs.GetInt("RateUs") == 0)
            {
                if (PlayerPrefs.GetInt("Level") == 3 || PlayerPrefs.GetInt("Level") == 6 || PlayerPrefs.GetInt("Level") == 10 || PlayerPrefs.GetInt("Level") == 15 || PlayerPrefs.GetInt("Level") == 20)
                RateUsDialog.GetComponent<Animation>().Play("Dialog Down");
            }

            if (PlayerPrefs.GetInt("Level") < 20)
            {
                levelComplete.SetActive(true);
                nextButton.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level") == 20)
            {
                //Application.LoadLevel("Game Completed");
                levelComplete.SetActive(true);
                nextButton.SetActive(false);
            }
        }
		
		totalEarning = PlayerPrefs.GetInt("TotalEarning") + PlayerPrefs.GetInt("Earning");
		PlayerPrefs.SetInt("TotalEarning",totalEarning);
        timeTxt.text = " " + PlayerPrefs.GetInt("Time") + " Sec";
        earningTxt.text = " $ " + PlayerPrefs.GetInt("Earning");
	}
   

    public void Restart()
    {
        loading.SetActive(true);
        StartCoroutine("wait");
    }
    public void MainMenu()
    {
        loading.SetActive(true);
        SceneManager.LoadScene("Main Menu");
    }
    public void NextButton()
    {
        if (PlayerPrefs.GetInt("Level") < 20)
        {
            loading.SetActive(true);
           
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            StartCoroutine("wait");
        }
    }

    public void FacebookButton()
    {
        RateUsDialog.GetComponent<Animation>().Play("Dialog Up");
        PlayerPrefs.SetInt("RateUs", 1);
        //Application.OpenURL(AdsIDScript.faceBookLink);
    }

    public void RatusButtonClick()
    {
        RateUsDialog.GetComponent<Animation>().Play("Dialog Up");
        PlayerPrefs.SetInt("RateUs", 1);
        //Application.OpenURL(AdsIDScript.RateUSLink);
    }

    public void NoButton()
    {
        RateUsDialog.GetComponent<Animation>().Play("Dialog Up");
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
        StopCoroutine("wait");
    }
}

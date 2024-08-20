using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//using ChartboostSDK;

public class GameOverScript : MonoBehaviour {

	public GameObject crash;
	public GameObject timeUp;
	public GameObject levelComplete;
	public GameObject buttons;
	public GameObject restartButton;
	public GameObject mainMenuButton;
	public GameObject nextButton;
	public GameObject loading;
	public GameObject earningTxt;
	public GameObject totalEarningTxt;
	public GameObject timeTxt;
	public GameObject levelTxt;
	public GameObject dialog;
	public GameObject dialogYesButton;
	public GameObject dialogNoButton;
	public GameObject camera;
	public AudioClip levelCompleteClip;
	public AudioClip gameOverClip;
	private AudioSource mAudioSource1;
	private AudioSource mAudioSource2;
	public int totalEarning;
	private RaycastHit hit;
	private Ray myRay;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	public GameObject close;
	//public AudioClip starBlink;
	//public GameObject loading;
   
	// Use this for initialization
    void Awake()
    {
        //AdmobAd.Instance().LoadInterstitialAd(true);
    }
	void Start () {

        //AddControllerScript.GameOverAds();
		totalEarning = PlayerPrefs.GetInt("TotalEarning") + PlayerPrefs.GetInt("Earning");
		PlayerPrefs.SetInt("TotalEarning",totalEarning);
		levelTxt.transform.GetComponent<TextMesh>().text = "" + PlayerPrefs.GetInt("Level");
		timeTxt.transform.GetComponent<TextMesh>().text = " "+ PlayerPrefs.GetInt("Time") + " Sec";
		earningTxt.transform.GetComponent<TextMesh>().text = " $ "+ PlayerPrefs.GetInt("Earning");
		totalEarningTxt.transform.GetComponent<TextMesh>().text = " $ "+ PlayerPrefs.GetInt("TotalEarning");
        //AdmobAd.Instance().LoadBannerAd(AdmobAd.BannerAdType.Universal_Banner_320x50, AdmobAd.AdLayout.Top_Left);
        //AdmobAd.Instance().ShowBannerAd();
		Time.timeScale = 1.0f;
		star1.SetActive (false);
		star2.SetActive (false);
		star3.SetActive (false);
		if(PlayerPrefs.GetString("GameOver") == "TimeUp")
		{
		
			mAudioSource1 = this.gameObject.AddComponent<AudioSource>();
			mAudioSource1.clip = gameOverClip;
			//mAudioSource1.loop = true;
			mAudioSource1.Play();
			timeUp.SetActive(true);
		}
		if(PlayerPrefs.GetString("GameOver") == "Crash")
		{
		
			mAudioSource1 = this.gameObject.AddComponent<AudioSource>();
			mAudioSource1.clip = gameOverClip;
			//mAudioSource1.loop = true;
			mAudioSource1.Play();
			crash.SetActive(true);
		}

		if (PlayerPrefs.GetString ("GameOver") == "LevelComplete") 
		{

			if (PlayerPrefs.GetInt ("UnlockedLevel") == PlayerPrefs.GetInt ("Level")) 
			{
				PlayerPrefs.SetInt ("UnlockedLevel", PlayerPrefs.GetInt ("UnlockedLevel") + 1);
			}
			//camera.gameObject.GetComponent<AudioSource>().clip = levelCompleteClip;
			mAudioSource2 = this.gameObject.AddComponent<AudioSource> ();
			mAudioSource2.clip = levelCompleteClip;
			//mAudioSource2.loop = true;
			mAudioSource2.Play ();
			if (PlayerPrefs.GetInt ("RateUs") == 0) 
			{
				iTween.MoveTo (dialog, iTween.Hash ("y", 0.0f, "easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
			}
			if (PlayerPrefs.GetInt ("Level") < 20) 
			{
				nextButton.SetActive (true);
				levelComplete.SetActive (true);
			}
			if (PlayerPrefs.GetInt ("Level") == 20) 
			{
				levelComplete.SetActive (true);
				nextButton.SetActive (false);
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (myRay, out hit)) 
		{
			if(Input.GetMouseButtonUp(0))
			{
				buttonFunctions(hit);
			}
		}
		if (PlayerPrefs.GetInt ("star") == 0) 
		{
						star3.SetActive (false);	
						star2.SetActive (false);
						star1.SetActive (false);
//						Debug.Log ("0");
			
		} 
		else if (PlayerPrefs.GetInt ("star") == 1) 
		{
			Debug.Log ("1");
			StartCoroutine ("pause_star");
			PlayerPrefs.SetInt("star",122);
		} 
		else if (PlayerPrefs.GetInt ("star") == 2) 
		{
			Debug.Log ("2");
			StartCoroutine ("pause_star");
			StartCoroutine ("pause_star1");
			PlayerPrefs.SetInt("star",122);
		} 
		else if (PlayerPrefs.GetInt ("star") == 3) 
		{
			Debug.Log ("3");
			StartCoroutine ("pause_star");
			StartCoroutine ("pause_star1");
			StartCoroutine ("pause_star2");
			PlayerPrefs.SetInt("star",122);
		} 
		else 
		{
//			star1.SetActive(false);
//			star2.SetActive(false);
//			star3.SetActive(false);
		}
	}
	
	private void buttonFunctions(RaycastHit mhit)
	{
		if(mhit.collider.gameObject == restartButton.gameObject)
		{
			loading.SetActive(true);
           SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
		}
		if(mhit.collider.gameObject == mainMenuButton.gameObject)
		{
			loading.SetActive(true);
			SceneManager.LoadScene("Main Menu");
		}
		if(mhit.collider.gameObject == nextButton.gameObject)
		{
			if (PlayerPrefs.GetInt("Level") < 20)
			{
				
				loading.SetActive(true);
				PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
				SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
				
			}
		}
		
		if(mhit.collider.gameObject == dialogYesButton.gameObject){
            Application.OpenURL("market://details?id=com.ips.CrazyCityCar.RoofJumping");
			PlayerPrefs.SetInt("RateUs",1);
			iTween.MoveTo(dialog,iTween.Hash("y",-5.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		
		}
		if(mhit.collider.gameObject == dialogNoButton.gameObject){
            Application.OpenURL("https://www.facebook.com/andandroidgame");
			iTween.MoveTo(dialog,iTween.Hash("y",-5.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		if(mhit.collider.gameObject == close.gameObject){
		//	Application.OpenURL("https://www.facebook.com/andandroidgame");
			iTween.MoveTo(dialog,iTween.Hash("y",-5.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
        
	}
	IEnumerator pause_star(){
		yield return new WaitForSeconds(0.8f);
		Debug.Log ("starts");
		star1.SetActive(true);
		//audio.PlayOneShot (starBlink);
		StopCoroutine ("pause_star");
	}
	IEnumerator pause_star1(){
		yield return new WaitForSeconds(1.5f);
		Debug.Log ("starts1");
		star2.SetActive(true);
		//audio.PlayOneShot (starBlink);
		StopCoroutine ("pause_star1");
	}
	IEnumerator pause_star2(){
		yield return new WaitForSeconds(2.0f);
		Debug.Log ("starts2");
		star3.SetActive (true);
		//audio.PlayOneShot (starBlink);
		StopCoroutine ("pause_star2");
	}
	
}
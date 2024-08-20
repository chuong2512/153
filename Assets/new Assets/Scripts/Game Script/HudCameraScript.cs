using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HudCameraScript : MonoBehaviour {
	
	string uniqueUserId = "APPLICATION_USER_ID_HERE"; 
	string appKey = "3f488d89";
	public Camera HudCamera;
    public GameObject skipBtn;
	public GameObject pauseButton;
	public GameObject hornButton;
	public GameObject lightButton;
	public GameObject cameraButton;
	public GameObject startButtonHelp;
	public GameObject startButton;
	public GameObject raceButton;
	public GameObject breakButton;
	public GameObject gearButton;
	public GameObject timeBar;
	public GameObject timeText;
	public GameObject progressBar;
	public GameObject lifesNumber;
	public GameObject objectiveDialog;
	public GameObject objectiveText;
	public GameObject levelStrip;
	public GameObject insideView;
	public GameObject outsideView;
	public GameObject pauseMenu;
	public GameObject pauseMainMenuButton;
	public GameObject pauseRestartButton;
	public GameObject pauseResumeButton;
	public GameObject hudObj;
	public GameObject Truck;
	public GameObject Truck1;
	public GameObject Truck2;
	public GameObject Truck3;
	public GameObject Truck4;
	public GameObject Truck5;
	public GameObject Truck6;
	//	public GameObject TruckLights1;
	//	public GameObject TruckLights2;
	//	public GameObject TruckLights3;
	//	public GameObject TruckLights4;
	public GameObject OutSideCamera;
	public GameObject OutSideCamera1;
	public GameObject InSideCamera1;
	public GameObject InSideCamera2;
	public GameObject InSideCamera3;
	public GameObject InSideCamera4;
	public GameObject helpView;
	public GameObject controls;
	public GameObject hudControls;
	public GameObject Mirror;
	public bool isPause = false; 
	public bool gameOver = false;
	public bool insideCamera = true;
	public int outsideCameraNo = 0;
	public bool  statposition = false;
	public bool endPosition = false;
	private int isListOn;
	public float mytime; 
	public float timeBarValue;
	public float totalTime;
	public int lifes;
	public int earning;
	public float direction;
    public GameObject panel;
	public AudioClip hornClip;
	public AudioClip breakClip;
	public AudioClip busStartClip;
	public AudioClip busSoundClip;
	public AudioClip crashSoundClip;
	public AudioClip lightSoundClip;
	private AudioSource mAudioSource;
	private AudioSource mAudioSource1;
	private AudioSource mAudioSource2;
	public DriveScript driveScript;
	public SmoothFollowCSharp smoothFollowScript;
	private RaycastHit hit;
	private Ray myRay;
	Ray ray;
	public Touch touch;
	private bool consumeLifeFlag;
    public GameObject skiplevel;
	void Awake() 
    {
	}

    public void YesButton()
    {
        //AddControllerScript.ShowChartBoost();
        panel.GetComponent<Animation>().Play("Dialog down");
        panel.transform.parent.gameObject.SetActive(false);
        controls.SetActive(true);
        //isPause = true;
        Time.timeScale = 1;
        driveScript.carRePosition();
    }

    public void NoButton()
    {
        //panel.animation.Play("Dialog down");
        lifes = 0;
    }

   
    // Use this for initialization
	void Start () 
	{
       // GoogleAnalytics.instance.LogScreen(Application.loadedLevelName);
        StartCoroutine(yesbutton());
        
        //AddControllerScript.hudScriptAdd();
		PlayerPrefs.SetInt ("cam", 0);
		setLevelValues();
		totalTime = mytime;
		timeBarValue = 1.0f;
		consumeLifeFlag = true;
		//PlayerPrefs.SetInt("Truck",1);
		//PlayerPrefs.SetInt("Level",0);
		Truck1.SetActive(false);
		Truck2.SetActive(false);
		Truck3.SetActive(false);
		Truck4.SetActive(false);
		Truck5.SetActive(false);
		Truck6.SetActive(false);
		if(PlayerPrefs.GetInt("Truck") == 1)
		{
			Truck1.SetActive(true);
			Truck = Truck1;
		}
		if(PlayerPrefs.GetInt("Truck") == 2){
			Truck2.SetActive(true);
			Truck = Truck2;
		}
		if(PlayerPrefs.GetInt("Truck") == 3){
			Truck3.SetActive(true);
			Truck = Truck3;
		}
		if(PlayerPrefs.GetInt("Truck") == 4){
			Truck4.SetActive(true);
			Truck = Truck4;
		}
		if(PlayerPrefs.GetInt("Truck") == 5){
			Truck5.SetActive(true);
			Truck = Truck5;
		}
		if(PlayerPrefs.GetInt("Truck") == 6){
			Truck6.SetActive(true);
			Truck = Truck6;
		}
		driveScript = (DriveScript) Truck.GetComponent("DriveScript");
		smoothFollowScript = (SmoothFollowCSharp) OutSideCamera.GetComponent("SmoothFollowCSharp");
		objectiveText.transform.GetComponent<TextMesh>().text = " "+ mytime + " Sec";
		// if(PlayerPrefs.GetInt ("selectionOn") == 0){
			// if (PlayerPrefs.GetInt ("Level") == 1) {
				
				// helpView.SetActive (true);
				// controls.SetActive (false);
			// }
		// }
		levelStrip.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load (("2D/LevelSelection/Levels Text/l" + PlayerPrefs.GetInt ("Level")), typeof(Sprite));
		iTween.MoveTo(levelStrip,iTween.Hash("x",-0.0f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f,"delay",1.0f));
		//iTween.MoveTo (levelStrip, iTween.Hash ("y", -8.0f, "easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f, "delay", 2.5f));
		Destroy (levelStrip.gameObject,4.0f);
		startFunction ();
		mAudioSource = Truck.AddComponent<AudioSource>();
		mAudioSource.playOnAwake = false;
		mAudioSource.loop = true;
		PlayerPrefs.SetInt("star", 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//====================Game Time Calculation==========
		gameCalculation();
		int myTouchCount = 0;
		//====================Multi Touch====================
		for (int i = 0; i < Input.touchCount; i++ ) 
		{
			touch = Input.GetTouch(i);
			ray = HudCamera.ScreenPointToRay(touch.position);
			if( Physics.Raycast(ray,out hit))
			{
				if(touch.phase == TouchPhase.Began)
				{
					myTouchCount = i;
					touchBegainFunctions(hit);
				}
				if(touch.phase == TouchPhase.Ended && i == myTouchCount)
				{
					touchEndFunctions(hit);
				}
			}
		}
		//===========================================================================================
		if(PlayerPrefs.GetInt ("selection") == 1)
		{
			if (PlayerPrefs.GetInt ("Level") == 1) 
			{
				helpView.SetActive (true);
				controls.SetActive (false);
				PlayerPrefs.SetInt ("selection", 2);
			}
		}
		
		myRay = HudCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (myRay, out hit)) 
		{
			if(Input.GetMouseButtonUp(0) == true && isPause == false)
			{
				BtnFunctions(hit);
				stopHorn();
			}
			if(Input.GetMouseButtonDown(0) == true && isPause == false)
			{
				if(hit.collider.gameObject == hornButton.gameObject)
				{
					playHorn();
				}
			}
		}
		timeBar.transform.localScale = new Vector3(timeBarValue,1.0f,1.0f);
		lifesNumber.transform.GetComponent<TextMesh>().text = " x "+ lifes;
		timeText.transform.GetComponent<TextMesh>().text = " " +  (int)mytime;
		//=================================================================
		//		if(Input.GetKey(KeyCode.Escape)){
		//			pauseFunction();
		//		}
		
		//mySpeed = (int) busScript.power;
		//mySpeed = (int) busScript.busSpeed;
		//if(mySpeed >= 0 && mySpeed < 100){
		//	speed.transform.GetComponent<TextMesh>().text = ""+ mySpeed;	
		//}
		//=============================================================================================
		
	}

	//===============Touch Begain Function===============
	private void touchBegainFunctions(RaycastHit mhit)
	{
		if(mhit.collider.gameObject == raceButton.gameObject)
		{
			if(driveScript.engineStart == 0.0f)
			{
				startButtonHelp.SetActive(true);
			}
			else
			{
				startButtonHelp.SetActive(false);
			}
			raceButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/Race1",typeof(Sprite));
			driveScript.accelerator = direction;
			driveScript.raceOn = true;
		}
		if(mhit.collider.gameObject == breakButton.gameObject)
		{
			breakButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/Break1",typeof(Sprite));
			playBreakSound();
			driveScript.applyBreak();
		}
	}
	
	//=============Touch End Function====================
	private void touchEndFunctions(RaycastHit mhit)
	{
		if(hit.collider.gameObject == gearButton.gameObject)
		{
			if(direction == 0.0f)
			{
				gearButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/Gear1",typeof(Sprite));
				direction = 1.0f;
			}
			else if(direction == 1.0f)
			{
				gearButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/Gear3",typeof(Sprite));
				direction = -1.0f;
			}
			else if(direction == -1.0f)
			{
				gearButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/Gear2",typeof(Sprite));
				direction = 0.0f;
			}
		}
		raceButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/Race",typeof(Sprite));
		breakButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/Break",typeof(Sprite));
		driveScript.accelerator = 0.0f;
		driveScript.raceOn = false;
		driveScript.releaseBreak();
	}

	//=================== Button Functions ======================
	private void BtnFunctions(RaycastHit mhit)
	{
		if(mhit.collider.gameObject == helpView.gameObject)
		{
			helpView.SetActive(false);
			controls.SetActive(true);
		}
		if(mhit.collider.gameObject == pauseButton.gameObject)
		{
            
			PlayerPrefs.SetInt ("pausetouch", 1);
			pauseFunction();
		}
		if(mhit.collider.gameObject == cameraButton.gameObject)
		{
			//switchOutsideCameraFunction();
			switchCameraFunction();
		}
		if(mhit.collider.gameObject == startButton.gameObject)
		{
			startFunction();
		}
		if(mhit.collider.gameObject == lightButton.gameObject)
		{
			lightFunction();	
		}
		//		if(mhit.collider.gameObject == pauseMainMenuButton.gameObject){
		//			Debug.Log("Go To Main Menu");
		//			Application.LoadLevel("Main Menu");
		//		}
		//		if(mhit.collider.gameObject == pauseRestartButton.gameObject){
		//			Application.LoadLevel("Level" + PlayerPrefs.GetInt("Level"));
		//		}
		//		if(mhit.collider.gameObject == pauseResumeButton.gameObject){
		//			resumeFunction();
		//		}
	}
    public void SkipBtn()
    {
        Time.timeScale = 1;
        print("skip");
        skipBtn.SetActive(false);
        PlayerPrefs.SetInt("selectionOn", 0);
        PlayerPrefs.SetInt("cam", 0);
        PlayerPrefs.SetInt("selection", 1);
        smoothFollowScript.StopCoroutine("fireView");
    }
	//=================== Game Play Calculation Function ========
	public void gameCalculation()
    {
		
		if(!isPause)
        {
			mytime = mytime - Time.deltaTime;
			if(!gameOver)
            {
				timeBarValue = (mytime - Time.deltaTime)/totalTime;
			}
		}
		
		if(statposition == true && endPosition == true && gameOver == false){
			//CBBinding.StopAdMob();
			PlayerPrefs.SetInt("Earning",earning);
			PlayerPrefs.SetInt("Time",(int) mytime);
			PlayerPrefs.SetString("GameOver","LevelComplete");
			SceneManager.LoadScene("Game Over");
		}
		
		if(timeBarValue <= 0){
			gameOver = true;
			//CBBinding.StopAdMob();
			PlayerPrefs.SetInt("Earning",0);
			PlayerPrefs.SetInt("Time",0);
			PlayerPrefs.SetString("GameOver","TimeUp");
			SceneManager.LoadScene("Game Over");
		}
		
		if(lifes <= 0){
			
			gameOver = true;
			//CBBinding.StopAdMob();
			PlayerPrefs.SetInt("Earning",0);
			PlayerPrefs.SetInt("Time",0);
			PlayerPrefs.SetString("GameOver","Crash");
			PlayerPrefs.SetInt("star", 0);
			SceneManager.LoadScene("Game Over");
			
		}
        if (timeBarValue >= .9f)
        {
            earning = 150;
        }
		if(timeBarValue <= 0.9f && timeBarValue >= 0.5f){
			earning = 150;
			if(lifes > 0){
				PlayerPrefs.SetInt("star", 3);
			}
			
		}
		else if (timeBarValue <= 0.5f && timeBarValue >= 0.3f) {
			earning = 120;
			if (lifes > 0){
				PlayerPrefs.SetInt ("star", 2);
			}
		}
        else if (timeBarValue <= 0.3f && timeBarValue > 0.0f)
        {
			earning = 90;
			if(lifes > 0)
			{
				PlayerPrefs.SetInt("star", 1);
			}
		}
        else if (timeBarValue <= 0.0f)
		{
			PlayerPrefs.SetInt("star", 0);	
		}
		
		
		if(Truck.GetComponent<AudioSource>().pitch <= 2.0f && driveScript.raceOn == true){  
			Truck.GetComponent<AudioSource>().pitch += 0.01f;
		}	
		if(Truck.GetComponent<AudioSource>().pitch > 1.0f && driveScript.raceOn == false){
			Truck.GetComponent<AudioSource>().pitch -= 0.01f;
		}
	}
	
	
	//========================== Sound Function ======================
	
	public void playHorn(){
		
		mAudioSource.clip = hornClip;
		mAudioSource.loop = true;
		mAudioSource.Play();
		//mAudioSource.PlayOneShot(hornClip);
		
		//Truck.audio.PlayOneShot(hornClip);
	}
	
	public void stopHorn(){
		
		mAudioSource.Stop();
	}
	
	public void playCrashSound(){
		mAudioSource1 = Truck.AddComponent<AudioSource>();
		//mAudioSource1.clip = hornClip;
		mAudioSource1.PlayOneShot(crashSoundClip);
	}	
	
	
	public void playLightSound(){
		mAudioSource2 = Truck.AddComponent<AudioSource>();
		//mAudioSource2.clip = hornClip;
		mAudioSource2.PlayOneShot(lightSoundClip);
	}
	
	
	
	public void playBusStart(){
		Truck.GetComponent<AudioSource>().PlayOneShot(busStartClip);
	}
	
	public void playBreakSound(){
		//Truck.audio.PlayOneShot(breakClip);
	}
	
	public void playBusSound(){
		Truck.GetComponent<AudioSource>().Play();
	}
	
	public void stopBusSound(){
		Truck.GetComponent<AudioSource>().Stop();
	}
	
	//private Color previousColor;
	public void gameOverFunction(){
		gameOver = true;
		PlayerPrefs.SetInt("Earning",0);
		PlayerPrefs.SetInt("Time",0);
		PlayerPrefs.SetString("GameOver","Crash");
		SceneManager.LoadScene("Game Over");
	}
	
	
	
	public void consumeLife(){
		if(consumeLifeFlag == true){
			playCrashSound();
			lifes--;
			consumeLifeFlag = false;
			//lifesNumber.transform.GetComponent<TextMesh>().renderer.material.color = Color.red;
			iTween.ScaleTo(lifesNumber,iTween.Hash("x",0.2f,"y",0.2f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 0.3f));
			iTween.ScaleTo(lifesNumber,iTween.Hash("x",0.1f,"y",0.1f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 0.3f,"delay",0.3f));
			iTween.ScaleTo(lifesNumber,iTween.Hash("x",0.2f,"y",0.2f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 0.3f,"delay",0.6f));
			iTween.ScaleTo(lifesNumber,iTween.Hash("x",0.1f,"y",0.1f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 0.3f,"delay",0.9f,"oncomplete","setConsumeLifeFlag", "oncompletetarget",transform.gameObject));
		}
	}
	
	public void setConsumeLifeFlag(){
		//lifesNumber.transform.GetComponent<TextMesh>().color = previousColor;
		consumeLifeFlag = true;
	}
	
	//=====================================================
	
	private void startFunction(){
		if(driveScript.engineStart == 0){
			startButtonHelp.SetActive(false);
			startButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/EngineOn",typeof(Sprite));
			playBusStart();
			playBusSound();
			driveScript.engineStart = 1.0f;
		}
	}
	
	
	public void pauseFunction(){
        //AddControllerScript.ShowChartBoost();
        if (skiplevel)
        {
            skiplevel.SetActive(false);
        }
		PlayerPrefs.SetInt ("hornStart",0);
		Time.timeScale = 0;
		isPause = true;
		driveScript.applyBreak();
		hudObj.SetActive(false);
		pauseMenu.SetActive(true);
	}
	
	public void resumeFunction(){
        if (skiplevel)
        {
            skiplevel.SetActive(true);
        }
		PlayerPrefs.SetInt ("pausetouch", 0);
		isPause = false;
		driveScript.releaseBreak();
		hudObj.SetActive(true);
		pauseMenu.SetActive(false);
        Time.timeScale = 1;
	}
	
	
	public void switchOutsideCameraFunction(){
		switch(outsideCameraNo){
		case 0:
			OutSideCamera.SetActive(false);
			OutSideCamera1.SetActive(true);
			outsideCameraNo = 1; 
			break;
		case 1:
			OutSideCamera.SetActive(true);
			OutSideCamera1.SetActive(false);
			outsideCameraNo = 0;
			break;
		}
	}
	
	public void switchCameraFunction(){
		//		if(insideCamera == true){
		//			OutSideCamera.SetActive(true);
		//			InSideCamera1.SetActive(false);
		//			InSideCamera2.SetActive(false);
		//			InSideCamera3.SetActive(false);
		//			InSideCamera4.SetActive(false);
		//			insideView.SetActive(false);
		//			outsideView.SetActive(true);
		//			insideCamera = false;
		//		}else{
		//			OutSideCamera.SetActive(false);
		//			if(PlayerPrefs.GetInt("Truck") == 1){
		//				InSideCamera1.SetActive(true);
		//			}
		//			if(PlayerPrefs.GetInt("Truck") == 2){
		//				InSideCamera2.SetActive(true);
		//			}
		//			if(PlayerPrefs.GetInt("Truck") == 3){
		//				InSideCamera3.SetActive(true);
		//			}
		//			if(PlayerPrefs.GetInt("Truck") == 4){
		//				InSideCamera4.SetActive(true);
		//			}
		//			outsideView.SetActive(false);
		//			insideView.SetActive(true);
		//			insideCamera = true;
		//		}
		Debug.Log ("cam touched");
		if (insideCamera == true) 
		{
			if (PlayerPrefs.GetInt ("cam") == 0) 
			{
				PlayerPrefs.SetInt ("cam",1);
			}
			else if (PlayerPrefs.GetInt ("cam") == 1) 
			{
				PlayerPrefs.SetInt ("cam",2);
			}
			else if (PlayerPrefs.GetInt ("cam") == 2) 
			{
				PlayerPrefs.SetInt ("cam",3);
			}
			else if (PlayerPrefs.GetInt ("cam") == 3) 
			{
				PlayerPrefs.SetInt ("cam",0);
			}
//									else if (PlayerPrefs.GetInt ("cam") == 4) {
//											PlayerPrefs.SetInt ("cam",0);
//									}
		}
		//			if(insideCamera == true){
		//			//smoothFollowScript.height = 10.0f;
		//			PlayerPrefs.SetInt("cam",0);
		//
		//			insideCamera = false;
		//		}else{
		////			smoothFollowScript.height = 5.5f;
		////			insideCamera = true;
		//			PlayerPrefs.SetInt("cam",1);
		//		}
		//		if(insideCamera == true){
		//			OutSideCamera.SetActive(true);
		//			InSideCamera1.SetActive(false);
		//			InSideCamera2.SetActive(false);
		//			InSideCamera3.SetActive(false);
		//			InSideCamera4.SetActive(false);
		//			insideView.SetActive(false);
		//			outsideView.SetActive(true);
		//			insideCamera = false;
		//		}else
		//	{
		//			OutSideCamera.SetActive(false);
		//			if(PlayerPrefs.GetInt("Truck") == 1){
		//				InSideCamera1.SetActive(true);
		//			}
		//			if(PlayerPrefs.GetInt("Truck") == 2){
		//				InSideCamera2.SetActive(true);
		//			}
		//			if(PlayerPrefs.GetInt("Truck") == 3){
		//				InSideCamera3.SetActive(true);
		//			}
		//			if(PlayerPrefs.GetInt("Truck") == 4){
		//				InSideCamera4.SetActive(true);
		//			}
		//			outsideView.SetActive(false);
		//			insideView.SetActive(true);
		//			insideCamera = true;
		//		}
	}
	
	
	public void lightFunction()
	{
		playLightSound();
		if(isListOn == 0)
		{
			//			lightButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/HeadlightOn",typeof(Sprite));
			//			if(PlayerPrefs.GetInt("Truck") == 1){
			//				TruckLights1.SetActive(true);
			//			}
			//			if(PlayerPrefs.GetInt("Truck") == 2){
			//				TruckLights2.SetActive(true);
			//			}
			//			if(PlayerPrefs.GetInt("Truck") == 3){
			//				TruckLights3.SetActive(true);
			//			}
			//			if(PlayerPrefs.GetInt("Truck") == 4){
			//				TruckLights4.SetActive(true);
			//			}
			isListOn = 1;
		}
		else
		{
			//			lightButton.GetComponent<SpriteRenderer>().sprite = (Sprite) Resources.Load("2D/GamePlay/HeadlightOff",typeof(Sprite));
			//			TruckLights1.SetActive(false);
			//			TruckLights2.SetActive(false);
			//			TruckLights3.SetActive(false);
			//			TruckLights4.SetActive(false);
			isListOn = 0;
		}
	}
	
	
	public void showObjectiveBar()
	{
		iTween.MoveTo(objectiveDialog,iTween.Hash("y",(objectiveDialog.transform.position.y+5.0f),"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f,"oncomplete","hideObjectiveBar", "oncompletetarget",transform.gameObject));
	}
	
	public void hideObjectiveBar()
	{
		//endPosition = true;
		iTween.MoveTo(objectiveDialog,iTween.Hash("y",(objectiveDialog.transform.position.y-5.0f),"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f,"delay",1.0f));
		
	}
    IEnumerator yesbutton()
    {
        yield return new WaitForSeconds(7f);
        if (PlayerPrefs.GetInt("Level") != 20)
        {
            Instantiate(Resources.Load("Skip Level GamePlay"));
            skiplevel = GameObject.Find("Skip Level Button");
        }
        
        StopCoroutine(yesbutton());
    }
	public void showProgressBar(){
		progressBar.SetActive(true);
		progressBar.GetComponent<Animation>().Play ("ShowProgress");
	}
	
	
	//============================================================
	private void setLevelValues(){
		switch(PlayerPrefs.GetInt("Level")){
		case 0:
			mytime = 300.0f;
			lifes = 10;
			break;
		case 1:
			mytime = 200.0f;
			lifes = 1;
			break;
		case 2:
			mytime = 200.0f;
			lifes = 1;
			break;
		case 3:
			mytime = 200.0f;
			lifes = 1;
			break;
		case 4:
			mytime = 200.0f;
			lifes = 1;
			break;
		case 5:
			mytime = 230.0f;
			lifes = 1;
			break;
		case 6:
			mytime = 230.0f;
			lifes = 1;
			break;
		case 7:
			mytime = 230.0f;
			lifes = 1;
			break;
		case 8:
			mytime = 230.0f;
			lifes = 1;
			break;
		case 9:
			mytime = 260.0f;
			lifes = 1;
			break;
		case 10:
			mytime = 260.0f;
			lifes = 1;
			break;
		case 11:
			mytime = 260.0f;
			lifes = 1;
			break;
		case 12:
			mytime = 260.0f;
			lifes = 1;
			break;
		case 13:
			mytime = 290.0f;
			lifes = 1;
			break;
		case 14:
			mytime = 290.0f;
			lifes = 1;
			break;
		case 15:
			mytime = 290.0f;
			lifes = 1;
			break;
		case 16:
			mytime = 290.0f;
			lifes = 1;
			break;
		case 17:
			mytime = 330.0f;
			lifes = 1;
			break;
		case 18:
			mytime = 330.0f;
			lifes = 1;
			break;
		case 19:
			mytime = 330.0f;
			lifes = 1;
			break;
		case 20:
			mytime = 330.0f;
			lifes = 1;
			break;
			//		case 21:
			//			mytime = 130.0f;
			//			lifes = 2;
			//			break;
			//		case 22:
			//			mytime = 130.0f;
			//			lifes = 2;
			//			break;
			//		case 23:
			//			mytime = 130.0f;
			//			lifes = 2;
			//			break;
			//		case 24:
			//			mytime = 130.0f;
			//			lifes = 2;
			//			break;
			//		case 25:
			//			mytime = 130.0f;
			//			lifes = 2;
			//			break;
			//		case 26:
			//			mytime = 140.0f;
			//			lifes = 1;
			//			break;
			//		case 27:
			//			mytime = 140.0f;
			//			lifes = 1;
			//			break;
			//		case 28:
			//			mytime = 140.0f;
			//			lifes = 1;
			//			break;
			//		case 29:
			//			mytime = 140.0f;
			//			lifes = 1;
			//			break;
			//		case 30:
			//			mytime = 140.0f;
			//			lifes = 1;
			//			break;
		}
		
	}
	
}

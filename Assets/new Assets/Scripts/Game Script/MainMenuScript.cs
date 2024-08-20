using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {



	public Camera HudCamera;

	public GameObject playButton;
	public GameObject moreappsButton;
	public GameObject feedbackButton;
	public GameObject RateUs;
	public GameObject soundOn;
	public GameObject soundOff;
	public GameObject exitDialog;
	public GameObject exitDialogYesButton;
	public GameObject exitDialogNoButton;
	private RaycastHit hit;
	private Ray myRay;
    public GameObject loading;

    

	void Awake() 
    {
	}
	
	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //AddControllerScript.MainMenuAdds();
		if (AudioListener.volume == 1) 
		{
			soundOn.SetActive(true);
			soundOff.SetActive(false);
		}
		if (AudioListener.volume == 0) 
		{
			soundOff.SetActive(true);
			soundOn.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		myRay = HudCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(myRay, out hit)){
			if(Input.GetMouseButtonUp(0)){
				buttonFunctions(hit);
			}
		}
		if(Input.GetKey(KeyCode.Escape)){
			iTween.MoveTo(exitDialog,iTween.Hash("y",0.0f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
	}
	
	private void buttonFunctions(RaycastHit mhit){
		if(mhit.collider.gameObject == playButton.gameObject)
        {
            loading.SetActive(true);
            StartCoroutine("wait");
            //Application.LoadLevel("Truck Selection");
		}
		if(mhit.collider.gameObject == moreappsButton.gameObject){
            //Application.OpenURL(AdsIDScript.MoreApps);
		}
		if(mhit.collider.gameObject == feedbackButton.gameObject){
			SendFeedbackMail();
		}
		if(mhit.collider.gameObject == soundOn.gameObject){
			AudioListener.volume=0;
			soundOn.SetActive(false);
			soundOff.SetActive(true);
		}
		if(mhit.collider.gameObject == soundOff.gameObject){
			AudioListener.volume=1;
			soundOn.SetActive(true);
			soundOff.SetActive(false);
		}
		if(mhit.collider.gameObject == RateUs.gameObject){
			Application.OpenURL("");
			
		}
		if(mhit.collider.gameObject == exitDialogYesButton.gameObject){
			if(Application.platform == RuntimePlatform.Android)
			{ 
				#if UNITY_ANDROID
				//StartApp.StartAppWrapper.showAd(true);
				#endif
				Application.Quit();
			}else{
				Application.Quit();
			}
		}
		if(mhit.collider.gameObject == exitDialogNoButton.gameObject){
			iTween.MoveTo(exitDialog,iTween.Hash("y",-6.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
	}
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Truck Selection");
        AdsHandler.Instance.LoadAd();
        StopCoroutine("wait");
    }
	public void SendFeedbackMail(){
		#if UNITY_ANDROID
		AndroidJavaObject emailObject;
		
		AndroidJavaClass emailClass = new AndroidJavaClass( "com.oas.emailcompose.EmailActivity" )  ;
		emailObject = emailClass.CallStatic<AndroidJavaObject>("instance");
		emailObject.Call( "sendEmail" , "Real Free Car Parking Game" ,"com.gls.realfreecarparkinggame"," 1, 1.0 ","admin@i6.com"  );
		#elif UNITY_IPHONE
		
		sendMail("Counter Air Attack 3D"); 
		
		#endif
	}
}

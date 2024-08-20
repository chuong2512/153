using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

	public Camera HudCamera;
	HudCameraScript hudScript;

    public GameObject Loading;
	public GameObject mainMenuButton;
	public GameObject restartButton;
	public GameObject resumeButton;
	
	private RaycastHit hit;
	private Ray myRay;
	
	
	// Use this for initialization
	void Start () {
		hudScript = (HudCameraScript) HudCamera.GetComponent("HudCameraScript");
	}
	
	// Update is called once per frame
	void Update () {
		myRay = HudCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(myRay, out hit)){
			if(Input.GetMouseButtonDown(0)){
				buttonFunctions(hit);
			}
		}
	}
	
	private void buttonFunctions(RaycastHit mhit){
		if(mhit.collider.gameObject == mainMenuButton.gameObject){
			Time.timeScale=1;
			SceneManager.LoadScene("Main Menu");
		}
		if(mhit.collider.gameObject == restartButton.gameObject)
        {
			Time.timeScale=1;
            Loading.SetActive(true);
            StartCoroutine("wait");
		}
		if(mhit.collider.gameObject == resumeButton.gameObject){
			Time.timeScale=1;
			hudScript.resumeFunction();
		}
	}
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
        AdsHandler.Instance.LoadAd();
        StopCoroutine("wait");
    }
}

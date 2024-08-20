using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class VehicleSelection : MonoBehaviour {

	public Camera HudCamera;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject backButton;
	public GameObject selectButton;
	public GameObject[] List;
	public int selectedBus = 0;
	public AudioClip selectSound;
	public AudioClip clickSound;
	private RaycastHit hit;
	private Ray myRay;

	// Use this for initialization
	void Start () {
		//LeadBoltAds.initialize();
		//LeadBoltAds.LoadInterstitial();
		//LeadBoltAds.LoadAlert();
		
	}
	
	// Update is called once per frame
	void Update () {
		myRay = HudCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(myRay, out hit)){
			if(Input.GetMouseButtonUp(0) == true){
				buttonFunctions(hit);
			}
		}
		
		setLeftRightButtons(selectedBus,0,List.Length-1);
		
		if(Input.GetKey(KeyCode.Escape)){
			SceneManager.LoadScene("Mode Selection");
		}
	}
	
	private void buttonFunctions(RaycastHit mhit){
		if(mhit.collider.gameObject == backButton.gameObject){
			SceneManager.LoadScene("Mode Selection");
		}
		if(mhit.collider.gameObject == selectButton.gameObject){
			//HudCamera.audio.PlayOneShot(selectSound);
			if(PlayerPrefs.GetInt("Level") == 0){
				PlayerPrefs.SetInt("Truck",selectedBus + 1);
				SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
			}else{
				PlayerPrefs.SetInt("Truck",selectedBus + 1);
				SceneManager.LoadScene("Level Selection");
			}
			
		}
		if(mhit.collider.gameObject == leftButton.gameObject){
			//HudCamera.audio.PlayOneShot(clickSound);
			selectedBus--;
			setAllGameObjectFalse(List);
			setGameObjectActive(List,selectedBus);
		}
		if(mhit.collider.gameObject == rightButton.gameObject){
			//HudCamera.audio.PlayOneShot(clickSound);
			selectedBus++;
			setAllGameObjectFalse(List);
			setGameObjectActive(List,selectedBus);
		}
		
	
	}
	
	private void setLeftRightButtons(int number,int min,int max){
		if(number != min && number != max){
			rightButton.SetActive(true);
			leftButton.SetActive(true);
		}
		if(number == min){
			leftButton.SetActive(false);
			rightButton.SetActive(true);
		}
		if(number == max){
			rightButton.SetActive(false);
			leftButton.SetActive(true);
		}
	}
	
	

	
	private void setAllGameObjectFalse(GameObject[] mlist){
		for(int i = 0; i < mlist.Length; i++){
			mlist[i].gameObject.SetActive(false);
		}
	}
	
	private void setGameObjectActive(GameObject[] mlist,int n){
		mlist[n].gameObject.SetActive(true);
	}
	
	
	
}

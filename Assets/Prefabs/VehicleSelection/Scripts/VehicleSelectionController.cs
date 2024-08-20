using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VehicleSelectionController : MonoBehaviour
{

	public GameObject leftButton;
	public GameObject rightButton;
    public Text totalEarningText;
	public GameObject lockedImage;
	private bool isLocked;
    public GameObject loading;
	public GameObject[] List;
	public int Vehicle2Price = 0;
	public int Vehicle3Price = 0;
	public int Vehicle4Price = 0;

	public int selectedVehicle = 0;
    public Animator unlockDialogAnimator;
    public Animator insufficientCashDialogAnimator;


	// Use this for initialization
	void Start () {

    
		
		isLocked = false;
        totalEarningText.text = " $ " + PlayerPrefs.GetInt("TotalEarning") + "";
        PlayerPrefs.SetInt("Vehicle1", 1);
        //PlayerPrefs.SetInt("Vehicle2", 1);
        //PlayerPrefs.SetInt("Vehicle3", 1);
//		PlayerPrefs.SetInt("Vehicle4",1);
	}
	
	// Update is called once per frame
	void Update () {	
		
		setLeftRightButtons(selectedVehicle,0,List.Length-1);
		setLocks();
        totalEarningText.text = " $ " + PlayerPrefs.GetInt("TotalEarning") + "";

	}

    public void LeftButton()
    {
        selectedVehicle--;
        setAllGameObjectFalse(List);
        setGameObjectActive(List, selectedVehicle);
    }
	
    public void RightButton()
    {
        selectedVehicle++;
		setAllGameObjectFalse(List);
		setGameObjectActive(List,selectedVehicle);
		
    }

    
    public void UnLockedDialogYesButton()
    {
        unlockFunction();
    }

    public void UnLockedDialogNoButton()
    {
        unlockDialogAnimator.SetBool("IsDialogDown", true);
    }

    public void InSufcientCashDialogOkButton()
    {
        insufficientCashDialogAnimator.SetBool("IsDialogDown",true);
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

    public void SelectBUtton()
    {
        if (isLocked == true)
        {
            showDialogs();
        }
        else
        {
            PlayerPrefs.SetInt("Vehicle", selectedVehicle + 1);
            Debug.Log("Vehicle = " + PlayerPrefs.GetInt("Vehicle"));
            loading.SetActive(true);
            SceneManager.LoadScene("Level Selection");
        }
    }

	private void showDialogs(){
			if(selectedVehicle == 1 && PlayerPrefs.GetInt("TotalEarning") >= Vehicle2Price && PlayerPrefs.GetInt("Vehicle2") == 0){
                Debug.Log("Condition 1");
                unlockDialogAnimator.gameObject.SetActive(true);
                unlockDialogAnimator.SetBool("IsDialogDown", false);
              
			}else if(selectedVehicle == 2 && PlayerPrefs.GetInt("TotalEarning") >= Vehicle3Price && PlayerPrefs.GetInt("Vehicle3") == 0){
                Debug.Log("Condition 2");
                unlockDialogAnimator.gameObject.SetActive(true);
                unlockDialogAnimator.SetBool("IsDialogDown", false);
			}else if(selectedVehicle == 3 && PlayerPrefs.GetInt("TotalEarning") >= Vehicle4Price && PlayerPrefs.GetInt("Vehicle4") == 0){
                Debug.Log("Condition 3");
                unlockDialogAnimator.gameObject.SetActive(true);
                unlockDialogAnimator.SetBool("IsDialogDown", false);
			}else
            {
                Debug.Log("else");
                insufficientCashDialogAnimator.gameObject.SetActive(true);
                insufficientCashDialogAnimator.SetBool("IsDialogDown", false);
			}
	}
    public void LoadLevel(string LevelName)
    {
	    SceneManager.LoadScene(LevelName);
    }
	private void unlockFunction(){
		if(selectedVehicle == 1 && PlayerPrefs.GetInt("TotalEarning") >= Vehicle2Price){
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Vehicle2Price);
			PlayerPrefs.SetInt("Vehicle2",1);
			isLocked = true;
		}
		if(selectedVehicle == 2 && PlayerPrefs.GetInt("TotalEarning") >= Vehicle3Price){
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Vehicle3Price);
			PlayerPrefs.SetInt("Vehicle3",1);
			isLocked = true;
		}
		if(selectedVehicle == 3 && PlayerPrefs.GetInt("TotalEarning") >= Vehicle4Price){
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Vehicle4Price);
			PlayerPrefs.SetInt("Vehicle4",1);
			isLocked = true;
		}
	}
	
	private void setLocks(){
		lockedImage.SetActive(false);
		isLocked = false;
		if(selectedVehicle == 0 && PlayerPrefs.GetInt("Vehicle1") == 0){
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedVehicle == 1 && PlayerPrefs.GetInt("Vehicle2") == 0){
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedVehicle == 2 && PlayerPrefs.GetInt("Vehicle3") == 0){
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedVehicle == 3 && PlayerPrefs.GetInt("Vehicle4") == 0){
			lockedImage.SetActive(true);
			isLocked = true;
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

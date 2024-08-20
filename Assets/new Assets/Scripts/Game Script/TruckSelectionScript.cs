using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TruckSelectionScript : MonoBehaviour {

	public Camera HudCamera;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject backButton;
	public GameObject selectButton;
	public Text totalEarningTxt;
	public GameObject lockedImage;
	public GameObject cashDialog;
	public GameObject cashDialogButton;
	public GameObject unlockDialog;
	public GameObject unlockDialogYesButton;
	public GameObject unlockDialogNoButton;
	private bool isLocked;
	public GameObject[] List;
	public int Truck2Price = 0;
	public int Truck3Price = 0;
	public int Truck4Price = 0;
	public int Truck5Price = 0;
	public int Truck6Price = 0;
	public int selectedBus = 0;
	public AudioClip selectSound;
	public AudioClip clickSound;
	private RaycastHit hit;
	private Ray myRay;
	private RaycastHit hit2;
	private Ray myRay2;
	public GameObject freeCashDialogYes;
	public GameObject cross;
	public GameObject freeCashDialog;
	public GameObject freeCashButton;

    void Awake()
    {
    }
	// Use this for initialization
	void Start () 
	{

        //AddControllerScript.VehicleSelectionAds();
		isLocked = false;
        totalEarningTxt.text = " $ " + PlayerPrefs.GetInt("TotalEarning") + "";
		PlayerPrefs.SetInt("Truck1",1);
        //PlayerPrefs.SetInt("Truck2", 0);
        //PlayerPrefs.SetInt("Truck3", 0);
        //PlayerPrefs.SetInt("Truck4", 0);
        //PlayerPrefs.SetInt("Truck5", 0);
        //PlayerPrefs.SetInt("Truck6", 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
        totalEarningTxt.text = " $ " + PlayerPrefs.GetInt("TotalEarning") + "";
		myRay = HudCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(myRay, out hit))
		{
			if(Input.GetMouseButtonUp(0) == true)
			{
				buttonFunctions(hit);
			}
		}
		myRay2 = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(myRay2, out hit2))
		{
			if(Input.GetMouseButtonUp(0) == true)
			{
				hit2Functions(hit2);
			}
		}
		setLeftRightButtons(selectedBus,0,List.Length-1);
		setLocks();
//		if(Input.GetKey(KeyCode.Escape)){
//			Application.LoadLevel("Mode Selection");
//		}
	}
	
	private void buttonFunctions(RaycastHit mhit)
	{
		if(mhit.collider.gameObject == backButton.gameObject)
		{
			
		}

		if(mhit.collider.gameObject == selectButton.gameObject)
		{
			//HudCamera.audio.PlayOneShot(selectSound);
		
		}
		if(mhit.collider.gameObject == leftButton.gameObject)
		{
			//HudCamera.audio.PlayOneShot(clickSound);
			
		}
		if(mhit.collider.gameObject == rightButton.gameObject)
		{
			//HudCamera.audio.PlayOneShot(clickSound);
			
		}
	}

    public void backButtonClick()
    {
	    SceneManager.LoadScene("Main Menu");
    }

    public void SelectButtonClick()
    {
        if (isLocked == true)
        {
            showDialogs();
        }
        else
        {
            PlayerPrefs.SetInt("Truck", selectedBus + 1);
            SceneManager.LoadScene("Level Selection");
        }
    }

    public void leftButtonClick()
    {
        selectedBus--;
        setAllGameObjectFalse(List);
        setGameObjectActive(List, selectedBus);
    }

    public void RightButton()
    {
        selectedBus++;
        setAllGameObjectFalse(List);
        setGameObjectActive(List, selectedBus);
    }

	private void hit2Functions(RaycastHit mhit)
	{
		if(mhit.collider.gameObject == freeCashButton.gameObject)
		{
			freeCashDialog.SetActive (true);
		}
		if(mhit.collider.gameObject == freeCashDialogYes.gameObject)
		{
			freeCashDialog.SetActive (false);
//			showAd();

		}
		if(mhit.collider.gameObject == cross.gameObject)
		{
			freeCashDialog.SetActive (false);
		}
		if(mhit.collider.gameObject == cashDialogButton.gameObject)
		{
			iTween.MoveTo(cashDialog,iTween.Hash("y",-8.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		if(mhit.collider.gameObject == unlockDialogYesButton.gameObject)
		{
			//UnityAdsHelper.ShowAd();
			
			iTween.MoveTo(unlockDialog,iTween.Hash("y",-8.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		if(mhit.collider.gameObject == unlockDialogNoButton.gameObject)
		{
			//UnityAdsHelper.ShowAd();
			iTween.MoveTo(unlockDialog,iTween.Hash("y",-8.0f,"easeType", iTween.EaseType.easeInBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
	}

    public void UnlockButtonYes()
    {
        unlockFunction();
        totalEarningTxt.text = " " + PlayerPrefs.GetInt("TotalEarning") + " $";
        unlockDialog.GetComponent<Animation>().Play("Dialog down");
    }
    public void unLockedDialogNobutton()
     {
            unlockDialog.GetComponent<Animation>().Play("Dialog down");
    }
	private void setLeftRightButtons(int number,int min,int max)
	{
		if(number != min && number != max)
		{
			rightButton.SetActive(true);
			leftButton.SetActive(true);
		}
		if(number == min)
		{
			leftButton.SetActive(false);
			rightButton.SetActive(true);
		}
		if(number == max)
		{
			rightButton.SetActive(false);
			leftButton.SetActive(true);
		}
	}
	
	
	private void showDialogs()
	{
		if(selectedBus == 1 && PlayerPrefs.GetInt("TotalEarning") >= Truck2Price && PlayerPrefs.GetInt("Truck2") == 0)
		{
            unlockDialog.GetComponent<Animation>().Play("Dialog up");
			//iTween.MoveTo(unlockDialog,iTween.Hash("y",-1.3f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		else if(selectedBus == 2 && PlayerPrefs.GetInt("TotalEarning") >= Truck3Price && PlayerPrefs.GetInt("Truck3") == 0)
		{
            unlockDialog.GetComponent<Animation>().Play("Dialog up");
			//.MoveTo(unlockDialog,iTween.Hash("y",-1.3f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		else if(selectedBus == 3 && PlayerPrefs.GetInt("TotalEarning") >= Truck4Price && PlayerPrefs.GetInt("Truck4") == 0)
		{
            unlockDialog.GetComponent<Animation>().Play("Dialog up");
		//	iTween.MoveTo(unlockDialog,iTween.Hash("y",-1.3f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		else if(selectedBus == 4 && PlayerPrefs.GetInt("TotalEarning") >= Truck5Price && PlayerPrefs.GetInt("Truck5") == 0)
		{
            unlockDialog.GetComponent<Animation>().Play("Dialog up");
		//	iTween.MoveTo(unlockDialog,iTween.Hash("y",-1.3f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		else if(selectedBus == 5 && PlayerPrefs.GetInt("TotalEarning") >= Truck6Price && PlayerPrefs.GetInt("Truck6") == 0)
		{
            unlockDialog.GetComponent<Animation>().Play("Dialog up");
		//	iTween.MoveTo(unlockDialog,iTween.Hash("y",-1.3f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
		else
		{
            cashDialog.GetComponent<Animation>().Play("Dialog up");
			//iTween.MoveTo(cashDialog,iTween.Hash("y",-1.3f,"easeType", iTween.EaseType.easeOutBack, "loopType", iTween.LoopType.none, "time", 1.0f));
		}
	}
    public void OkAYBttonClick()
    {
        cashDialog.GetComponent<Animation>().Play("Dialog down");
    }
	private void unlockFunction()
	{
		if(selectedBus == 1 && PlayerPrefs.GetInt("TotalEarning") > Truck2Price)
		{
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Truck2Price);
			PlayerPrefs.SetInt("Truck2",1);
			isLocked = true;
		}
		if(selectedBus == 2 && PlayerPrefs.GetInt("TotalEarning") > Truck3Price)
		{
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Truck3Price);
			PlayerPrefs.SetInt("Truck3",1);
			isLocked = true;
		}
		if(selectedBus == 3 && PlayerPrefs.GetInt("TotalEarning") > Truck4Price)
		{
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Truck4Price);
			PlayerPrefs.SetInt("Truck4",1);
			isLocked = true;
		}
		if(selectedBus == 4 && PlayerPrefs.GetInt("TotalEarning") > Truck5Price)
		{
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Truck5Price);
			PlayerPrefs.SetInt("Truck5",1);
			isLocked = true;
		}
		if(selectedBus == 5 && PlayerPrefs.GetInt("TotalEarning") > Truck6Price)
		{
			PlayerPrefs.SetInt("TotalEarning",PlayerPrefs.GetInt("TotalEarning") - Truck6Price);
			PlayerPrefs.SetInt("Truck6",1);
			isLocked = true;
		}
	}
	
	private void setLocks()
	{
		lockedImage.SetActive(false);
		isLocked = false;
		if(selectedBus == 0 && PlayerPrefs.GetInt("Truck1") == 0)
		{
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedBus == 1 && PlayerPrefs.GetInt("Truck2") == 0)
		{
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedBus == 2 && PlayerPrefs.GetInt("Truck3") == 0)
		{
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedBus == 3 && PlayerPrefs.GetInt("Truck4") == 0)
		{
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedBus == 4 && PlayerPrefs.GetInt("Truck5") == 0)
		{
			lockedImage.SetActive(true);
			isLocked = true;
		}
		if(selectedBus == 5 && PlayerPrefs.GetInt("Truck6") == 0)
		{
			lockedImage.SetActive(true);
			isLocked = true;
		}
	}

	private void setAllGameObjectFalse(GameObject[] mlist)
	{
		for(int i = 0; i < mlist.Length; i++)
		{
			mlist[i].gameObject.SetActive(false);
		}
	}
	
	private void setGameObjectActive(GameObject[] mlist,int n)
	{
		mlist[n].gameObject.SetActive(true);
	}
}
using UnityEngine;
using System.Collections;

public class DriveScript : MonoBehaviour {

//	public GameObject []body;
	public Transform[] wheels;
	public GameObject tailLights;
	public GameObject reverseLights;
	public GameObject Parasuit;
	public float enginePower = 30.0f;
	public float engineStart = 0.0f;
	public float power = 0.0f;
	public float TopSpeed = 0.0f;
	public float brake = 0.0f;
	public float steer = 0.0f;
	public float maxSteer = 25.0f;
	public float busSpeed = 0.0f;
	private Vector3 spawnPoint;
	private Quaternion spawnPointRotation;
	public bool raceOn = false;
	public float accelerator = 0.0f;
	public float divider = 10.0f;
	private bool reSpawnCar = false;
	public AudioClip crash;
	public GameObject revSound;
//	public GameObject SnowSparks;
//	public GameObject SnowSparks2;
//	public GameObject SnowArrow;
//	public GameObject parkArrow;
//	public GameObject parkingArea;
//	public GameObject yellowParking;
//	public GameObject BlueParking;
//	public GameObject GreenParking;
//	public GameObject MatchBetter;
//	public GameObject WrongWay;
	private AudioSource mAudioSource;
//	public GameObject mainCamera;
//	public GameObject trafficCamera;
//	public GameObject dummyTraffic;
//	public GameObject realTraffic;
//	public GameObject NewSnow;
//	public GameObject OldSnow;
	private RaycastHit hit;
	private Ray myRay;
	public Vector3 centerOfMass;
	private GameObject hudCameraObj;
	private HudCameraScript hudScript;
    private Targets targetsObj;
	int index=0;
	int Platform;
	GameObject gameplay;
	
	// Use this for initialization
	void Start () {
		spawnPoint = this.gameObject.transform.position;
		spawnPointRotation = this.gameObject.transform.rotation;
//		OldSnow.SetActive (true);
//		NewSnow.SetActive (false);
//		parkingArea.SetActive (false);
//		dummyTraffic.SetActive (true);
//		realTraffic.SetActive (false);
//		trafficCamera.SetActive (false);
		//SnowSparks.particleEmitter.emit = false;
		//SnowSparks2.particleEmitter.emit = false;
//		yellowParking.SetActive (true);
//		BlueParking.SetActive (false);
//		GreenParking.SetActive (false);
//		MatchBetter.SetActive (false);
//		WrongWay.SetActive (false);

		//revSound.SetActive (false);
		//enginePower = 30.0f;
		PlayerPrefs.SetInt("shake",0);
		if(PlayerPrefs.GetInt("Truck") == 1){
			enginePower = 30.0f;
			index=0;
		}
		if(PlayerPrefs.GetInt("Truck") == 2){
			enginePower = 33.0f;
			index=1;
		}
		if(PlayerPrefs.GetInt("Truck") == 3){
			enginePower = 42.0f;
			index=2;
		}
		if(PlayerPrefs.GetInt("Truck") == 4){
			enginePower = 42.0f;
			index=3;
		}
		
		this.transform.gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0.0f,-0.3f,-0.5f);
		Platform = (int) Application.platform;
		hudCameraObj = GameObject.Find("Hud Camera");
		hudScript = (HudCameraScript) hudCameraObj.GetComponent("HudCameraScript");
        targetsObj = hudCameraObj.GetComponent<Targets>();
		
		//gameplay = GameObject.Find("Hud Camera");
//		SnowArrow.SetActive(true);
//		parkArrow.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("selection") == 0) 
		{
			hudScript.hudControls.SetActive (false);
			hudScript.isPause = true;
		} 
		else  
		{
			//extraSnow.SetActive(false);
			hudScript.hudControls.SetActive(true);
			//tailLights.SetActive(false);
			//releaseBreak();
			hudScript.isPause = false;
		}
		if (PlayerPrefs.GetInt ("selectionOn") == 0) 
		{
			Parasuit.SetActive(false);		
		}
		//===============================================================================


	
//			if(power>0){
//				Quaternion targetup= Quaternion.Euler(body[index].transform.localRotation.x-2.5f,body[index].transform.localRotation.y,
//				                                      body[index].transform.localRotation.z);
//				body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetup,Time.deltaTime*2f);
//				
//		}
//			else if(power<0){
//				Quaternion targetup= Quaternion.Euler(body[index].transform.localRotation.x+2.5f,body[index].transform.localRotation.y,
//				                                      body[index].transform.localRotation.z);
//				body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetup,Time.deltaTime*2f);
//			}
//			else if(power==0){
//				Quaternion targetup= Quaternion.Euler(body[index].transform.localRotation.x,body[index].transform.localRotation.y,
//				                                      body[index].transform.localRotation.z);
//				body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetup,Time.deltaTime*2f);
//
//			}
//		if (steer>0){
//			Quaternion targetright= Quaternion.Euler(body[index].transform.localRotation.x,body[index].transform.localRotation.y,
//			                                         body[index].transform.localRotation.z-5f);
//			body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetright,Time.deltaTime*4f);
//			
//		}
//		else if (steer<0){
//			Quaternion targetright= Quaternion.Euler(body[index].transform.localRotation.x,body[index].transform.localRotation.y,
//			                                         body[index].transform.localRotation.z+5f);
//			body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetright,Time.deltaTime*4f);
//			
//		}
//			

	




		//===============================================================================



		//this.transform.gameObject.rigidbody.centerOfMass = new Vector3(0.0f,-0.5f,-0.5f);
		this.transform.gameObject.GetComponent<Rigidbody>().centerOfMass = centerOfMass;
		
		Vector3 up = transform.TransformDirection(Vector3.up);
		Vector3 front = transform.TransformDirection(Vector3.forward);
		
		if (Physics.Raycast (transform.position, -up, out hit)) 
		{
			Debug.DrawLine(transform.position, hit.point, Color.red);
			hudScript.statposition = true;
			if(hit.collider.gameObject.tag == "startPosition")
			{
				//audio.PlayOneShot(garbagesound,2.0f);
				hit.collider.gameObject.SetActive(false);
				//hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
				//gameplay.GetComponent<HudCameraScript>().showProgressBar();
				//hudScript.showObjectiveBar();
				//hudScript.statposition = true;
				
//				gameplay.GetComponent<HudCameraScript>().showObjectiveBar();
//				gameplay.GetComponent<HudCameraScript>().statposition = true;
				
			}
			if(hit.collider.gameObject.tag == "snow")
			{
//				Debug.Log("Blue");
//				BlueParking.SetActive(true);
//				yellowParking.SetActive(false);
			}
//			if(hit.collider.gameObject.tag == "endPosition"){
//				//hit.collider.gameObject.SetActive(false);
//				//hudScript.showProgressBar();
//				//hudScript.showObjectiveBar();
//				//PlayerPrefs.SetInt ("cam", 0);
//				//hudScript.isPause=true;
//				//rigidbody.velocity=Vector3.zero;
////				GreenParking.SetActive(true);
////				BlueParking.SetActive(false);
////				MatchBetter.SetActive(false);
////				yellowParking.SetActive(false);
////				StartCoroutine("WaitYar");
//				//hudScript.endPosition = true;
//				//gameplay.GetComponent<HudCameraScript>().endPosition = true;
////				trafficCamera.SetActive(true);
////				mainCamera.SetActive(false);
////				dummyTraffic.SetActive(false);
////				realTraffic.SetActive(true);
////				NewSnow.SetActive(false);
//				applyBreak();
//				StartCoroutine("WaitYar");
//				PlayerPrefs.SetInt ("selectionOn", 1);
//				PlayerPrefs.SetInt ("timeStart", 0);
//				rigidbody.isKinematic = true;
//				//hudScript.endPosition = true;
//			}
			if(hit.collider.gameObject.tag == "trashCan"){
				hit.collider.gameObject.transform.parent.gameObject.SetActive(false);
			}
			//Debug.Log("Under the Bus is = " + hit.collider.gameObject.name);
		}
		
//		if (Physics.Raycast (transform.position, front, out hit,4.0f)) {
//			Debug.DrawLine(transform.position, hit.point, Color.red);
//		}
		
		power = accelerator * enginePower * engineStart;

		if(accelerator == -1){
			reverseLights.SetActive(true);
			revSound.SetActive(true);
			//StartCoroutine("RevSound");

		}else{
			reverseLights.SetActive(false);
			//StopCoroutine ("RevSound");
			revSound.SetActive(false);
		}

		if(Platform == (int) RuntimePlatform.WindowsEditor || Platform == (int) RuntimePlatform.OSXEditor){
			power=Input.GetAxis("Vertical") * enginePower * 100;
			steer=Input.GetAxis("Horizontal") * maxSteer * 2;
			if(Input.GetKey("space")){
				applyBreak();
			}else{
				releaseBreak();
			}
			//brake=Input.GetKey("space") ? rigidbody.mass * 0.1f: 0.0f;
			if(Input.GetAxis("Vertical") == -1){
				reverseLights.SetActive(true);
				//revSound.SetActive(true);
				//StartCoroutine("RevSound");
				//audio.PlayOneShot (revSound);
			}else{
				reverseLights.SetActive(false);
				//revSound.SetActive(false);
				//StopCoroutine ("RevSound");
			}
		}
		
		GetCollider(0).steerAngle = steer;
		GetCollider(1).steerAngle = steer;
		
		
		getWheel(8).transform.localRotation = Quaternion.Euler (0,steer,0); 
		getWheel(9).transform.localRotation = Quaternion.Euler (0,steer,0);
//		getWheel(10).transform.localRotation = Quaternion.Euler (0,steer/2,0);
//		getWheel(11).transform.localRotation = Quaternion.Euler (0,steer/2,0);

		
		if(brake > 0.0){
			GetCollider(0).brakeTorque = brake;
			GetCollider(1).brakeTorque = brake;
			GetCollider(2).brakeTorque = brake;
			GetCollider(3).brakeTorque = brake;
			GetCollider(2).motorTorque = 0.0f;
			GetCollider(3).motorTorque = 0.0f;
			
		} else {
			GetCollider(0).brakeTorque = 0.0f;
			GetCollider(1).brakeTorque = 0.0f;
			GetCollider(2).brakeTorque = 0.0f;
			GetCollider(3).brakeTorque = 0.0f;
			GetCollider(2).motorTorque = power;
			GetCollider(3).motorTorque = power;
			
		}
		if(busSpeed >TopSpeed){
			GetCollider(0).brakeTorque = brake;
			GetCollider(1).brakeTorque = brake;
			GetCollider(2).brakeTorque = brake;
			GetCollider(3).brakeTorque = brake;
			GetCollider(2).motorTorque = 0.0f;
			GetCollider(3).motorTorque = 0.0f;
		}
		if(busSpeed <= -50){
			GetCollider(0).brakeTorque = brake;
			GetCollider(1).brakeTorque = brake;
			GetCollider(2).brakeTorque = brake;
			GetCollider(3).brakeTorque = brake;
			GetCollider(2).motorTorque = 0.0f;
			GetCollider(3).motorTorque = 0.0f;
		}
		
		//busSpeed = GetCollider(2).rpm;
		busSpeed = 2* 22/7*GetCollider(2).radius*GetCollider(2).rpm*60/1000;
		getWheel(4).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);
		getWheel(5).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);
		getWheel(6).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);
		getWheel(7).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);


		PlayerPrefs.SetFloat ("Rotatespeed", GetCollider (2).rpm * 6 * Time.deltaTime);

	}
	
	public void applyBreak(){
		brake = GetComponent<Rigidbody>().mass * 0.1f;
		tailLights.SetActive(true);
	}
	
	public void releaseBreak(){
		brake = 0.0f;
		tailLights.SetActive(false);
	}

    public void carRePosition()
    {
		GetComponent<Rigidbody>().isKinematic = false;
		this.gameObject.transform.rotation = spawnPointRotation;//targetsObj.currentTarget.transform.rotation;
		this.gameObject.transform.position = spawnPoint;//targetsObj.currentTarget.transform.position;
    }
	//void OnTriggerEnter() {
        //Time.timeScale = 0.1f;
        //PlayerPrefs.SetInt ("cam",4);
        //PlayerPrefs.SetInt ("hornStart",0);
        ////hudScript.controls.SetActive (false);
        //PlayerPrefs.SetInt ("selection",0);
        ////Debug.Log ("trigger");
	//}
	void OnTriggerExit(){
		Time.timeScale = 1;
		PlayerPrefs.SetInt ("cam",0);
		PlayerPrefs.SetInt ("hornStart",1);
		PlayerPrefs.SetInt ("selection",1);
		//hudScript.hudControls.SetActive (true);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Hardle") 
		{
            //hudScript.Mirror.SetActive(true);
            Handheld.Vibrate();
            GetComponent<AudioSource>().PlayOneShot(crash);
            //hudScript.consumeLife ();
            //PlayerPrefs.SetInt ("selection",0);
            //PlayerPrefs.SetInt("shake", 1);
            //StartCoroutine("trafficView");
           // GetComponent<Rigidbody>().isKinematic = true;
            //if (reSpawnCar == false)
            //{
            //    hudScript.panel.GetComponent<Animation>().Play("Dialog up");
            //    hudScript.panel.transform.parent.gameObject.SetActive(true);
            //    hudScript.controls.SetActive(false);
            //    //Time.timeScale = 0;
            //    //hudScript.isPause = true;
            //    reSpawnCar = true;
            //}
            //else
            //{
               
            //    //				gameplay.GetComponent<HudCameraScript>().consumeLife();
            //}
            hudScript.Mirror.SetActive(true);
            Handheld.Vibrate();
            GetComponent<AudioSource>().PlayOneShot(crash);
            //hudScript.consumeLife ();
            //PlayerPrefs.SetInt ("selection",0);
            PlayerPrefs.SetInt("shake", 1);
            StartCoroutine("trafficView");
		}
		if (collision.gameObject.tag == "endPosition") 
		{
//			NewSnow.SetActive(true);
//			OldSnow.SetActive(false);
			applyBreak();
			PlayerPrefs.SetInt ("selection",0);
			PlayerPrefs.SetInt ("selectionOn", 1);
			PlayerPrefs.SetInt ("timeStart", 0);
			GetComponent<Rigidbody>().isKinematic = true;
			//enginePower = 12.0f;
//			SnowArrow.SetActive(false);
//			parkArrow.SetActive(true);
//			parkingArea.SetActive(true);
			//SnowSparks.particleEmitter.emit = true;
			//SnowSparks2.particleEmitter.emit = true;
			//audio.PlayOneShot (crash);
			StartCoroutine("WaitYar");
			//Debug.Log ("snow hit");
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Collider>().name == "ReSpawnPosition") 
		{
			Debug.Log("Spawn");
			spawnPoint=other.GetComponent<Collider>().gameObject.transform.position;
			spawnPointRotation=other.GetComponent<Collider>().gameObject.transform.rotation;
			other.GetComponent<Collider>().gameObject.SetActive(false);
			//spawnPoint.rotation=other.gameObject.transform.rotation;
		}
		if(other.GetComponent<Collider>().name == "Cube"){
			Time.timeScale = 0.1f;
			PlayerPrefs.SetInt ("cam",4);
			PlayerPrefs.SetInt ("hornStart",0);
			//hudScript.controls.SetActive (false);
			PlayerPrefs.SetInt ("selection",0);
			//Debug.Log ("trigger");
		}
//		if(other.gameObject.name == "Color"){
////			BlueParking.SetActive(true);
////			MatchBetter.SetActive (true);
////			yellowParking.SetActive(false);
//		}
//		if (other.gameObject.name == "Wrong") {
////			WrongWay.SetActive(true);		
//		}
	}
//	void OnTriggerStay(Collider other){
//		if (other.gameObject.name == "Color") {
//			BlueParking.SetActive (true);
//			yellowParking.SetActive(false);
//		}
//	}
	void OnTriggerExit(Collider other)
	{
	
				//if (other.gameObject.name == "Color") {
//						BlueParking.SetActive (false);
//						MatchBetter.SetActive (false);
//						yellowParking.SetActive (true);
//						WrongWay.SetActive (false);
				//}
	}
	private GameObject getWheel(int n){
		return wheels[n].gameObject;
	}
	
	private WheelCollider GetCollider(int n){
		return (WheelCollider) wheels[n].transform.gameObject.GetComponent("WheelCollider");
	}
	IEnumerator WaitYar()
	{
		yield return new WaitForSeconds (1.0f);
		//SnowSparks.particleEmitter.emit = false;
		//SnowSparks2.particleEmitter.emit = false;
		//Debug.Log ("wait");
		PlayerPrefs.SetInt ("cam", 4);
		hudScript.endPosition = true;
		StopCoroutine ("WaitYar");
	}
	IEnumerator trafficView()
	{
		yield return new WaitForSeconds (1.0f);
		hudScript.consumeLife ();
        //hudScript.lifes = 0;
        //hudScript.endPosition = true;
		StopCoroutine ("trafficView");
	}
}
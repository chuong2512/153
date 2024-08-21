using UnityEngine;
using System.Collections;

public class driveAuto : MonoBehaviour {
	
	//public GameObject []body;
	
	
	public Transform[] wheels;
	
	//public GameObject tailLights;
	//public GameObject reverseLights;
	
	public float enginePower = 30.0f;
	public float engineStart = 0.0f;
	public float power = 12.0f;
	
	
	public float brake = 0.0f;
	public float steer = 0.0f;
	public float maxSteer = 25.0f;
	public float busSpeed = 0.0f;
	public float topSpeed = 100;
	public float reverseSpeed = 50;
	//public GameObject bussSpeed;
	
	public bool raceOn = false;
	public float accelerator = 1.0f;
	public float divider = 10.0f;
	
	//public AudioClip crash;
	//public GameObject crashSound;
	
	//public GameObject revSound;
	//public GameObject hitSound;
	//public GameObject SnowSparks;
	//public GameObject SnowSparks2;
	//public GameObject SnowArrow;
	//public GameObject parkingArea;
	//	public GameObject yellowParking;
	//	public GameObject BlueParking;
	//	public GameObject GreenParking;
	//	public GameObject MatchBetter;
	//	public GameObject WrongWay;
	private AudioSource mAudioSource;
	//public GameObject mainCamera;
	//public GameObject trafficCamera;
	//public GameObject dummyTraffic;
	//public GameObject realTraffic;
	
	//public GameObject NewSnow;
	//public GameObject OldSnow;
	
	
	
	
	private RaycastHit hit;
	private Ray myRay;
	
	public Vector3 centerOfMass;
	
	//private GameObject hudCameraObj;
	//private HudCameraScript hudScript;
	int index=0;
	int Platform;
	GameObject gameplay;
	//public GameObject Speedometer;
	private float motionfactor ;
	private float rotationAngle;
	//public Sprite speedomtr;
	//**************************************************************************
	//	public WheelCollider wheelFL;
	//	//public WheelCollider wheelFR;
	//	public Transform wheelFLTrans;
	//	public Vector3 wheelPos; 
	
	
	
	//**************************************************************************
	// Use this for initialization
	void Start () {
		//crashSound.SetActive (false);
		//hitSound.SetActive (false);
		PlayerPrefs.SetInt("stop",1);
		//OldSnow.SetActive (true);
		//NewSnow.SetActive (false);
		//		parkingArea.SetActive (false);
		//		//dummyTraffic.SetActive (true);
		//		//realTraffic.SetActive (false);
		//		trafficCamera.SetActive (false);
		//		SnowSparks.particleEmitter.emit = false;
		//		SnowSparks2.particleEmitter.emit = false;
		//		yellowParking.SetActive (true);
		//		BlueParking.SetActive (false);
		//		GreenParking.SetActive (false);
		//		MatchBetter.SetActive (false);
		//		WrongWay.SetActive (false);
		
		//revSound.SetActive (false);
		//enginePower = 30.0f;
		if(PlayerPrefs.GetInt("Truck") == 1){
			enginePower = 7.0f;
			index=0;
		}
		if(PlayerPrefs.GetInt("Truck") == 2){
			enginePower = 7.0f;
			index=1;
		}
		if(PlayerPrefs.GetInt("Truck") == 3){
			enginePower = 8.0f;
			index=2;
		}
		if(PlayerPrefs.GetInt("Truck") == 4){
			enginePower = 12.0f;
			index=3;
		}
		this.transform.gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0.0f,-0.3f,-0.5f);
		Platform = (int) Application.platform;
		//hudCameraObj = GameObject.Find("Hud Camera");
		//hudScript = (HudCameraScript) hudCameraObj.GetComponent("HudCameraScript");
		
		//gameplay = GameObject.Find("Hud Camera");
		//SnowArrow.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update () {

		//steer=Input.GetAxis("Horizontal") * maxSteer;
		if (PlayerPrefs.GetInt ("stop") == 1) {
			raceOn = true;
			enginePower = 12.0f;
			power=1 * enginePower;
						getWheel (4).transform.Rotate (GetCollider (2).rpm * 6 * Time.deltaTime, 0.0f, 0.0f);
						getWheel (5).transform.Rotate (GetCollider (2).rpm * 6 * Time.deltaTime, 0.0f, 0.0f);
						getWheel (6).transform.Rotate (GetCollider (2).rpm * 6 * Time.deltaTime, 0.0f, 0.0f);
						getWheel (7).transform.Rotate (GetCollider (2).rpm * 6 * Time.deltaTime, 0.0f, 0.0f);
		
		
						PlayerPrefs.SetFloat ("Rotatespeed", GetCollider (3).rpm * 6 * Time.deltaTime);
						busSpeed = 2 * 22 / 7 * GetCollider (2).radius * GetCollider (2).rpm * 60 / 1000;
						busSpeed = Mathf.Round (busSpeed);
						busSpeed = Mathf.Abs (busSpeed);
						GetCollider (0).brakeTorque = 0.0f;
						GetCollider (1).brakeTorque = 0.0f;
						GetCollider (2).brakeTorque = 0.0f;
						GetCollider (3).brakeTorque = 0.0f;
						GetCollider (2).motorTorque = power;
						GetCollider (3).motorTorque = power;
		} else if(PlayerPrefs.GetInt ("stop") == 0){
			raceOn =false;
			enginePower = 0.0f;
			power=1 * enginePower;
			GetCollider(0).brakeTorque = brake;
			GetCollider(1).brakeTorque = brake;
			GetCollider(2).brakeTorque = brake;
			GetCollider(3).brakeTorque = brake;
			GetCollider(2).motorTorque = 0.0f;
			GetCollider(3).motorTorque = 0.0f;
		}
		//power = 1 * enginePower;
		//bussSpeed.transform.GetComponent<TextMesh>().text = " " +  (int)busSpeed;
		//		if(busSpeed)
		//wheelposition();
//		if (PlayerPrefs.GetInt ("selectionOn") == 1) {
//			//hudScript.hudControls.SetActive (false);		
//		} else{
//			//extraSnow.SetActive(false);
//			hudScript.hudControls.SetActive(true);	
//		}
//		if (PlayerPrefs.GetInt ("increasedrag") == 1) {
//			applyBreak();
//			rigidbody.freezeRotation = true;
//			//hitSound.SetActive(true);
//			//audio.PlayOneShot (crash);
//		}
		//		if (PlayerPrefs.GetInt ("crashSound") == 1) {
		//			crashSound.SetActive (false);
		//		} else if (PlayerPrefs.GetInt ("crashSound") == 0) {
		//			crashSound.SetActive(false);
		//		}
		//===============================================================================
		
		
		
//		if(power>0){
//			Quaternion targetup= Quaternion.Euler(body[index].transform.localRotation.x-2.0f,body[index].transform.localRotation.y,
//			                                      body[index].transform.localRotation.z);
//			body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetup,Time.deltaTime*2f);
//			
//		}
//		else if(power<0){
//			Quaternion targetup= Quaternion.Euler(body[index].transform.localRotation.x+2.0f,body[index].transform.localRotation.y,
//			                                      body[index].transform.localRotation.z);
//			body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetup,Time.deltaTime*2f);
//		}
//		else if(power==0){
//			Quaternion targetup= Quaternion.Euler(body[index].transform.localRotation.x,body[index].transform.localRotation.y,
//			                                      body[index].transform.localRotation.z);
//			body[index].transform.localRotation=Quaternion.Slerp(body[index].transform.localRotation,targetup,Time.deltaTime*2f);
//			
//		}
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
		
		
		
		
		
		
		
		//===============================================================================
		
		
		
		//this.transform.gameObject.rigidbody.centerOfMass = new Vector3(0.0f,-0.5f,-0.5f);
		this.transform.gameObject.GetComponent<Rigidbody>().centerOfMass = centerOfMass;
		
		Vector3 up = transform.TransformDirection(Vector3.up);
		Vector3 front = transform.TransformDirection(Vector3.forward);
		
		if (Physics.Raycast (transform.position, -up, out hit)) {
			Debug.DrawLine(transform.position, hit.point, Color.red);
			//hudScript.statposition = true;
			if(hit.collider.gameObject.tag == "startPosition"){
				//audio.PlayOneShot(garbagesound,2.0f);
				hit.collider.gameObject.SetActive(false);
				//hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
				//gameplay.GetComponent<HudCameraScript>().showProgressBar();
				//hudScript.showObjectiveBar();
				//hudScript.statposition = true;
				
				//				gameplay.GetComponent<HudCameraScript>().showObjectiveBar();
				//				gameplay.GetComponent<HudCameraScript>().statposition = true;
				
			}
			//			if(hit.collider.gameObject.tag == "snow"){
			//
			//
			//				Debug.Log("Blue");
			//				BlueParking.SetActive(true);
			//				yellowParking.SetActive(false);
			//			}
			
			
			
			if(hit.collider.gameObject.tag == "endPosition"){
				//hit.collider.gameObject.SetActive(false);
				//hudScript.showProgressBar();
				//hudScript.showObjectiveBar();
				//PlayerPrefs.SetInt ("cam", 0);
				//hudScript.isPause=true;
				//rigidbody.velocity=Vector3.zero;
				//				GreenParking.SetActive(true);
				//				BlueParking.SetActive(false);
				//				MatchBetter.SetActive(false);
				//				yellowParking.SetActive(false);
				//				StartCoroutine("WaitYar");
				//hudScript.endPosition = true;
				//gameplay.GetComponent<HudCameraScript>().endPosition = true;
				//trafficCamera.SetActive(true);
				//mainCamera.SetActive(false);
				//dummyTraffic.SetActive(false);
				//realTraffic.SetActive(true);
				///NewSnow.SetActive(false);
				//StartCoroutine("trafficView");
				//PlayerPrefs.SetInt ("selectionOn", 1);
				//PlayerPrefs.SetInt ("timeStart", 0);
				PlayerPrefs.SetInt("stop",0);
				GetComponent<Rigidbody>().isKinematic = true;
				//hudScript.endPosition = true;
			}
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
			//reverseLights.SetActive(true);
			//revSound.SetActive(true);
			//StartCoroutine("RevSound");
			
		}else{
			//reverseLights.SetActive(false);
			//StopCoroutine ("RevSound");
			//revSound.SetActive(false);
		}

		if (Platform == (int)RuntimePlatform.WindowsEditor)
		{
			power = Input.GetAxis("Vertical") * enginePower * 100;
			power = 1 * enginePower;
			steer = Input.GetAxis("Horizontal") * maxSteer;
			if (Input.GetKey("space"))
			{
				applyBreak();
			}
			else
			{
				releaseBreak();
			}

			//brake=Input.GetKey("space") ? rigidbody.mass * 0.1f: 0.0f;
			if (Input.GetAxis("Vertical") == -1)
			{
				//reverseLights.SetActive(true);
				//revSound.SetActive(true);
				//StartCoroutine("RevSound");
				//audio.PlayOneShot (revSound);
			}
			else
			{
				//reverseLights.SetActive(false);
				//revSound.SetActive(false);
				//StopCoroutine ("RevSound");
			}
			//}

			GetCollider(0).steerAngle = steer;
			GetCollider(1).steerAngle = steer;


			getWheel(8).transform.localRotation = Quaternion.Euler(0, steer, 0);
			getWheel(9).transform.localRotation = Quaternion.Euler(0, steer, 0);



			if (brake > 0.0)
			{
				//			if (rigidbody.velocity.magnitude >1){
				//				PlayerPrefs.SetInt("friction",1);
				//			}
				//			else{
				//				PlayerPrefs.SetInt("friction",2);
				//			}

				GetCollider(0).brakeTorque = brake;
				GetCollider(1).brakeTorque = brake;
				GetCollider(2).brakeTorque = brake;
				GetCollider(3).brakeTorque = brake;
				GetCollider(2).motorTorque = 0.0f;
				GetCollider(3).motorTorque = 0.0f;

			}
			else
			{
				//PlayerPrefs.SetInt("friction",3);
				GetCollider(0).brakeTorque = 0.0f;
				GetCollider(1).brakeTorque = 0.0f;
				GetCollider(2).brakeTorque = 0.0f;
				GetCollider(3).brakeTorque = 0.0f;
				GetCollider(2).motorTorque = power;
				GetCollider(3).motorTorque = power;

			}

			//////////////////////////////////////////////////////////////////////////
			if (busSpeed >= topSpeed)
			{
				GetCollider(0).brakeTorque = brake;
				GetCollider(1).brakeTorque = brake;
				GetCollider(2).brakeTorque = brake;
				GetCollider(3).brakeTorque = brake;
				GetCollider(2).motorTorque = 0.0f;
				GetCollider(3).motorTorque = 0.0f;

			}
			else
			{
				GetCollider(0).brakeTorque = 0.0f;
				GetCollider(1).brakeTorque = 0.0f;
				GetCollider(2).brakeTorque = 0.0f;
				GetCollider(3).brakeTorque = 0.0f;
				GetCollider(2).motorTorque = power;
				GetCollider(3).motorTorque = power;

			}
		}

		//		motionfactor = busSpeed / topSpeed;
		//		rotationAngle = Mathf.Lerp (0,180, motionfactor);
		//Speedometer.transform.RotateAround (Vector3.zero, Vector3.up, motionfactor * Time.deltaTime);
		
		//				Quaternion needlemove= Quaternion.Euler(Speedometer.transform.rotation.x,Speedometer.transform.rotation.y,
		//		                                        Speedometer.transform.rotation.z-(busSpeed*1.35f));
		//		Speedometer.transform.localRotation=Quaternion.Slerp(Speedometer.transform.localRotation,needlemove,Time.deltaTime*2f);
		
		
		
		
		/////////////////////////////////////////////////////////////////////
		
		
		
		
		
		
		
		
		
		
		
//		
//		getWheel(4).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);
//		getWheel(5).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);
//		getWheel(6).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);
//		getWheel(7).transform.Rotate(GetCollider(2).rpm*6*Time.deltaTime,0.0f,0.0f);
//		
//		
//		PlayerPrefs.SetFloat ("Rotatespeed", GetCollider (3).rpm * 6 * Time.deltaTime);
//		busSpeed = 2* 22/7*GetCollider(2).radius*GetCollider(2).rpm*60/1000;
//		busSpeed = Mathf.Round (busSpeed);
//		busSpeed = Mathf.Abs (busSpeed);
	}
	
	//	void OnCollisionEnter (Collision coll)
	//	{
	//		if (coll.gameObject.name == "Yellow Parking") {
	//			BlueParking.SetActive(true);
	//			yellowParking.SetActive(false);
	//		}
	//		else{
	//			BlueParking.SetActive(false);
	//			yellowParking.SetActive(true);
	//		}
	//	}
	//	public void wheelposition(){
	//		//Vector3 wheelPos;
	//		if (Physics.Raycast (wheelFL.transform.position, -wheelFL.transform.up, hit, wheelFL.radius + wheelFL.suspensionDistance)) {
	//			wheelPos = hit.point + wheelFL.transform.up * wheelFL.radius;
	//		}
	//		else {
	//			wheelPos = wheelFL.transform.position-wheelFL.transform.up*wheelFL.suspensionDistance;
	//		}
	//		wheelFLTrans.position = wheelPos;
	//	}
	
	
	public void applyBreak(){
		brake = GetComponent<Rigidbody>().mass * 3.0f;
		//tailLights.SetActive(true);
	}
	
	public void releaseBreak(){
		brake = 0.0f;
		//tailLights.SetActive(false);
	}
	
	//	void OnTriggerEnter(Collider other) {
	////		if(other.gameObject.tag == "Hardle"){
	////			Handheld.Vibrate();
	////			hudScript.consumeLife();
	////			//gameplay.GetComponent<HudCameraScript>().consumeLife();
	////		}
	//	}
	
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Hardle") {
			Handheld.Vibrate ();
			Debug.Log("hardle");
			//audio.PlayOneShot (crash);
			//hudScript.consumeLife ();
			//gameplay.GetComponent<HudCameraScript>().consumeLife();
		}
		
		if (collision.gameObject.tag == "endPosition") {
			//NewSnow.SetActive(true);
			//OldSnow.SetActive(false);
			applyBreak();
			raceOn= false;
			PlayerPrefs.SetInt("stop",0);
			GetComponent<Rigidbody>().isKinematic = true;
			//enginePower = 12.0f;
			//			SnowArrow.SetActive(false);
			//			parkingArea.SetActive(true);
			//			SnowSparks.particleEmitter.emit = true;
			//			SnowSparks2.particleEmitter.emit = true;
			//audio.PlayOneShot (crash);
			//StartCoroutine("WaitYar");
			Debug.Log ("snow hit");
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Color"){
			//			BlueParking.SetActive(true);
			//			MatchBetter.SetActive (true);
			//			yellowParking.SetActive(false);
		}
		if (other.gameObject.name == "Wrong") {
			//			WrongWay.SetActive(true);		
		}
	}
	//	void OnTriggerStay(Collider other){
	//		if (other.gameObject.name == "Color") {
	//			BlueParking.SetActive (true);
	//			yellowParking.SetActive(false);
	//		}
	//	}
	void OnTriggerExit(Collider other){
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
	IEnumerator WaitYar(){
		yield return new WaitForSeconds (2.0f);
		//		SnowSparks.particleEmitter.emit = false;
		//		SnowSparks2.particleEmitter.emit = false;
		Debug.Log ("wait");
		//hudScript.endPosition = true;
		StopCoroutine ("WaitYar");
		
	}
	//	IEnumerator trafficView(){
	//		yield return new WaitForSeconds (10.0f);
	//
	//		hudScript.endPosition = true;
	//		StopCoroutine ("trafficView");
	//		
	//	}
}
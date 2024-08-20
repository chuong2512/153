using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class waypoints : MonoBehaviour {
	
	public float speed= 2f;
	public int waypoint=0;
	public Transform[] target;
	public Transform[] wheelCollders;
	public float brake;

	private GameObject hudCameraObj;
	private HudCameraScript hudScript;
	private DriveScript drivesc;
	//public GameObject crash;
	// Use this for initialization
	
	public Quaternion rotation;
	public Transform temp;
	RaycastHit hit;
	public Vector3 fwd;
	double db=0;
	void Start () {

		//transform.tag = "box";
		hudCameraObj = GameObject.Find("Hud Camera");
		hudScript = (HudCameraScript) hudCameraObj.GetComponent("HudCameraScript");


	
	}
	
	// Update is called once per frame
	void Update () {
				fwd = transform.TransformDirection (transform.forward);
				temp = target [waypoint];
				//db = System.Math.Round (Vector3.Distance (GameObject.FindGameObjectWithTag ("snowpt").transform.position, transform.position) / 1000f, 1);

//		if (db < 0.01) {
//			PlayerPrefs.SetInt("crashSound",1);
//				} else {
//			PlayerPrefs.SetInt("crashSound",0);
//		}
						GetComponent<Rigidbody>().drag = 0;
						if (Physics.Raycast (transform.position + (Vector3.up * 0.3f), transform.forward, out hit, 5.0f)) {
								Debug.DrawLine (transform.position + (Vector3.up * 0.3f), hit.point, Color.blue);
			
								if (hit.collider.tag == "Collider" || hit.collider.tag == "traffic") {
										Debug.Log (hit.collider.gameObject.name);
										move (false);
								} else {
										move (true);
								}
						} else {
			if(PlayerPrefs.GetInt("pausetouch")==0){
								move (true);
						}}
				}
//	}
	void OnTriggerEnter()
	{
		target [waypoint].gameObject.SetActive (false);
		waypoint++ ;
		if (waypoint == target.Length)
			waypoint = 0;
		target [waypoint].gameObject.SetActive (true);
	}
	
	void move(bool move)
	{
		if(move)
		{
			rotation=Quaternion.LookRotation(temp.position-transform.position);
			transform.rotation =Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime * 2);	
			transform.Translate (0.0f,0.0f,speed * Time.deltaTime);
			
		}
		else
		{
//			brake = rigidbody.mass * 4.0f;
//			wheelCollders[0].GetComponent<WheelCollider>().brakeTorque = brake;
//			wheelCollders[1].GetComponent<WheelCollider>().brakeTorque = brake;
//			wheelCollders[2].GetComponent<WheelCollider>().brakeTorque = brake;
//			wheelCollders[3].GetComponent<WheelCollider>().brakeTorque = brake;
		}
	}
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Truck") {
			Debug.Log("Hit with Truck");
			Handheld.Vibrate ();
			PlayerPrefs.SetInt("increasedrag",1);
			//rigidbody.freezeposition =true;
			//drivesc.applyBreak ();
			hudScript.isPause = true;
			GetComponent<Rigidbody>().isKinematic = true;
			PlayerPrefs.SetInt ("pausetouch", 1);
			//audio.PlayOneShot (crash);
			//hudScript.consumeLife ();
			StartCoroutine("WaitYar");
		}

	}
	IEnumerator WaitYar(){
		yield return new WaitForSeconds (1.0f);
		//		SnowSparks.particleEmitter.emit = false;
		//		SnowSparks2.particleEmitter.emit = false;
		Debug.Log ("wait");
		hudScript.gameOver = true;
		PlayerPrefs.SetInt("Earning",0);
		PlayerPrefs.SetInt("Time",0);
		PlayerPrefs.SetString("GameOver","Crash");
		SceneManager.LoadScene("Game Over");

		StopCoroutine ("WaitYar");
		
	}
	
}

var target : Transform;
var damping = 1.0;
var smooth = true;
var tempObj : GameObject;

@script AddComponentMenu("Camera-Control/Smooth Look At")

function LateUpdate () {
	tempObj = GameObject.FindGameObjectWithTag ("traffic");
	target = tempObj.transform;
	if (target) {
		if (smooth)
		{
			// Look at and dampen the rotation
			var rotation = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		}
		else
		{
			// Just lookat
		    transform.LookAt(target);
		}
	}
}

function Start () {
	// Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>())
		GetComponent.<Rigidbody>().freezeRotation = true;
}
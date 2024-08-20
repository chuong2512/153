#pragma strict
var wheelFL : WheelCollider;
var wheelFR : WheelCollider;
var wheelRL : WheelCollider;
var wheelRR : WheelCollider;

var wheelFLTrans : Transform; 
var wheelFRTrans : Transform; 
var wheelRLTrans : Transform; 
var wheelRRTrans : Transform; 

private var mySidewayFriction : float;
private var myForwardFriction : float;
private var slipSidewayFriction : float;
private var slipForwardFriction : float;

private var currentSpeed : float;
//var blast : GameObject;


function Start () {
SetValues();
}
function SetValues (){
myForwardFriction  = wheelRR.forwardFriction.stiffness;
mySidewayFriction  = wheelRR.sidewaysFriction.stiffness;
slipForwardFriction = 0.05;
slipSidewayFriction = 0.085;
}

function Update () {
if(PlayerPrefs.GetInt("selection")==1){
wheelposition();
Controle();
ReverseSlip();
}
}
function Controle (){
currentSpeed = 2*22/7*wheelRR.radius*wheelRR.rpm*60/1000;
currentSpeed = Mathf.Round(currentSpeed);

}
function wheelposition(){
		var hit : RaycastHit;
		var wheelPos : Vector3;
		if (Physics.Raycast (wheelFL.transform.position, -wheelFL.transform.up, hit, wheelFL.radius + wheelFL.suspensionDistance)) {
			wheelPos = hit.point + wheelFL.transform.up * wheelFL.radius;
		}
		else {
			wheelPos = wheelFL.transform.position-wheelFL.transform.up*wheelFL.suspensionDistance;
		}
		wheelFLTrans.position = wheelPos;
		
		
				if (Physics.Raycast (wheelFR.transform.position, -wheelFR.transform.up, hit, wheelFR.radius + wheelFR.suspensionDistance)) {
			wheelPos = hit.point + wheelFR.transform.up * wheelFR.radius;
		}
		else {
			wheelPos = wheelFR.transform.position-wheelFR.transform.up*wheelFR.suspensionDistance;
		}
		wheelFRTrans.position = wheelPos;
		
		
				if (Physics.Raycast (wheelRL.transform.position, -wheelRL.transform.up, hit, wheelRL.radius + wheelRL.suspensionDistance)) {
			wheelPos = hit.point + wheelRL.transform.up * wheelRL.radius;
		}
		else {
			wheelPos = wheelRL.transform.position-wheelRL.transform.up*wheelRL.suspensionDistance;
		}
		wheelRLTrans.position = wheelPos;

		
				if (Physics.Raycast (wheelRR.transform.position, -wheelRR.transform.up, hit, wheelRR.radius + wheelRR.suspensionDistance)) {
			wheelPos = hit.point + wheelRR.transform.up * wheelRR.radius;
		}
		else {
			wheelPos = wheelRR.transform.position-wheelRR.transform.up*wheelRR.suspensionDistance;
		}
		wheelRRTrans.position = wheelPos;			
}
function ReverseSlip(){
if (currentSpeed <0){
SetFrontSlip(slipForwardFriction ,slipSidewayFriction);
}
else {
SetFrontSlip(myForwardFriction ,mySidewayFriction);
}
}
 
function SetRearSlip (currentForwardFriction : float,currentSidewayFriction : float){
wheelRR.forwardFriction.stiffness = currentForwardFriction;
wheelRL.forwardFriction.stiffness = currentForwardFriction;
wheelRR.sidewaysFriction.stiffness = currentSidewayFriction;
wheelRL.sidewaysFriction.stiffness = currentSidewayFriction;
}
function SetFrontSlip (currentForwardFriction : float,currentSidewayFriction : float){
wheelFR.forwardFriction.stiffness = currentForwardFriction;
wheelFL.forwardFriction.stiffness = currentForwardFriction;
wheelFR.sidewaysFriction.stiffness = currentSidewayFriction;
wheelFL.sidewaysFriction.stiffness = currentSidewayFriction;
}
//function OnCollisionEnter(col : Collision){
//if(col.transform != transform ){
//for(var i=0; i< col.contacts.Length; i++){
//Instantiate(blast,col.contacts[i].point,Quaternion.identity);
//}
//}
//}
//function OnTriggerEnter(){
//Time.timeScale = 0.08f;
//PlayerPrefs.SetInt ("cam", 4);
//}
//function OnTriggerExit(){
//Time.timeScale = 1.0f;
//PlayerPrefs.SetInt ("cam", 0);
//}
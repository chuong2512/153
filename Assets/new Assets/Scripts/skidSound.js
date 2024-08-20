 #pragma strict
var currentFrictionValue : float;
var skidAt : float = 1.5;
var soundEmition : float = 15;
var soundWait : float;
var skidSound : GameObject;
var markWidth : float = 0.2;
private var skidding : int;
private var lastPos = new Vector3[2];
var skidMaterial : Material;


function Start () {

}

function Update () {
	var hit : WheelHit;
	transform.GetComponent(WheelCollider).GetGroundHit(hit);
	currentFrictionValue = Mathf.Abs(hit.sidewaysSlip);
	
	if(skidAt <= currentFrictionValue && soundWait <= 0){
		Instantiate(skidSound,hit.point,Quaternion.identity);
		soundWait = 1;
	}
	soundWait -= Time.deltaTime*soundEmition;
	if(skidAt <= currentFrictionValue){
	skidMesh();
	}
	else{
	skidding = 0;
	}
}
function skidMesh(){
var hit :WheelHit;
transform.GetComponent(WheelCollider).GetGroundHit(hit);
var mark : GameObject = new GameObject("Mark");
var filter : MeshFilter = mark.AddComponent(MeshFilter);
mark.AddComponent(MeshRenderer);
var markMesh : Mesh= new Mesh();
var vertices = new Vector3 [4];
var triangles = new int[6];
if(skidding == 0){
vertices[0] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z)*(Vector3(markWidth,0.01,0));
vertices[1] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z)*(Vector3(-markWidth,0.01,0));
vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z)*(Vector3(-markWidth,0.01,0));
vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z)*(Vector3(markWidth,0.01,0));
lastPos[0] = vertices[2];
lastPos[1] = vertices[3];


skidding = 1;
}
else{
 	vertices[1]= lastPos[0];
	vertices[0]= lastPos[1];
	vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z)*(Vector3(-markWidth,0.01,0));
	vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z)*(Vector3(markWidth,0.01,0));
	lastPos[0] = vertices[2];
	lastPos[1] = vertices[3];
}
triangles = [0,1,2,2,3,0];
markMesh.vertices = vertices;
markMesh.triangles = triangles;
markMesh.RecalculateNormals();
var uvm = new Vector2[4];
uvm[0] = Vector2(1,0);
uvm[1] = Vector2(0,0);
uvm[2] = Vector2(0,1);
uvm[3] = Vector2(1,1);

markMesh.uv = uvm;
filter.mesh = markMesh;
mark.GetComponent.<Renderer>().material = skidMaterial;
mark.AddComponent(DestroyObj);
}

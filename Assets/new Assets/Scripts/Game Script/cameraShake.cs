using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour {
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (PlayerPrefs.GetInt ("shake") == 1) {
						Shake ();
				}
				if (shake_intensity > 0) {
					transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
					transform.rotation = new Quaternion (
					originRotation.x + Random.Range (-shake_intensity, shake_intensity) * .15f,
					originRotation.y + Random.Range (-shake_intensity, shake_intensity) * .15f,
					originRotation.z + Random.Range (-shake_intensity, shake_intensity) * .15f,
					originRotation.w + Random.Range (-shake_intensity, shake_intensity) * .15f);
					shake_intensity -= shake_decay;
						}
				}


	void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = .1f;
		shake_decay = 0.002f;
	}
}

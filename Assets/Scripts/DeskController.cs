using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DeskController : MonoBehaviour {

	private FirstPersonController fpsController;
	private Camera playerCamera;
	private bool isWaiting = false;
	private int speed = 5;

	void Start () {
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		playerCamera = GameObject.Find ("FPSController").GetComponentInChildren<Camera> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			//centerCamera();
			fpsController.enabled = false;
			isWaiting = true;
		}
	}

	void centerCamera() {

		Quaternion targetRotation = Quaternion.LookRotation(GetComponentInParent<Transform> ().position - playerCamera.transform.position);
		playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, speed * Time.deltaTime);
		//playerCamera.transform.LookAt(GetComponentInParent<Transform> ());
	}
	
	// Update is called once per frame
	void Update () {
		if (isWaiting) {
			centerCamera();
			if (Input.GetKeyDown ("escape")) {
				fpsController.enabled = true;
				isWaiting = false;
			}
		}
	}
}

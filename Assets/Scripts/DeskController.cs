using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DeskController : MonoBehaviour {

	private FirstPersonController fpsController;
	private Camera playerCamera;
	private bool isWaiting = false;

	void Start () {
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		playerCamera = GameObject.Find ("FPSController").GetComponentInChildren<Camera> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			centerCamera();
			isWaiting = true;
		}
	}

	void centerCamera() {
		//fpsController.ge
		fpsController.enabled = false;
		playerCamera.transform.LookAt(GetComponentInParent<Transform> ());
		//GetComponentInParent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWaiting) {
			if (Input.GetKeyDown ("escape")) {
				fpsController.enabled = true;
				isWaiting = false;
			}
		}
	}
}

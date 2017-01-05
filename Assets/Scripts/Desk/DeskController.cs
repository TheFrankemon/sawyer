using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DeskController : MonoBehaviour {

	enum PlayerDeskState {
		WAITING, LOOKING_AT_LECTURER, NONE
	}

	private FirstPersonController fpsController;
	private Camera playerCamera;
	private int speed = 5;
	private Transform lecturer;
	private PlayerDeskState state;
	private CurriculumController curriculum;
	private DeskPictureFrameController pictureFrame;

	void Start () {
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		playerCamera = GameObject.Find ("FPSController").GetComponentInChildren<Camera> ();
		curriculum = GetComponentInChildren<CurriculumController> ();
		pictureFrame = GetComponentInChildren<DeskPictureFrameController> ();

		curriculum.setDeskController (this);
		pictureFrame.setDeskController (this);
		state = PlayerDeskState.NONE;

		foreach (Transform t in transform)
		{
			if(t.name == "Lecturer") {
				lecturer = t;
				break;
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			//centerCamera();
			lookDown();
		}
	}

	public void lookDown() {
		enabled = true;
		fpsController.enabled = false;
		//isWaiting = true;
		state = PlayerDeskState.WAITING;
		curriculum.setAvailability (true);
		pictureFrame.setAvailability (true);
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls(ControlsUIController.ControlsType.DESK);
	}

	public void lookAtLecturer() {
		state = PlayerDeskState.LOOKING_AT_LECTURER;
		curriculum.setAvailability (false);
		pictureFrame.setAvailability (false);
	}

	void centerCameraToDesk() {

		Quaternion targetRotation = Quaternion.LookRotation(GetComponentInParent<Transform> ().position - playerCamera.transform.position);
		playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, speed * Time.deltaTime);
		//playerCamera.transform.LookAt(GetComponentInParent<Transform> ());
	}

	public void centerCameraToLecturer() {
		Quaternion targetRotation = Quaternion.LookRotation(lecturer.position - playerCamera.transform.position);
		playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, speed * Time.deltaTime);
	}

	public bool isLookingAtLecturer() {
		Vector3 dirFromAtoB = (lecturer.position - playerCamera.transform.position).normalized;
		float dotProd = Vector3.Dot(dirFromAtoB, playerCamera.transform.forward);
		return dotProd > 0.9;
	}

	void handleExit() {
		if (Input.GetKeyDown ("escape")) {
			fpsController.enabled = true;
			enabled = false;
			GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls(ControlsUIController.ControlsType.NORMAL);
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
			case PlayerDeskState.WAITING:
				centerCameraToDesk();
				handleExit();
				break;
			case PlayerDeskState.LOOKING_AT_LECTURER:
				centerCameraToLecturer();
				break;
			default:
				break;
		}
	}
}

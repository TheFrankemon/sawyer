using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CurriculumMouseRotate : MonoBehaviour {

	private float rotationSpeed = 10.0F;
	private float lerpSpeed = 1.0F;
	
	private Vector3 theSpeed;
	private Vector3 avgSpeed;
	private bool isDragging = false;
	private Vector3 targetSpeedX;

	private Camera playerCamera;
	private FirstPersonController fpsController;
	private CurriculumScrollController curriculum2D;
	private DeskController deskController;
	private float defaultFOV;
	private Texture2D imgFront;
	private Texture2D imgBack;

	private float minFov = 15f;
	private float maxFov = 90f;
	private float sensitivity = 10f;

	private float speedH = 2.0f;
	private float speedV = 2.0f;
	private float maxYaw = 30f;
	private float maxPitch = 25f;
	
	private float yaw = 0.0f;
	private float pitch = 0.0f;

	private Vector3 angles;

	void Start () {
		playerCamera = GameObject.Find ("FPSController").GetComponentInChildren<Camera> ();
		curriculum2D = GameObject.Find ("Curriculum ScrollView").GetComponent<CurriculumScrollController> ();
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		defaultFOV = playerCamera.fieldOfView;
		Hide ();
	}
	
	void OnMouseDown() {
		isDragging = true;
	}
	
	void Update() {
		HandleRotation ();
		HandleCameraMovement ();

		if (Input.GetKeyDown ("escape")) {
			Hide();
			Back ();
		} else if (Input.GetKeyDown ("space")) {
			//Hide ();
			curriculum2D.Show(imgBack);
		}
	}

	private void HandleRotation() {
		if (Input.GetMouseButton (0) && isDragging) {
			theSpeed = new Vector3 (-Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0.0F);
			avgSpeed = Vector3.Lerp (avgSpeed, theSpeed, Time.deltaTime * 5);
		} else {
			if (isDragging) {
				theSpeed = avgSpeed;
				isDragging = false;
			}
			float i = Time.deltaTime * lerpSpeed;
			theSpeed = Vector3.Lerp (theSpeed, Vector3.zero, i);
		}
		
		transform.Rotate (playerCamera.transform.up * theSpeed.x * rotationSpeed, Space.World);
		transform.Rotate (playerCamera.transform.right * theSpeed.y * rotationSpeed, Space.World);
	}

	private void HandleCameraMovement() {
		float fov = playerCamera.fieldOfView;
		fov -= Input.GetAxis ("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp (fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
		
		yaw += speedH * Input.GetAxis ("Mouse X");
		pitch -= speedV * Input.GetAxis ("Mouse Y");
		
		if (pitch < -maxPitch) {
			pitch = -maxPitch;
		} else if (pitch > maxPitch) {
			pitch = maxPitch;
		}
		
		if (yaw < -maxYaw) {
			yaw = -maxYaw;
		} else if (yaw > maxYaw) {
			yaw = maxYaw;
		}
		
		playerCamera.transform.eulerAngles = new Vector3 (angles.x + pitch, angles.y + yaw, angles.z + 0.0f);
	}

	public void Show(string image, DeskController controller) {
		if (controller != null) {
			deskController = controller;
		}
		deskController.lookAtLecturer ();
		StartCoroutine (CenterCameraAndDisplay (image));
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls(ControlsUIController.ControlsType.CURRICULUM_ROTATE);
	}

	public IEnumerator CenterCameraAndDisplay(string image) {

		while (!deskController.isLookingAtLecturer()) {
			yield return new WaitForSeconds(0.3f);
		}

		transform.position = playerCamera.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane + 1));
		transform.LookAt (playerCamera.transform.position);
		enabled = true;

		fpsController.enabled = false;

		angles = playerCamera.transform.eulerAngles;


		if (image != null) {
			imgFront = Resources.Load<Texture2D> ("Image/Curriculum/" + image + "1");
			imgBack = Resources.Load<Texture2D> ("Image/Curriculum/" + image + "2");
		}

		MeshRenderer[] faces = GetComponentsInChildren<MeshRenderer>();
		faces [0].material.mainTexture = imgBack;
		faces [2].material.mainTexture = imgFront;
	}

	public void Hide() {
		transform.position = (new Vector3 (0, 0, 0));
		enabled = false;
		playerCamera.fieldOfView = defaultFOV;
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls (ControlsUIController.ControlsType.NORMAL);
	}

	public void Back() {
		if (deskController != null) {
			deskController.lookDown();
		}
	}
}

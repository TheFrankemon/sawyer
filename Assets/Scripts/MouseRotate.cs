using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MouseRotate : MonoBehaviour {

	private float rotationSpeed = 10.0F;
	private float lerpSpeed = 1.0F;
	
	private Vector3 theSpeed;
	private Vector3 avgSpeed;
	private bool isDragging = false;
	private Vector3 targetSpeedX;

	private Camera playerCamera;
	private GameObject bg;
	private FirstPersonController fpsController;
	private float defaultFOV;

	private float minFov = 15f;
	private float maxFov = 90f;
	private float sensitivity = 10f;

	private float speedH = 2.0f;
	private float speedV = 2.0f;
	private float maxYaw = 30f;
	private float maxPitch = 25f;
	
	private float yaw = 0.0f;
	private float pitch = 0.0f;

	void Start () {
		playerCamera = GameObject.Find ("FPSController").GetComponentInChildren<Camera> ();
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		bg = GameObject.Find ("Curriculum BG");
		defaultFOV = playerCamera.fieldOfView;;//playerCamera.orthographicSize;
		Hide ();
	}
	
	void OnMouseDown() {
		isDragging = true;
	}
	
	void Update() {
		
		if (Input.GetMouseButton(0) && isDragging) {
			theSpeed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0F);
			avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
		} else {
			if (isDragging) {
				theSpeed = avgSpeed;
				isDragging = false;
			}
			float i = Time.deltaTime * lerpSpeed;
			theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);
		}

		transform.Rotate(playerCamera.transform.up * theSpeed.x * rotationSpeed, Space.World);
		transform.Rotate(playerCamera.transform.right * theSpeed.y * rotationSpeed, Space.World);

		float fov  = playerCamera.fieldOfView;
		fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;

		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

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

		Debug.Log (yaw + " " + pitch);
		
		playerCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

		if (Input.GetKeyDown ("escape")) {
			Hide();
		}
	}

	public void Show() {
		transform.position = playerCamera.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane + 1));
		transform.LookAt (playerCamera.transform.position);
		enabled = true;

		bg.transform.position = playerCamera.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane + 2));
		bg.transform.LookAt (playerCamera.transform.position);
		bg.SetActive (true);

		fpsController.enabled = false;
	}

	public void Hide() {
		transform.position = (new Vector3 (0, 0, 0));
		enabled = false;
		bg.SetActive (false);
		fpsController.enabled = true;
		playerCamera.fieldOfView = defaultFOV;
	}
}

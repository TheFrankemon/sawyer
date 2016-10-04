using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class MapCameraController : MonoBehaviour {

	public Transform target;
	public int miniFOV = 40;
	public int fullFOV = 170;

	private Camera cam;
	private Rect miniRect;
	private Rect fullRect;
	private bool fullMap = false;
	private Quaternion fullMapRotation = Quaternion.Euler(90, 0, 0);
	private FirstPersonController fpsController;
	private ControlsUIController controlsUI;
	private Vector3 defaultPosition;
	private BlurOptimized cameraBlur;
	//private GameObject mapBackground;
//	private Vector3 fullMapPosition = new Vector3(250

	void Start() {
		cam = gameObject.GetComponent<Camera> ();
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		controlsUI	 = GameObject.FindGameObjectWithTag ("Player").GetComponent<ControlsUIController>();
		cameraBlur = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<BlurOptimized>();
		//mapBackground = GameObject.Find ("MapBackground");
		defaultPosition = new Vector3 (target.position.x, transform.position.y, target.position.z);
		float width = 0.15f;
		float height = width * 2;
		miniRect = new Rect (1 - width, 1 - height, width, height);
		fullRect = new Rect (-0.1f, 0, 1, 1);
		//cam.rect = new Rect (Screen.width - width, Screen.height - height, width, height);
		displayMiniMap ();
		//cameraBlur.enabled = false;
		//mapBackground.SetActive (false);
		//cam.rect = new Rect (0,0, width, width);

		//Debug.Log (width);
	}

	void Update() {
		if (controlsUI.getCurrentControls () == ControlsUIController.ControlsType.MAP ||
			controlsUI.getCurrentControls () == ControlsUIController.ControlsType.NORMAL) {
			if (Input.GetKeyDown ("m")) {

				if (!fullMap) {
					displayFullMap ();
					//fpsController.enabled = false;
					//mapBackground.SetActive(true);
					//cameraBlur.enabled = true;
				} else {
					displayMiniMap ();
					//fpsController.enabled = true;
					//cameraBlur.enabled = false;
					//mapBackground.SetActive(false);
				}
			}
			if (Input.GetKeyDown ("escape")) {
				//fullMap = false;
				displayMiniMap ();
				//fpsController.enabled = true;
				//cameraBlur.enabled = false;
			}
		}
	}

	public void displayFullMap() {
		cam.rect = fullRect;
		cam.orthographicSize = fullFOV;
		controlsUI.changeControls (ControlsUIController.ControlsType.MAP);
		fpsController.enabled = false;
		cameraBlur.enabled = true;
		fullMap = true;
	}

	public void displayMiniMap() {
		cam.rect = miniRect;
		cam.orthographicSize = miniFOV;
		controlsUI.changeControls (ControlsUIController.ControlsType.NORMAL);
		fpsController.enabled = true;
		cameraBlur.enabled = false;
		fullMap = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!fullMap) {
			transform.position = new Vector3 (target.position.x, transform.position.y, target.position.z);
			transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
		} else {
			transform.rotation = fullMapRotation;
			transform.position = defaultPosition;
		}
	}
}

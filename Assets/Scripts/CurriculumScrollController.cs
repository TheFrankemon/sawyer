using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class CurriculumScrollController : MonoBehaviour {

	/*private float rotationSpeed = 10.0F;
	private float lerpSpeed = 1.0F;
	
	private Vector3 theSpeed;
	private Vector3 avgSpeed;
	private bool isDragging = false;
	private Vector3 targetSpeedX;*/
	
	//private Camera playerCamera;
	private CurriculumMouseRotate curriculum3d;
	private Image curriculumImage;
	
	/*private float minFov = 15f;
	private float maxFov = 90f;
	private float sensitivity = 10f;
	
	private float speedH = 2.0f;
	private float speedV = 2.0f;
	private float maxYaw = 30f;
	private float maxPitch = 25f;
	
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	
	private Vector3 angles;*/
	
	void Start () {
		//playerCamera = GameObject.Find ("FPSController").GetComponentInChildren<Camera> ();
		//defaultFOV = playerCamera.fieldOfView;;//playerCamera.orthographicSize;
		curriculum3d = GameObject.Find ("Curriculum").GetComponent<CurriculumMouseRotate> ();
		curriculumImage = GameObject.Find ("Curriculum Big").GetComponent<Image> ();
		//rect.sizeDelta = new Vector2 (rect.sizeDelta.x, rect.sizeDelta.x * ratio);
		Hide ();
	}
	
	/*void OnMouseDown() {
		isDragging = true;
	}*/
	
	void Update() {

		//Debug.Log (theSpeed.y * rotationSpeed);

		//rect.position = new Vector3(rect.position.x, , rect.position.z);
		
		//transform.Rotate(playerCamera.transform.up * theSpeed.x * rotationSpeed, Space.World);
		//transform.Rotate(playerCamera.transform.right * theSpeed.y * rotationSpeed, Space.World);
		
		/*float fov  = playerCamera.fieldOfView;
		fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
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
		}*/

		//Debug.Log (yaw + " " + pitch);
		
		//playerCamera.transform.eulerAngles = new Vector3(angles.x + pitch, angles.y + yaw, angles.z + 0.0f);

		if (Input.GetKeyDown ("escape")) {
			Hide();
			Back();
		}
	}
	
	public void Show(Texture2D img) {
		//transform.position = playerCamera.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane + 1));
		//transform.LookAt (playerCamera.transform.position);
		enabled = true;
		GetComponent<Image>().enabled = true;
		curriculumImage.enabled = true;
		curriculumImage.sprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(.5f,.5f), 100);
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls(ControlsUIController.ControlsType.CURRICULUM_BIG);
		/*bg.transform.position = playerCamera.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, playerCamera.nearClipPlane + 2));
		bg.transform.LookAt (playerCamera.transform.position);
		bg.SetActive (true);*/
		//curriculum3d.enabled = false;
		
		//angles = playerCamera.transform.eulerAngles;
		
		//GetComponent<MeshRenderer> ().material = Resources.Load<Material>("Materials/Curriculum/" + image);
		//GetComponent<MeshRenderer> ().material.mainTexture = Resources.Load<Texture2D>("Image/Curriculum/" + image);
	}
	
	public void Hide() {
		//transform.position = (new Vector3 (0, 0, 0));
		enabled = false;
		GetComponent<Image>().enabled = false;
		//bg.SetActive (false);
		curriculumImage.enabled = false;
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls (ControlsUIController.ControlsType.NORMAL);
		//playerCamera.fieldOfView = defaultFOV;
	}

	public void Back() {
		curriculum3d.enabled = true;
		curriculum3d.Show (null, null);
	}
}

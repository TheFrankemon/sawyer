using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	private Camera cam;

	void Start() {
		cam = gameObject.GetComponent<Camera> ();
		float width = 0.15f;
		float height = width * 2;
		//cam.rect = new Rect (Screen.width - width, Screen.height - height, width, height);
		cam.rect = new Rect (1 - width, 1 - height, width, height);
		//cam.rect = new Rect (0,0, width, width);

		Debug.Log (width);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (target.position.x, transform.position.y, target.position.z);
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
	}
}

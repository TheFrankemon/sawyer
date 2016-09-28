using UnityEngine;
using System.Collections;

public class TotemIconGoTo : MonoBehaviour {

	public MapCameraController mapController;
	public GameObject target;
	public Material highlighter;
	public GameObject targetIcon;

	private Material[] defaultMaterials;
	private bool iconIsActive = false;

	void Start() {
		defaultMaterials = target.GetComponent<MeshRenderer>().materials;
		Debug.Log ("Materials = " + defaultMaterials.Length);
	}

	void OnMouseDown() {

		/*for (int i = 0; i < defaultMaterials.Length; i++) {
			target.GetComponent<MeshRenderer>().materials[i] = highlighter;
		}*/
		target.GetComponent<MeshRenderer> ().materials = new Material[] {highlighter};
		InvokeRepeating ("BlinkTargetIcon", 0, 0.5f);
		/*foreach (Transform t in target.transform) {
			if (t.gameObject.CompareTag("Map Icon")) {
				t.gameObject.SetActive(true);
			}
		}*/
		mapController.displayFullMap ();
		//mapController.enabled = false;
	}

	void Update() {
		if (Input.GetKeyDown ("escape") || Input.GetKeyDown("m")) {
			/*for (int i = 0; i < defaultMaterials.Length; i++) {
				target.GetComponent<MeshRenderer>().materials[i] = defaultMaterials[i];
			}*/
			target.GetComponent<MeshRenderer>().materials = defaultMaterials;
			CancelInvoke();
			/*foreach (Transform t in target.transform) {
				if (t.gameObject.CompareTag("Map Icon")) {
					t.gameObject.SetActive(false);
				}
			}*/
			iconIsActive = false;
			targetIcon.SetActive(iconIsActive);
			//mapController.displayMiniMap ();
			//mapController.enabled = true;
		}
	}

	void BlinkTargetIcon() {
		iconIsActive = !iconIsActive;
		targetIcon.SetActive (iconIsActive);
	}
}

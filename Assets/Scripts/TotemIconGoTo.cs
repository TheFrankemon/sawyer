using UnityEngine;
using System.Collections;

public class TotemIconGoTo : MonoBehaviour {

	public MapCameraController mapController;
	public GameObject target;
	public Material highlighter;

	private Material[] defaultMaterials;

	void Start() {
		defaultMaterials = target.GetComponent<MeshRenderer>().materials;
		Debug.Log ("Materials = " + defaultMaterials.Length);
	}

	void OnMouseDown() {

		/*for (int i = 0; i < defaultMaterials.Length; i++) {
			target.GetComponent<MeshRenderer>().materials[i] = highlighter;
		}*/
		target.GetComponent<MeshRenderer> ().materials = new Material[] {highlighter};
		foreach (Transform t in target.transform) {
			if (t.gameObject.CompareTag("Map Icon")) {
				t.gameObject.SetActive(true);
			}
		}
		mapController.displayFullMap ();
		//mapController.enabled = false;
	}

	void Update() {
		if (Input.GetKeyDown ("escape") || Input.GetKeyDown("m")) {
			/*for (int i = 0; i < defaultMaterials.Length; i++) {
				target.GetComponent<MeshRenderer>().materials[i] = defaultMaterials[i];
			}*/
			target.GetComponent<MeshRenderer>().materials = defaultMaterials;
			foreach (Transform t in target.transform) {
				if (t.gameObject.CompareTag("Map Icon")) {
					t.gameObject.SetActive(false);
				}
			}
			//mapController.displayMiniMap ();
			//mapController.enabled = true;
		}
	}
}

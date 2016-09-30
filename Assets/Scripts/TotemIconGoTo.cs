using UnityEngine;
using System.Collections;

public class TotemIconGoTo : MonoBehaviour {

	public MapCameraController mapController;
	public Material highlighter;
	public GameObject[] targets;
	public GameObject[] targetIcons;

	private Material[][] defaultMaterials;
	private bool iconIsActive = false;

	void Start() {
		defaultMaterials = new Material[targets.Length][];
		for (int i = 0; i < targets.Length; i++) {
			defaultMaterials[i] = targets[i].GetComponent<MeshRenderer>().materials;
		}

		//Debug.Log ("Materials = " + defaultMaterials.Length);
	}

	void OnMouseDown() {
		foreach (GameObject target in targets) {
			target.GetComponent<MeshRenderer> ().materials = new Material[] {highlighter};
		}
		InvokeRepeating ("BlinkTargetIcons", 0, 0.5f);
		mapController.displayFullMap ();
	}

	void Update() {
		if (Input.GetKeyDown ("escape") || Input.GetKeyDown("m")) {
			for (int i = 0; i < targets.Length; i++) {
				targets[i].GetComponent<MeshRenderer>().materials = defaultMaterials[i];
			}
			CancelInvoke();
			iconIsActive = false;
			foreach (GameObject icon in targetIcons) {
				icon.SetActive(iconIsActive);
			}
		}
	}

	void BlinkTargetIcons() {
		iconIsActive = !iconIsActive;
		foreach (GameObject icon in targetIcons) {
			icon.SetActive (iconIsActive);
		}
	}
}

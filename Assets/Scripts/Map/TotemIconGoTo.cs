using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotemIconGoTo : MonoBehaviour {

	public MapCameraController mapController;
	public GameObject[] targets;
	public GameObject[] targetIcons;
	public string text;

	private Material[][] defaultMaterials;
	private bool iconIsActive = false;

	void OnMouseEnter() {
		GameObject.Find ("Auxiliar Text").GetComponent<Text> ().text = text;
	}

	void OnMouseExit() {
		GameObject.Find ("Auxiliar Text").GetComponent<Text> ().text = "";
	}

	void OnMouseDown() {
		InvokeRepeating ("BlinkTargetIcons", 0, 0.5f);
		mapController.displayFullMap ();
	}

	void Update() {
		if (Input.GetKeyDown ("escape") || Input.GetKeyDown("m")) {
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlsUIController : MonoBehaviour {

	public enum ControlsType {
		NORMAL, DESK, CURRICULUM_ROTATE, CURRICULUM_BIG, CONVERSATION
	}

	private GameObject controlsPanel;
	private Dictionary<ControlsType, string> controlsNames = new Dictionary<ControlsType, string>();
	private bool showing = false;

	// Use this for initialization
	void Start () {
		controlsPanel = GameObject.Find ("ControlsUI");

		controlsPanel.SetActive (showing);

		controlsNames.Add (ControlsType.NORMAL, "Normal");
		controlsNames.Add (ControlsType.DESK, "Desk");
		controlsNames.Add (ControlsType.CURRICULUM_ROTATE, "CurriculumRotate");
		controlsNames.Add (ControlsType.CURRICULUM_BIG, "CurriculumBig");
		controlsNames.Add (ControlsType.CONVERSATION, "Conversation");

		changeControls (ControlsType.NORMAL);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("h")) {
			showing = !showing;

			controlsPanel.SetActive(showing);
		}
	}

	public void changeControls(ControlsType currentControls) {
		foreach (Transform t in controlsPanel.transform) {
			if (t.name == controlsNames[currentControls]) {
				t.gameObject.SetActive(true);
			} else {
				t.gameObject.SetActive(false);
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InputController : MonoBehaviour {

	private InputField inputField;
	private FirstPersonController fpsController;
	private bool isWaiting;

	void Start() {
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		inputField = GameObject.Find ("Name Input Field").GetComponent<InputField> ();
		WaitForInput ();
	}

	void WaitForInput() {
		fpsController.enabled = false;
		isWaiting = true;
		Debug.Log ("Waiting for name");
	}

	// Update is called once per frame
	void Update () {
		if (isWaiting) {
			if (Input.GetKey ("return")) {
				if (checkInputText ()) {
					PlayerInfo.setName (inputField.text);
					fpsController.enabled = true;
					this.enabled = false;
					Destroy(GameObject.Find ("Logo"));
					Destroy(GameObject.Find ("Name Input Field"));
				}
			}
		}
	}

	bool checkInputText() {
		if (inputField.text.Trim ().Equals("")) {
			return false;
		} else {
			return true;
		}
	}
}

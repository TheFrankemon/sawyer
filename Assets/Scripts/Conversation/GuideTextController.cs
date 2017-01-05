using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class GuideTextController : ConversationController {

	private GameObject[] sameTagList;
	
	void Start() {
		BaseStart ();
		sameTagList = GameObject.FindGameObjectsWithTag (gameObject.tag);	
	}
	
	void Update () {
		if (isTalking) {
			if (Input.GetKeyDown("space")) {
				if (textIsScrolling) {
					textGUI.text = currentMessage.getText();
					textIsScrolling = false;
					audioSource.Stop();
				} else {
					if (currentConversation.hasNext()) {
						currentMessage = currentConversation.getNext();
						StartCoroutine(startScrolling());
					} else {
						stop();
					}
				}
			} else if (Input.GetKeyDown("escape")) {
				if (textIsScrolling) {
					textGUI.text = currentMessage.getText();
					textIsScrolling = false;
					audioSource.Stop();
				}
				stop();
			}
		}
	}
	
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			fpsController.enabled = false;
			StartCoroutine(waitForAnim(anim));
			GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls(ControlsUIController.ControlsType.CONVERSATION);
		}
	}
	
	protected override void stop() {
		base.stop ();
		fpsController.enabled = true;
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls (ControlsUIController.ControlsType.NORMAL);
		Destroy (gameObject);
		foreach(GameObject obj in sameTagList) {
			Destroy(obj);
		}
	}

	protected override IEnumerator waitForAnim(Animation anim) {
		currentConversation = JSONParser.createGuideText(textPathName);
		return base.waitForAnim (anim);
	}
}

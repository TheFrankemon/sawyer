using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class GuideTextController : MonoBehaviour {

	public int textScrollSpeed;
	public string textPathName;

	private bool isTalking;
	private bool textIsScrolling;
	private FirstPersonController fpsController;
	private Conversation currentConversation;
	private Message currentMessage;
	private Animation anim;
	private Text textGUI;
	private Text nameGUI;
	private Image image;
	private AudioSource audioSource;
	private GameObject[] sameTagList;
	
	void Start() {
		audioSource = GameObject.Find ("FPSController").GetComponent<AudioSource> ();
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		anim = GameObject.Find ("TxtGUI").GetComponent<Animation> ();
		textGUI = GameObject.Find ("NPC Text").GetComponent<Text> ();
		nameGUI = GameObject.Find ("NPC Name").GetComponent<Text> ();
		image = GameObject.Find ("NPC Image").GetComponent<Image> ();
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
	
	IEnumerator waitForAnim(Animation anim) {
		anim.Play("UIBegin");
		currentConversation = JSONParser.createGuideText(textPathName);
		Texture2D texture = Resources.Load<Texture2D>(currentConversation.getImg());
		image.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f,.5f), 100);
		currentMessage = currentConversation.getNext();
		do {
			yield return null;
		} while (anim.isPlaying);
		nameGUI.text = currentConversation.getName();
		image.enabled = true;
		isTalking = true;
		StartCoroutine (startScrolling());
	}
	
	IEnumerator startScrolling() {
		audioSource.clip = currentMessage.getAudio();
		audioSource.Play ();
		
		textIsScrolling = true;
		string startText = currentMessage.getText();
		string displayText = "";
		
		for (int i = 0; i < currentMessage.getText().Length; i++) {
			if (textIsScrolling && currentMessage.getText() == startText) {
				displayText += currentMessage.getText()[i];
				textGUI.text = displayText;
				yield return new WaitForSeconds (1 / textScrollSpeed);
			} else {
				return false;
			}
		}		
	}
	
	void stop() {
		textGUI.text = "";
		nameGUI.text = "";
		isTalking = false;
		fpsController.enabled = true;
		image.enabled = false;
		anim.Play ("UIEnd");
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls (ControlsUIController.ControlsType.NORMAL);
		Destroy (gameObject);
		foreach(GameObject obj in sameTagList) {
			Destroy(obj);
		}
	}
}

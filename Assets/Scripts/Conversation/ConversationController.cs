using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class ConversationController : MonoBehaviour {

	public int textScrollSpeed;
	public string textPathName;

	protected bool isTalking;
	protected bool textIsScrolling;
	protected FirstPersonController fpsController;
	protected Conversation currentConversation;
	protected Message currentMessage;
	protected Animation anim;
	protected Text textGUI;
	protected Text nameGUI;
	protected Image image;
	protected AudioSource audioSource;

	protected void BaseStart() {
		audioSource = GameObject.Find ("FPSController").GetComponent<AudioSource> ();
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		anim = GameObject.Find ("TxtGUI").GetComponent<Animation> ();
		textGUI = GameObject.Find ("NPC Text").GetComponent<Text> ();
		nameGUI = GameObject.Find ("NPC Name").GetComponent<Text> ();
		image = GameObject.Find ("NPC Image").GetComponent<Image> ();
	}

	protected virtual IEnumerator waitForAnim(Animation anim) {
		anim.Play("UIBegin");
		//currentConversation = JSONParser.createConversation(textPathName);
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

	protected IEnumerator startScrolling() {
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

		textIsScrolling = false;
	}

	protected virtual void stop() {
		textGUI.text = "";
		nameGUI.text = "";
		isTalking = false;
		image.enabled = false;
		anim.Play ("UIEnd");
	}
}

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class DeskConversationController : ConversationController {

	public Button answerButton;

	protected bool isWaiting;
	protected List<Button> answers;
	protected List<Conversation> nextConversations;
	protected DeskController deskController;

	private RectTransform canvas;
	
	void Start() {
		BaseStart ();
		answers = new List<Button>();
		nextConversations = new List<Conversation>();
		canvas = GameObject.Find ("PanelW").GetComponent<RectTransform> ();
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
					} else if (currentConversation.hasAnswers()) {
						if (!isWaiting) {
							isWaiting = true;
							createAnswerButtons();
						}
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
				if (currentConversation.hasAnswers()) {
					isWaiting = true;
					createAnswerButtons();
				} else {
					stop();
				}
			}
		}
	}

	public void StartConversation(DeskController controller) {
		if (controller != null) {
			deskController = controller;
		}
		fpsController.enabled = false;
		deskController.lookAtLecturer ();
		StartCoroutine(MoveToLecturer());
		StartCoroutine(waitForAnim(anim));
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls(ControlsUIController.ControlsType.CONVERSATION);
	}

	IEnumerator MoveToLecturer() {
		while (!deskController.isLookingAtLecturer()) {
			/*deskController.centerCameraToLecturer();*/
			yield return new WaitForSeconds(1f);
		}
		//waitForAnim (anim);
	}
	
	void createAnswerButtons() {
		textGUI.text = "";
		float width = canvas.rect.width;
		float height = canvas.rect.height;
		float btnWidth = width / 4;
		float btnHeight = height / 8;
		//float dX = width / 10;
		//float dY = height / 10;
		float dX = btnWidth / 2;
		float dY = btnHeight / 2;
		float startX = (canvas.position.x - width / 2);
		float startY = (canvas.position.y + height / 6) + dY / 2;
		float posX = startX;
		float posY = startY;
		//dX += answerButton.GetComponent<RectTransform> ().rect.width;
		//dY += answerButton.GetComponent<RectTransform> ().rect.height;
		//answerButton.GetComponent<RectTransform> ().sizeDelta = new Vector2
		answerButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (btnWidth, btnHeight);
		float maxX = canvas.position.x + width / 2;
		float maxY = canvas.position.y + height / 2;
		foreach (Answer cur in currentConversation.getAnswers()) {
			Button ansBut = (Button) Instantiate (answerButton, new Vector2(posX, posY), Quaternion.identity);
			ansBut.gameObject.GetComponentInChildren<Text>().text = cur.getText();
			ansBut.transform.parent = GameObject.Find("Canvas").transform;
			nextConversations.Add(cur.getNextConversation());
			answers.Add(ansBut);
			posX += dX + btnWidth;
			if (posX >= maxX) {
				posX = startX;
				posY -= dY + btnHeight;
			}
		}
		for (int j = 0; j < answers.Count; j++) {
			Conversation conv = nextConversations[j];
			answers[j].onClick.RemoveAllListeners();
			answers[j].onClick.AddListener(() => changeConversation(conv));
		}
		//Debug.Log(answers.Count);
	}
	
	protected override void stop() {
		base.stop ();
		currentConversation.reset ();
		foreach (Button ans in answers) {
			Destroy (ans.gameObject);
		}
		answers.Clear ();
		nextConversations.Clear ();
		isWaiting = false;
		//fpsController.enabled = true;
		//GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls (ControlsUIController.ControlsType.NORMAL);
		deskController.lookDown ();
	}
	
	void changeConversation(Conversation next) {
		currentConversation.reset ();
		currentConversation = next;
		foreach (Button ans in answers) {
			Destroy (ans.gameObject);
		}
		answers.Clear ();
		nextConversations.Clear ();
		currentMessage = currentConversation.getNext();
		StartCoroutine (startScrolling());
		isWaiting = false;
	}

	protected override IEnumerator waitForAnim(Animation anim) {
		currentConversation = JSONParser.createConversation(textPathName);
		return base.waitForAnim (anim);
	}
}

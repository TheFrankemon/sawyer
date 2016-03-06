using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class ConversationMovieController : MonoBehaviour {
	public int textScrollSpeed;
	public string textPathName;
	public Button answerButton;

	private bool isTalking;
	private bool isWaiting;
	private bool textIsScrolling;
	private FirstPersonController fpsController;
	private List<Button> answers;
	private List<Conversation> nextConversations;
	private List<string> urls;
	private Conversation currentConversation;
	private Message currentMessage;
	private Animation anim;
	private Text textGUI;
	private Text nameGUI;
	private Image image;
	private AudioSource audioSource;
	private RectTransform canvas;
	
	void Start() {
		answers = new List<Button>();
		nextConversations = new List<Conversation>();
		urls = new List<string>();
		audioSource = GameObject.Find ("FPSController").GetComponent<AudioSource> ();
		fpsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<FirstPersonController>();
		anim = GameObject.Find ("TxtGUI").GetComponent<Animation> ();
		canvas = GameObject.Find ("PanelW").GetComponent<RectTransform> ();
		Debug.Log (canvas.rect.width);
		Debug.Log (canvas.rect.height);
		textGUI = GameObject.Find ("NPC Text").GetComponent<Text> ();
		nameGUI = GameObject.Find ("NPC Name").GetComponent<Text> ();
		image = GameObject.Find ("NPC Image").GetComponent<Image> ();
	}
	
	void Update () {
		if (MovieController.isRunning ()) {
			Debug.Log("Playing video");
			if (Input.GetKeyDown("escape")) {
				Debug.Log("escape");
				MovieController.StopMovie();
				MovieController.Stop();
			}
			if (Input.GetKeyDown("space")) {
				if (!MovieController.isPaused()) {
					Debug.Log("pause");
					MovieController.PauseMovie();
				} else {
					Debug.Log("play");
					MovieController.PlayMovie();
				}
			}
			if (!MovieController.isPlaying() && !MovieController.isPaused()) {
				Debug.Log("finished");
				MovieController.StopMovie();
				MovieController.Stop();
			}
			Debug.Log(MovieController.isRunning());
		} else if (isTalking) {
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
							Debug.Log("crear");
							isWaiting = true;
							createAnswerButtons();
						}
					} else {
						stop();
					}
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			//currentConversation = JSONParser.createDefaultConversation ();
			//anim.enabled = true;
			
			
			fpsController.enabled = false;
			StartCoroutine(waitForAnim(anim));
			//contGUI.SetActive(true);
			/*image.enabled = true;
			currentConversation = JSONParser.createConversation(npcText);
			Texture2D texture = Resources.Load<Texture2D>(currentConversation.getImg());
			image.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f,.5f), 100);
			nameGUI.text = currentConversation.getName();

			isTalking = true;
			currentMessage = currentConversation.getNext();
			StartCoroutine (startScrolling());*/
			
		}
	}
	
	IEnumerator waitForAnim(Animation anim) {
		anim.Play("UIBegin");
		currentConversation = JSONMovieParser.createConversation(textPathName);
		Texture2D texture = Resources.Load<Texture2D>(currentConversation.getImg());
		image.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f,.5f), 100);
		currentMessage = currentConversation.getNext();
		do
		{
			yield return null;
		} while (anim.isPlaying);
		nameGUI.text = currentConversation.getName();
		image.enabled = true;
		isTalking = true;
		StartCoroutine (startScrolling());
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
			Debug.Log(cur.getUrl());
			urls.Add(cur.getUrl());
			answers.Add(ansBut);
			posX += dX + btnWidth;
			if (posX >= maxX) {
				posX = startX;
				posY -= dY + btnHeight;
			}
		}
		for (int j = 0; j < answers.Count; j++) {
			Conversation conv = nextConversations[j];
			string url = urls[j];
			answers[j].onClick.RemoveAllListeners();
			if (url.Equals("")) {
				answers[j].onClick.AddListener(() => changeConversation(conv));
			} else {
				answers[j].onClick.AddListener(() => playVideo(url, conv));
			}
		}
		Debug.Log(answers.Count);
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
		
		textIsScrolling = false;
	}
	
	void stop() {
		currentConversation.reset ();
		textGUI.text = "";
		nameGUI.text = "";
		isTalking = false;
		fpsController.enabled = true;
		foreach (Button ans in answers) {
			Destroy (ans.gameObject);
		}
		answers.Clear ();
		nextConversations.Clear ();
		urls.Clear ();
		isWaiting = false;
		image.enabled = false;
		//anim.SetTrigger ("end");
		anim.Play ("UIEnd");
		//anim.enabled = false;
	}
	
	void changeConversation(Conversation next) {
		currentConversation.reset ();
		currentConversation = next;
		foreach (Button ans in answers) {
			Destroy (ans.gameObject);
		}
		answers.Clear ();
		nextConversations.Clear ();
		urls.Clear ();
		currentMessage = currentConversation.getNext();
		StartCoroutine (startScrolling());
		isWaiting = false;
	}

	void playVideo(string url, Conversation next) {
		/*currentConversation.reset ();
		currentConversation = next;
		foreach (Button ans in answers) {
			Destroy (ans.gameObject);
		}*/
		Debug.Log("Starting to play video");
		StartCoroutine (MovieController.LoadMovie (url));
		changeConversation (next);
	}
}

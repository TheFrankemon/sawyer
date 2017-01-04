using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation {

	private List<Message> messages;
	private List<Answer> answers;
	private int currentMessage;
	private string name;
	private string img;

	public Conversation(string name, string img) {
		messages = new List<Message>();
		answers = new List<Answer>();
		this.name = name;
		this.img = img;
		currentMessage = -1;
	}

	public bool hasNext() {
		return currentMessage < messages.Count - 1;
	}

	public Message getNext() {
		currentMessage++;
		if (currentMessage < messages.Count) {
			return messages [currentMessage];
		} else {
			return null;
		}
	}

	public List<Answer> getAnswers() {
		return answers;
	}

	public void addMessage(Message message) {
		messages.Add (message);
	}

	public void addAnswer(Answer answer) {
		answers.Add (answer);
	}

	public bool hasAnswers() {
		return answers.Count > 0;
	}

	public string getFirstText() {
		return messages [0].getText ();
	}

	public void reset() {
		currentMessage = -1;
	}

	public string getName() {
		return name;
	}

	public string getImg() {
		return img;
	}
}

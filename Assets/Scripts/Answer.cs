using UnityEngine;
using System.Collections;

public class Answer {

	private string text;
	private Conversation nextConversation;

	public Answer(string text) {
		this.text = text;
	}

	public string getText() {
		return text;
	}

	public Conversation getNextConversation() {
		return nextConversation;
	}

	public void setNextConversation(Conversation conv) {
		nextConversation = conv;
	}
}

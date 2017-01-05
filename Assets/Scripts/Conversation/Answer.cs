using UnityEngine;
using System.Collections;

public class Answer {

	private string text;
	private Conversation nextConversation;
	private string url;

	public Answer(string text, string url) {
		this.text = text;
		this.url = url;
	}

	public string getText() {
		return text;
	}

	public string getUrl() {
		return url;
	}

	public Conversation getNextConversation() {
		return nextConversation;
	}

	public void setNextConversation(Conversation conv) {
		nextConversation = conv;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public static class JSONParser {

	public static Conversation createConversation (string fileName) {
		string jsonString = readTextFile (/*"Assets/Resources/"*/"Text/" + fileName/* + ".json"*/);
		var N = JSON.Parse (jsonString);

		string name = N ["name"].Value;
		string img = N ["img"].Value;

		List<Conversation> conversations = new List<Conversation>();
		List<Answer> answers = new List<Answer>();

		Conversation start = new Conversation (name, img);

		for (int i = 0; i < N["start"].Count; i++) {
			Message msg = createMessage(N["start"][i]);
			start.addMessage(msg);
		}

		conversations.Add(start);

		for (int i = 0; i < N["ans"].Count; i++) {
			Conversation conv = new Conversation(name, img);
			Answer ans = new Answer(N["ans"][i]["text"].Value, N["ans"][i]["url"].Value);

			for (int j = 0; j < N["ans"][i]["nans"].Count; j++) {
				Message msg = createMessage(N["ans"][i]["nans"][j]);
				conv.addMessage(msg);
			}

			conversations.Add(conv);
			ans.setNextConversation(conv);
			answers.Add(ans);
		}

		for (int i = 0; i < conversations.Count - 1; i++) {
			foreach (Answer ans in answers) {
				conversations[i].addAnswer(ans);
			}
		}

		return conversations [0];
	}

	public static Conversation createGuideText (string fileName) {
		Debug.Log ("Text/" + fileName + ".json");
		string jsonString = readTextFile (/*"Assets/Resources/*/"Text/" + fileName/* + ".json"*/);
		var N = JSON.Parse (jsonString);
		
		string name = N ["name"].Value;
		string img = N ["img"].Value;
		
		Conversation conv = new Conversation (name, img);
		
		for (int i = 0; i < N["messages"].Count; i++) {
			Message msg = createMessage(N["messages"][i]);
			conv.addMessage(msg);
		}

		return conv;
	}

	private static Message createMessage (JSONNode msg) {
		return new Message (msg ["text"].Value, msg ["audio"].Value);
	}
	
	static string readTextFile(string filePath) {
		TextAsset readText = Resources.Load<TextAsset> (filePath);

		return readText.text;
	}
}

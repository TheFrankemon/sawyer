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
			Answer ans = new Answer(N["ans"][i]["text"].Value);

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
	
	/*public static Conversation createDefaultConversation() {
		Conversation start = new Conversation ("", "");
		Message message1 = new Message ("Bienvenido a la UPB");
		Message message2 = new Message ("Soy el decano del area de admisiones");
		Message moreInfo = new Message ("Desea averiguar informacion sobre algun tema?");
		start.addMessage (message1);
		start.addMessage (message2);
		start.addMessage (moreInfo);

		Answer answer1 = new Answer ("PAA");
		Answer answer2 = new Answer ("100 mejores");
		Answer answer3 = new Answer ("No, gracias");

		Conversation paa = new Conversation ("", "");
		Message message4 = new Message ("Prueba de aptitud academica");
		Message message5 = new Message ("Bla bla bla");
		paa.addMessage (message4);
		paa.addMessage (message5);
		paa.addMessage (moreInfo);

		answer1.setNextConversation (paa);

		Conversation best100 = new Conversation ("", "");
		Message message6 = new Message ("100 mejores");
		Message message7 = new Message ("Bla bla bla");
		best100.addMessage (message6);
		best100.addMessage (message7);
		best100.addMessage (moreInfo);

		answer2.setNextConversation (best100);

		Conversation bye = new Conversation ("", "");
		Message message8 = new Message ("Adios");
		bye.addMessage (message8);

		answer3.setNextConversation (bye);

		start.addAnswer (answer3);
		start.addAnswer (answer2);
		start.addAnswer (answer1);

		paa.addAnswer (answer3);
		paa.addAnswer (answer2);
		paa.addAnswer (answer1);

		best100.addAnswer (answer3);
		best100.addAnswer (answer2);
		best100.addAnswer (answer1);

		return start;
	}*/

	static string readTextFile(string filePath)
	{
		/*StreamReader stm = new StreamReader(filePath);
		string text = "";

		while(!stm.EndOfStream) {
			text += stm.ReadLine( );
		}
		
		stm.Close ();*/

		TextAsset readText = Resources.Load<TextAsset> (filePath);

		return readText.text;
	}
}

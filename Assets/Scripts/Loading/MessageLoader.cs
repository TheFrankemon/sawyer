using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class MessageLoader {

	private List<string> messages = new List<string>();
	private List<string> backup = new List<string> ();
	private System.Random random = new System.Random();
	private static MessageLoader instance;

	private MessageLoader ()	{
		string text = Resources.Load<TextAsset> ("Text/loading/messages").text;
		string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
		backup = lines.ToList ();
		reFill ();
	}

	public static MessageLoader Instance {
		get {
			if (instance == null) {
				instance = new MessageLoader();
			}
			return instance;
		}
	}

	void reFill() {
		messages = backup.Select(item => (string)item.Clone()).ToList();
	}

	public string getRandomMessage() {
		int messageNumber = random.Next (0, messages.Count);
		string message = messages [messageNumber];
		messages.RemoveAt (messageNumber);

		if (messages.Count == 0) {
			reFill();
		}

		return message;
	}
}
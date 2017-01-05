using UnityEngine;
using System.Collections;

public class Message {

	private string url;
	private AudioClip audio;
	private string text;

	public Message(string text, string audio) {
		this.text = string.Format(text, PlayerInfo.getName());
		this.audio = Resources.Load<AudioClip>(audio);
	}

	public Message(string text, string audio, string url) {
		this.text = string.Format(text, PlayerInfo.getName());
		this.audio = Resources.Load<AudioClip>(audio);
	}

	public string getText() {
		return text;
	}

	public AudioClip getAudio() {
		return audio;
	}

	public string getUrl() {
		return url;
	}
}

using System;

public class VideoMessage : Message {

	private string url;

	public VideoMessage (string text, string audio, string url) : base (text, audio) {
		this.url = url;
	}

	public string getUrl() {
		return url;
	}
}

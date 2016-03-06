using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovieController : MonoBehaviour {

	private static MovieTexture movieTexture;
	//public string url = "http://becunningandfulloftricks.files.wordpress.com/2013/04/hedgehog_in_the_fog.ogg";
	private static AudioSource audioSource;
	private static Image loadImg;
	private static Color loadColor;
	private static RawImage img;
	private static bool running;
	private static bool paused;
	private static float dload;

	// Use this for initialization
	//void Start () {
	static MovieController() {
		/*WWW www = new WWW (url);
		movieTexture = www.movie;
		StartCoroutine (LoadMovie());*/
		audioSource = GameObject.Find ("VideoScreen").GetComponent<AudioSource> ();
		img = GameObject.Find ("VideoScreen").GetComponent<RawImage> ();
		loadImg = GameObject.Find ("LoadImage").GetComponent<Image> ();
		loadColor = loadImg.color;
		loadColor.a = 0;
		running = false;
		paused = false;
		dload = 0.03f;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static bool isRunning() {
		return running;
	}

	public static bool isPlaying() {
		return movieTexture.isPlaying;
	}

	public static bool isPaused() {
		return paused;
	}

	public static void PlayMovie() {
		/*WWW www = new WWW (url);
		MovieTexture movieTexture = www.movie;

		while(!movieTexture.isReadyToPlay) {}

		GetComponent<Renderer> ().material.mainTexture = movieTexture;
		GetComponent<AudioSource> ().clip = movieTexture.audioClip;*/

		paused = false;
		movieTexture.Play ();
		audioSource.Play ();
	}

	/*IEnumerator LoadMovie() {
		
		while(!movieTexture.isReadyToPlay) {
			yield return null;
		}

		//GetComponent<Renderer> ().material.mainTexture = movieTexture;
		img.texture = movieTexture;
		audioSource.clip = movieTexture.audioClip;
		PlayMovie ();
	}*/

	public static IEnumerator LoadMovie(string url) {
		WWW www = new WWW (url);
		movieTexture = www.movie;
		running = true;
		paused = true;
		loadImg.enabled = true;
		
		while(!movieTexture.isReadyToPlay) {
			if (loadColor.a < 0 || loadColor.a > 0.5) {
				dload *= -1;
			}
			loadColor.a += dload;
			loadImg.color = loadColor;
			Debug.Log(loadImg.color.a);
			yield return null;
		}

		loadImg.enabled = false;

		img.texture = movieTexture;
		audioSource.clip = movieTexture.audioClip;
		img.enabled = true;
		PlayMovie ();
	}

	public static void PauseMovie() {
		paused = true;
		movieTexture.Pause ();
		audioSource.Pause ();
	}

	public static void StopMovie() {
		//running = false;
		paused = false;
		movieTexture.Stop ();
		audioSource.Stop ();
	}

	public static void Stop() {
		img.enabled = false;
		running = false;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovieController : MonoBehaviour {

	private static MovieTexture movieTexture;
	//public string url = "http://becunningandfulloftricks.files.wordpress.com/2013/04/hedgehog_in_the_fog.ogg";
	private static AudioSource audioSource;
	private static Image loadImg;
	private static Image iconImg;
	private static Color loadColor;
	private static RawImage img;
	private static bool running;
	private static bool paused;
	private static float dload;
	private static int iconTime;
	private static Texture2D pauseSprite;
	private static Texture2D playSprite;

	// Use this for initialization
	//void Start () {
	static MovieController() {
		/*WWW www = new WWW (url);
		movieTexture = www.movie;
		StartCoroutine (LoadMovie());*/
		audioSource = GameObject.Find ("VideoScreen").GetComponent<AudioSource> ();
		img = GameObject.Find ("VideoScreen").GetComponent<RawImage> ();
		iconImg = GameObject.Find ("VideoIcon").GetComponent<Image> ();
		loadImg = GameObject.Find ("LoadImage").GetComponent<Image> ();
		pauseSprite = Resources.Load<Texture2D>("Image/Icons/PauseIcon");
		playSprite = Resources.Load<Texture2D>("Image/Icons/PlayIcon");
		loadColor = loadImg.color;
		loadColor.a = 0;
		running = false;
		paused = false;
		dload = 0.03f;
		iconTime = 0;
	}
	
	// Update is called once per frame
	public static void Update () {
		Debug.Log ("showingIcon");
		if (iconTime > 0) {
			iconTime--;
		} else {
			iconImg.enabled = false;
			loadImg.enabled = false;
		}
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

		iconTime = 10;
		iconImg.sprite = Sprite.Create(playSprite, new Rect(0, 0, playSprite.width, playSprite.height), new Vector2(.5f,.5f), 100);
		iconImg.enabled = true;
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

		
		while(!movieTexture.isReadyToPlay) {
			loadImg.enabled = true;
			if (loadColor.a < 0 || loadColor.a > 0.5) {
				dload *= -1;
			}
			loadColor.a += dload;
			loadImg.color = loadColor;
			yield return null;
		}

		loadImg.enabled = false;

		img.texture = movieTexture;
		audioSource.clip = movieTexture.audioClip;
		img.enabled = true;
		PlayMovie ();
	}

	public static void PauseMovie() {
		iconTime = 60;
		loadImg.enabled = true;

		loadColor.a = 0.8f;
		loadImg.color = loadColor;		

		iconImg.sprite = Sprite.Create(pauseSprite, new Rect(0, 0, pauseSprite.width, pauseSprite.height), new Vector2(.5f,.5f), 100);
		iconImg.enabled = true;
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

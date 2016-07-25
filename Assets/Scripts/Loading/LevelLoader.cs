using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public Texture2D emptyProgressBar;
	public Texture2D fullProgressBar;
	public int nextLevelNumber;
	public Button nextLevelButton;
	public GUIStyle textStyle;

	private bool finishedLoading = false;
	
	void Start () {
		nextLevelButton.gameObject.SetActive (false);
	}

	void OnGUI () {
		GUI.DrawTexture (new Rect (0, Screen.height - 30, Screen.width, 30), emptyProgressBar);
		GUI.DrawTexture (new Rect (0, Screen.height - 30, Screen.width * Application.GetStreamProgressForLevel (nextLevelNumber), 30), fullProgressBar);
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 30, 100, 30),  ((int) (Application.GetStreamProgressForLevel(nextLevelNumber) * 100)).ToString(), textStyle);
		if (!finishedLoading) {	
			if (Application.CanStreamedLevelBeLoaded (nextLevelNumber)) {
				finishedLoading = true;
				nextLevelButton.gameObject.SetActive (true);
			}
		}
	}

	public void loadNextLevel() {
		Application.LoadLevel (nextLevelNumber);
	}
}

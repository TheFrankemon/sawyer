using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public float barHeight;
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
		GUI.DrawTexture (new Rect (0, Screen.height - barHeight, Screen.width, barHeight), emptyProgressBar);
		GUI.DrawTexture (new Rect (0, Screen.height - barHeight, Screen.width * Application.GetStreamProgressForLevel (nextLevelNumber), barHeight), fullProgressBar);

		int amountLoaded = ((int)(Application.GetStreamProgressForLevel (nextLevelNumber) * 100));
		Rect textRect = new Rect (Screen.width / 2 - 100, Screen.height - barHeight, 200, barHeight);
		if (amountLoaded == 100) {
			GUI.Label (textRect, "Carga terminada", textStyle);
		} else {
			GUI.Label (textRect, amountLoaded + "%", textStyle);
		}
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

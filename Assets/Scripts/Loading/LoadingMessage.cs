using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingMessage : MonoBehaviour {

	bool appeared = false;
	float currentScale = 10;

	public Color32 fontColor;
	public Color32 backgroundColor;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
		transform.SetParent (GameObject.Find ("Canvas").transform);
		GetComponentInChildren<Text> ().text = MessageLoader.Instance.getRandomMessage ();
		//Debug.Log (GetComponentInChildren<Image> ());

		Debug.Log (fontColor);
		Debug.Log (backgroundColor);

		GetComponentInChildren<Image> ().color = backgroundColor;
		GetComponentInChildren<Text> ().color = fontColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (!appeared) {
			if (currentScale <= 1) {
				currentScale = 1;
				appeared = true;
			} else {
				currentScale -= 1f;
			}
			transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
		}
	}

	public void setColors(Color32 font, Color32 background) {
		//Debug.Log (GetComponentInChildren<Image> ());
		//GetComponentInChildren<Image> ().color = background;
		//GetComponentInChildren<Text> ().color = font;
		fontColor = font;
		backgroundColor = background;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeImageOut : MonoBehaviour {

	private Image img;

	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
		img.CrossFadeAlpha (0, 10f, false);
	}
}

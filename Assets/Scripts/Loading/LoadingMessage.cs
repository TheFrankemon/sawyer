using UnityEngine;
using System.Collections;

public class LoadingMessage : MonoBehaviour {

	bool appeared = false;
	float currentScale = 0;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
		transform.SetParent (GameObject.Find ("Canvas").transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (!appeared) {
			if (currentScale >= 1) {
				currentScale = 1;
				appeared = true;
			} else {
				currentScale += 0.05f;
			}
			transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
		}
	}
}

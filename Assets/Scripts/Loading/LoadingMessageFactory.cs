using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingMessageFactory : MonoBehaviour {

	public GameObject message;
	public float spawnTime = 5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("createLoadingMessage", spawnTime, spawnTime);
		Debug.Log ("Width: " + Screen.width);
		Debug.Log ("Height: " + Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createLoadingMessage() {
		//GameObject newMessage = message;
		//Debug.Log (message);
		//newMessage.GetComponentInChildren<Text> ().text = "Holiiiiiiiiiiiiiiiiiiiiiiiii";
		//Sprite newSprite = Resources.Load<Sprite> ("UI/Triangle1");
		//Debug.Log (newSprite);
		//newMessage.GetComponentInChildren<Image> ().sprite = Resources.Load<Sprite> ("UI/Triangle1");
		Instantiate (message, getRandomPosition(), getRandomRotation());
	}

	Vector3 getRandomPosition() {
		float x = Random.Range (230, Screen.width - 200);
		float y = Random.Range (-70, -(Screen.height - 170)) + 371;
		Vector3 pos = new Vector3 (x, y, 0);
		Debug.Log ("x: " + x);
		Debug.Log ("y: " + y);
		Debug.Log (pos.ToString ());
		//Vector3 pos = new Vector3 (Random.Range (230, Screen.width - 200), Random.Range (70, (Screen.height - 170)), 0);
		return pos;
	}

	Quaternion getRandomRotation() {
		Quaternion rot = Quaternion.Euler (0, 0, Random.Range (-60f, 0f));
		return rot;
	}
}

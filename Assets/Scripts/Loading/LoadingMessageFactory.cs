using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingMessageFactory : MonoBehaviour {

	public GameObject message;
	public float spawnTime = 5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("createLoadingMessage", spawnTime, spawnTime);
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
		Vector3 pos = new Vector3 (Random.Range (0f, Screen.width - 500), Random.Range (50f, Screen.height), 0);
		return pos;
	}

	Quaternion getRandomRotation() {
		Quaternion rot = Quaternion.Euler (0, 0, Random.Range (-60f, 0f));
		return rot;
	}
}

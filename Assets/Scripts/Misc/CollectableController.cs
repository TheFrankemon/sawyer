using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectableController : MonoBehaviour {

	public Text countText;
	public AudioClip audio;
	private int count;

	void Start () {
		count = 0;
		setCounter();
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.gameObject.CompareTag("Collectible")) {
			AudioSource.PlayClipAtPoint(audio,this.gameObject.transform.localPosition);
			col.transform.parent.gameObject.SetActive(false);
			count++;
			setCounter();
		}
	}

	void setCounter() {
		countText.text = "Partes: " + count.ToString() + "/11";
	}
}

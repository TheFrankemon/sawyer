using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

	public GameObject[] buttons;

	public void showCredits() {
		gameObject.SetActive (true);
		foreach (GameObject button in buttons) {
			button.SetActive(false);
		}
	}

	public void hideCredits() {
		gameObject.SetActive (false);
		foreach (GameObject button in buttons) {
			button.SetActive(true);
		}
	}
}

using UnityEngine;
using System.Collections;

public class TotemIconGoTo : MonoBehaviour {

	public MapCameraController mapController;
	public GameObject target;

	void OnMouseDown() {
		mapController.displayFullMap ();

	}
}

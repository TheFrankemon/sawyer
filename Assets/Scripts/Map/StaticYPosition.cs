using UnityEngine;
using System.Collections;

public class StaticYPosition : MonoBehaviour {

	public float yPosition;

	void Update () {
		transform.position = new Vector3 (transform.position.x, yPosition, transform.position.z);
	}
}

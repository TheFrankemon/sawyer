using UnityEngine;
using System.Collections;

public class FloatingController : MonoBehaviour {

	public bool useFloat;
	public float minHeight;
	public float maxHeight;
	public float velocity;
	public bool useRotate;
	public int rotateSpeed;

	private float currentHeight;
	
	void Start() {
		currentHeight = 0;	
	}
	
	void Update() {
		if (useFloat)
			floatAnimation ();
		if (useRotate)
			rotateAnimation ();
	}
	
	void floatAnimation() {
		if (currentHeight < minHeight || currentHeight > maxHeight) {
			velocity *= -1;
		}
		currentHeight += velocity;
		transform.position = new Vector3 (transform.position.x, transform.position.y + velocity, transform.position.z);
	}

	void rotateAnimation() {
		transform.Rotate (0,rotateSpeed,0);
	}
}

using UnityEngine;
using System.Collections;

public class PressMe : MonoBehaviour {

	public float maxRotation = 7f;
	public float timeToWait = 100;
	public int numberOfRepetitions = 5;
	public float speed = 10f;

	//float dRot = 1f;
	int repetitions = 0;
	float currentRotation = 0f;
	bool isWaiting = false;
	int timeWaited = 0;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("shake", repTime, repTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isWaiting) {
			if (repetitions <= numberOfRepetitions) {
				shake();
			} else {
				if (center()) {
					isWaiting = true;
				}
			}
		} else {
			timeWaited++;
			if (timeWaited >= timeToWait) {
				timeWaited = 0;
				isWaiting = false;
			}
		}
	}

	void shake() {
		/*currentRotation += dRot;
		transform.rotation = Quaternion.Euler(0, 0, currentRotation);

		if (Mathf.Abs(currentRotation) > maxRotation) {
			dRot *= -1;
			repetitions++;
		}*/
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, maxRotation), speed * Time.deltaTime);
		if (Mathf.Abs(maxRotation - transform.rotation.eulerAngles.z) < 2) {
			repetitions++;
			maxRotation = 360 - maxRotation;
		}
	}

	bool center() {
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), speed * Time.deltaTime);
		if (Mathf.Abs(0 - transform.rotation.eulerAngles.z) < 0.1) {
			repetitions = 0;
			currentRotation = 0f;
			transform.rotation = Quaternion.Euler(0, 0, currentRotation);
			return true;
		}
		return false;
	}
}

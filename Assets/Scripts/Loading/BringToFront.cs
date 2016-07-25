using UnityEngine;
using System.Collections;

public class BringToFront : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.SetAsLastSibling ();
	}
}

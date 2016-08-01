using UnityEngine;
using System.Collections;

public class MapIconController : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, player.eulerAngles.y, transform.eulerAngles.z);
	}
}

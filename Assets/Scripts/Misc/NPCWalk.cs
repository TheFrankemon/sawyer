using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCWalk : MonoBehaviour {

	enum NPCState {
		WALKING, WAITING, READY
	};

	public GameObject route;
	
	private Transform[] points;
	private int currentPoint;
	private NavMeshAgent nav;
	private NPCState state;
	private int waitingTime;
	private Animation anim;

	// Use this for initialization
	void Start () {
		points = route.GetComponentsInChildren<Transform> ();
		Debug.Log (points.Length);
		currentPoint = 0;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animation> ();
		state = NPCState.READY;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
			case NPCState.READY:
				nextPoint ();
				nav.SetDestination (points [currentPoint].position);
				state = NPCState.WALKING;
				//anim.enabled = true;
				anim.Play();
				break;
			case NPCState.WALKING:
				if (nav.remainingDistance == 0) {
					state = NPCState.WAITING;
					//anim.enabled = false;
					anim.Stop();
 					waitingTime = 0;
				}
				break;
			case NPCState.WAITING:
				if (waitingTime > 0) {
					state = NPCState.READY;
				}
				waitingTime++;
				break;
		}
	}

	void nextPoint() {
		currentPoint++;
		if (currentPoint >= points.Length) {
			currentPoint = 1;
		}
	}
}

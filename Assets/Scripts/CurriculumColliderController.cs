using UnityEngine;
using System.Collections;

public class CurriculumColliderController : MonoBehaviour {

	public string image;
	//public Texure image;

	private CurriculumMouseRotate curriculumController;

	// Use this for initialization
	void Start () {
		GameObject curr = GameObject.Find ("Curriculum");
		curriculumController = curr.GetComponent<CurriculumMouseRotate> ();
		Debug.Log (curr);
		Debug.Log (curriculumController);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			curriculumController.Show(image, null);
		}
	}
}

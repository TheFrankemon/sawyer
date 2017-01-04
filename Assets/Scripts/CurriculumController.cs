using UnityEngine;
using System.Collections;

public class CurriculumController : MonoBehaviour {

	public string image;

	private Texture2D imgFront;
	private Texture2D imgBack;
	private CurriculumMouseRotate curriculumRotateController;
	private DeskController deskController;
	private bool available = true;

	// Use this for initialization
	void Start () {
		imgFront = Resources.Load<Texture2D>("Image/Curriculum/" + image + "1");
		imgBack = Resources.Load<Texture2D>("Image/Curriculum/" + image + "2");
		MeshRenderer[] faces = GetComponentsInChildren<MeshRenderer>();
		faces [0].material.mainTexture = imgBack;
		faces [2].material.mainTexture = imgFront;

		curriculumRotateController = GameObject.Find ("Curriculum").GetComponent<CurriculumMouseRotate> ();
	}

	public void setDeskController(DeskController controller) {
		deskController = controller;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setAvailability(bool b) {
		available = b;
	}

	void OnMouseDown() {
		//deskController.enabled = false;
		if (available) {
			curriculumRotateController.Show (image, deskController);
		}
	}
}

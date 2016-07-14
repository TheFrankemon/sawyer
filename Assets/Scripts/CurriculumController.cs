using UnityEngine;
using System.Collections;

public class CurriculumController : MonoBehaviour {

	public string image;

	private Texture2D imgFront;
	private Texture2D imgBack;
	private CurriculumMouseRotate curriculumRotateController;
	private DeskController deskController;

	// Use this for initialization
	void Start () {
		imgFront = Resources.Load<Texture2D>("Image/Curriculum/" + image + " front");
		imgBack = Resources.Load<Texture2D>("Image/Curriculum/" + image + " back");
		//GetComponent<MeshRenderer> ().material.mainTexture = img;
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

	void OnMouseDown() {
		//deskController.enabled = false;
		curriculumRotateController.Show(image, deskController);
	}
}

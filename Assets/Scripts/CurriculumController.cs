using UnityEngine;
using System.Collections;

public class CurriculumController : MonoBehaviour {

	public string image;

	private Texture2D imgFront;
	private Texture2D imgBack;
	private CurriculumMouseRotate curriculumRotateController;

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
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown() {
		curriculumRotateController.Show(image);
	}
}

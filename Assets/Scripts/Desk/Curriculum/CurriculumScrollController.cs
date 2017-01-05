using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class CurriculumScrollController : MonoBehaviour {

	private CurriculumMouseRotate curriculum3d;
	private Image curriculumImage;
	
	void Start () {
		curriculum3d = GameObject.Find ("Curriculum").GetComponent<CurriculumMouseRotate> ();
		curriculumImage = GameObject.Find ("Curriculum Big").GetComponent<Image> ();
		Hide ();
	}
	
	void Update() {
		if (Input.GetKeyDown ("escape")) {
			Hide();
			Back();
		}
	}
	
	public void Show(Texture2D img) {
		enabled = true;
		GetComponent<Image>().enabled = true;
		curriculumImage.enabled = true;
		curriculumImage.sprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(.5f,.5f), 100);
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls(ControlsUIController.ControlsType.CURRICULUM_BIG);
	}
	
	public void Hide() {
		enabled = false;
		GetComponent<Image>().enabled = false;
		curriculumImage.enabled = false;
		GameObject.Find ("FPSController").GetComponent<ControlsUIController> ().changeControls (ControlsUIController.ControlsType.NORMAL);
	}

	public void Back() {
		curriculum3d.enabled = true;
		curriculum3d.Show (null, null);
	}
}

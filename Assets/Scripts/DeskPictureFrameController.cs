using UnityEngine;
using System.Collections;

public class DeskPictureFrameController : MonoBehaviour {

	public Texture portrait;

	private DeskController deskController;
	private DeskConversationController conversationController;

	// Use this for initialization
	void Start () {
		conversationController = GetComponentInParent<DeskConversationController> ();
		GetComponentInParent<MeshRenderer> ().materials [1].mainTexture = portrait;
		//setPortrait ();
	}

	void setPortrait() {
		//Material[] mats = GetComponentInParent<MeshRenderer> ().materials;
		//mats [1].SetTexture (portrait);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		conversationController.StartConversation (deskController);
	}

	public void setDeskController(DeskController controller) {
		deskController = controller;
	}
}

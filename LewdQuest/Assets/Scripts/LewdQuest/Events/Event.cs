using UnityEngine;
using System.Collections;

public abstract class Event : MonoBehaviour {


	public int 			id			{ get; set; }
	public GameObject 	buttonPanel	{ get; set; }
	public GameObject 	textPanel	{ get; set; }
	public GameObject 	mainImage	{ get; set; }
	public GameObject 	npcImage 	{ get; set; }
	public GameObject 	dialogPanel	{ get; set; }
	public ItemManager	itemManager { get; set; }
	public GameObject 	scriptFight	{ get; set; }
	//public string image { get; set; }


	void Start(){
		dialogPanel.SetActive (false);
	}

	public abstract void prepareScene();



	// Update is called once per frame
	void Update () {
	
	}
}

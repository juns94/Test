using UnityEngine;
using System.Collections;

public abstract class Event : MonoBehaviour {


	public int 			id			{ get; set; }
	public GameObject 	buttonPanel	{ get; set; }
	public GameObject 	textPanel	{ get; set; }
	public GameObject 	mainImage	{ get; set; }
	//public string image { get; set; }




	public abstract void prepareScene();



	// Update is called once per frame
	void Update () {
	
	}
}

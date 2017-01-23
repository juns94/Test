using UnityEngine;
using System.Collections;

public abstract class SceneHub  {

	public string[] names;
	public int count = 0;
	public string refuseText;
	public string acceptText;


	//public string refuseText;
	/*
	public string sceneText { get; set; }
	public string sceneName{ get; set; }
	public string image { get; set; }


	public SceneHub(string name, string text,string image){

		this.sceneName = name;
		this.sceneText = text;
		this.image = image;
	}
/*/

	public abstract Scene get (int pos);
}

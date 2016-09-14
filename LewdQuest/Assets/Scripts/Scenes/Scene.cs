using UnityEngine;
using System.Collections;

public class Scene  {

	public string sceneText { get; set; }
	public string sceneName{ get; set; }
	public string image { get; set; }


	public Scene(string name, string text,string image){

		this.sceneName = name;
		this.sceneText = text;
		this.image = image;
	}


}

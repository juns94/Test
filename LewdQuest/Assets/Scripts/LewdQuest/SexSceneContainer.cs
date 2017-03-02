using UnityEngine;
using System.Collections;

public class SexSceneContainer : MonoBehaviour {


	public static Scene sexScene{ get; set; }

	public Scene getScene(){
		return sexScene;
	}


	public void setScene(Scene scene){
		sexScene = scene;
	}

}

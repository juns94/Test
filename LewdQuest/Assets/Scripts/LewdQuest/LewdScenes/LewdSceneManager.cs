using UnityEngine;
using System.Collections;

public class LewdSceneManager{



	public static Scene getSceneFromId(int id , int horny){
		switch(id){
		case 1:
			if (horny > 70) {
				
				return new OrbScene ().orbFuck ();
			} else {
				return null;
			}
			
		case 0:	
			return new AureliaScene().titFuck();

		case 2:
			return new LidiaScene().buttFuck();


		}

		return null;

	}


	public static SceneHub getHubFromId(int id){

		switch(id){
		case 0:	
			return new AureliaScene ();
		case 1:
			return new OrbScene ();
		case 2:
			return new LidiaScene();

		case 10:
			return new WinterScene();
		}
		return null;

	}



		





}

using UnityEngine;
using System.Collections;

public class LewdSceneManager{




	public static Scene getSceneFromId(int id){
		switch(id){
		case 0:
			return null;//new LidiaScene().buttFuck();

		case 1:	
			return null;//new //AureliaScene().buttFuck();

		case 2:
			return new LidiaScene().buttFuck();


		}

		return null;

	}



		





}

using UnityEngine;
using System.Collections;

public class LewdSceneManager{



	public static Scene getSceneFromId(int id , int horny){
		switch(id){
		case 1:
			if (horny > 70)
				return new OrbScene ().orbFuck ();
			else
				return null;
		case 0:	
			return null;//new //AureliaScene().buttFuck();

		case 2:
			return new LidiaScene().buttFuck();


		}

		return null;

	}



		





}

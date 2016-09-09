using UnityEngine;
using System.Collections;

public class EnemyCreator {

	public static Character create(int id){
		switch (id) {

		case 0:
			return new Character(id,"Aurelia",20,1,5,10,"_aurelia",true);







		}



		return null;
	}
}

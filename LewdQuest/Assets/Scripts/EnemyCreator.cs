using UnityEngine;
using System.Collections;

public class EnemyCreator {

	public static Character create(int id){
		switch (id) {

		case 0:
			return new Character (id, "Aurelia", 20, 1, 5, 10, "_aurelia", true);
			break;
		case 1:
			return new Character (id, "Orb", 25, 1, 5, 10, "_orb", false);
			break;
		case 2:
			return new Character (id, "Lidia", 30, 1, 5, 10, "_lidia", true);
			break;







		}



		return null;
	}
}

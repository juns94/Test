using UnityEngine;
using System.Collections;

public class EnemyCreator {
	/// <summary>
	/// Create the specified character from a region and absoluteId.
	/// absoluteId MUST be -1 if the character is not hardcoded with an ID
	/// </summary>
	/// <param name="region">Region.</param>
	/// <param name="absoluteId">Absolute identifier.</param>
	public static Character create(int region, int absoluteId){

		int id;


		if (absoluteId == -1) {
			id = getIdByRegion (region);
			Debug.Log (" El id generado inicialmente es " + id + " y lo tenia: " + PlayerPrefs.GetInt (id + "", 0));
			if (PlayerPrefs.GetInt ("" + id, 0) > 0) {
				////// IF THE ENEMY HAS ALREADY BEEN RECRUITED!!!!
				id = reRollId (region, id);
			}

		} else {
			id = absoluteId;
		}
	//	Debug.Log (" El id final generado fue " + id);
		switch (id) {

		case 0:
			return new Character (id, "Aurelia", 25, 5, 9, "_aurelia", true,true , "0,1,2");
			break;
		case 1:
			return new Character (id, "Orb", 25, 5, 10, "_orb", true ,true, "0,1");
			break;
		case 2:
			return new Character (id, "Lidia", 30, 10, 5, "_lidia", true ,true, " 0,5");
			break;
		case 4:
			return new Character (id, "Twisted Tree", 30, 5, 10, "_twisted_tree", false,false , "6" , new int[2]{0,1});
			break;
		case 5:
			return new Character (id, "Thystle", 30,  5, 10, "_thystle", true,false , "0,1,2", new int[2]{0,1});
			break;
		case 6:
			return new Character (id, "Forest Wisp", 30, 5, 20, "_wisp", false,false, "1", new int[2]{0,1});
			break;
		case 10:
			return new Character (id, "Winter", 60,  15,  5, "_winter", true,true , "4", new int[1]{2});
			break;
		case 11:
			return new Character (id, "Lamia Hoplite", 70, 15,  5, "_hoplite", true,true , "7,1", new int[1]{2});
			break;
		case 12:
			return new Character (id, "Twisted Tree", 50, 15, 10, "_twisted_tree", false,false , "6" , new int[1]{0});
			break;
		case 13:
			return new Character (id, "Cyclops", 200, 20, 10, "_cyclops", false,false , "5" , new int[1]{100});
			break;
		case 14:
			return new Character (id, "Barbara", 100, 20, 10, "_oni", true,true, "5,8" , new int[1]{100});
			break;





			///////////////////////////// PLAINS ////////////////////////////////
			/// 
			/// 
			/// 
			case 21:
			return new Character (id, "Goblin", 80, 20, 10, "_goblin", false ,false, "5" , new int[1]{100});
			break;

			////////////////////////////// SPECIAL ENEMIES!!!! ////////////////////////////////
			/// 
			/// 
			/// 
			/// 
			///
		case 200:
			return new Character (id, "Church Assassin", 150, 20, 20, "_nun", true,true, "5,4" , new int[1]{101});
			break;

		default : return create(region,-1);
			
		
		}


		return null;
	}


		

	public static int getIdByRegion(int region){

		switch (region) {
		case 0:
			return 0;
		case 1:// FOREST
			return (Random.Range(1,10));

		case 2:// MOUNTAIN
			return (Random.Range(10,21));
			break;
		case 3: // PLAINS
			return (Random.Range(21,31));
			break;
		}
		return 0;
	}



	public static int reRollId(int region, int id){
		
		while (isAlreadyOwned(id)) {
			//Debug.Log ("Loop");
			id = getIdByRegion(region);// (Random.Range (0, 4));
		}

		return id;
			
	}


	public static bool isAlreadyOwned(int id){
		if (PlayerPrefs.GetInt ("" + id) > 0) {
			return true;
		} else {
			return false;
		}
	}




}

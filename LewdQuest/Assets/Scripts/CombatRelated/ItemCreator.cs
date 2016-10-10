using UnityEngine;
using System.Collections;

public class ItemCreator {

	public static Item createItem(int id , int amount){



		switch (id) {

		case 0:
			return new Item (id, "Hp Potion" 		,amount);
			break;
		case 1:
			return new Item (id, "Vial of Health"	,amount);
			break;
		case 2:
			return new Item (id, "Life Elixir"		,amount);
			break;
		case 3:
			return new Item (id, "Horny Potion"		,amount);
			break;
		case 4:
			return new Item (id, "Strength Potion"	,amount);
			break;
		case 5:
			return new Item (id, "Mage Potion"	,amount);
			break;

		}
		return null;
	}

	public static void useItem(Character character , int itemId){

		switch (itemId) {

		case 0:
			character.heal (30);
			break;
		case 1:
			character.heal (100);
			break;
		case 2:
			character.heal (500);
			break;
		case 3:
			character.makeHorny(30);
			break;
		case 4:
			character.attack 		+= 10;
			break;
		case 5:
			character.magicPower 	+= 10;
			break;
		
		}

	}

}

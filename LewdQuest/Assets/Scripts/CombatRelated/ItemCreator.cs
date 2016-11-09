using UnityEngine;
using System.Collections;

public class ItemCreator {

	public static Item createItem(int id , int amount){



		switch (id) {

		case 0:
			return new Item (id, "Hp Potion" 		,amount , " Heals a character for 30 hitpoints.");
			break;
		case 1:
			return new Item (id, "Vial of Health"	,amount , " Heals a character for 100 hitpoints.");
			break;
		case 2:
			return new Item (id, "Life Elixir"		,amount, " Heals a character for 500 hitpoints.");
			break;
		case 3:
			return new Item (id, "Horny Potion"		,amount , " Makes a character horny by 30 points.");
			break;
		case 4:
			return new Item (id, "Strength Potion"	,amount , " Gives a temporary attack boost for the rest of the current fight.");
			break;
		case 5:
			return new Item (id, "Mage Potion"	,amount , " Gives a temporary magic power boost for the rest of the current fight.");
			break;

			///////***********************************************************************************************************************///////////////////
			/// 
			/// 

		case 100:
			return new Item (id, "Vitality powder"	,amount , " Permanently raises your HP by 10 hitpoints. Does not contain Giant marrow or artificial additives.", Item.TYPE.PERMANENT);
			break;
		case 101:
			return new Item (id, "Barbarian extract"	,amount , " Permanently raises your Attack by 1 point. Grinded barbarian bones and unknown substances into a vial of pure power.", Item.TYPE.PERMANENT);
			break;
		case 102:
			return new Item (id, "Mage Audiobook"	,amount , " Permanently raises your Magical Power by 1 point. Enchanted book of arcane knowledge that reads itself and then dissappears.", Item.TYPE.PERMANENT);
			break;

			///////***********************************************************************************************************************///////////////////
			/// 
			/// 

		case 200:
			return new Item (id, "Rose"	,amount , " Carefully handpicked by whisps on the Evergreen garden of luxury. Be careful about the thorns.", Item.TYPE.GIFT);
			break;
		case 201:
			return new Item (id, "Chocolates"	,amount , " Delicious candy for the most refined tastes. ( Does not contain trans fat or gluten ).", Item.TYPE.GIFT);
			break;
		case 202:
			return new Item (id, "Skull"	,amount , "Gruesome and slightly out of fashion, yet will be acclaimed by a select few.", Item.TYPE.GIFT);
			break;
		case 203:
			return new Item (id, "Bouquet of Flowers"	,amount , "A bunch of wildflowers and delicate ornaments.", Item.TYPE.GIFT);
			break;
		case 204:
			return new Item (id, "Candy bag"	,amount , "A lot of assorted sweets for the sugar friendly.", Item.TYPE.GIFT);
			break;
		case 205:
			return new Item (id, "Obedience candy"	,amount , "Ingesting it will make people slightly more obedient to you.", Item.TYPE.GIFT);
			break;
		case 206:
			return new Item (id, "Dragon Dildo"	,amount , "Someone is going to end with a stretched, loose butt with this.", Item.TYPE.GIFT);
			break;
		case 207:
			return new Item (id, "Wine"	,amount , "Made with fine berries and the tears of working gnomes ( They cry tears of joy, don't worry. ).", Item.TYPE.GIFT);
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



		case 100:
			character.totalHP 	+= 10;
			break;
		case 101:
			character.attack 	+= 1;
			break;
		case 102:
			character.magicPower 	+= 1;
			break;
		}

	}

}

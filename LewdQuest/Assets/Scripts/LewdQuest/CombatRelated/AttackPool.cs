using UnityEngine;
using System.Collections;

public class AttackPool {


	public static Attack createAttack(int id){


		switch (id) {

		case 0:
			return new Attack ("Punch" , 1 , " hits you with their fists." , Attack.TYPE.DAMAGE , Attack.SUBTYPE.NORMAL);

		case 1:
			return new Attack ("Magic Blast" , 1 , " unleashes a bolt of magic towards you." , Attack.TYPE.MAGIC , Attack.SUBTYPE.PAPER);
		
		case 2:
			return new Attack ("Low Heal" , 1 , " <b>heals</b> their party with arcane magic." , Attack.TYPE.HEAL , Attack.SUBTYPE.NORMAL);
		
		case 3:
			return new Attack ("Masturbate" , 1 , " cannot contain themselves and masturbates furiously." , Attack.TYPE.LEWD , Attack.SUBTYPE.NORMAL);

		case 4:
			return new Attack ("Sword Lunge" , 1.3f , " lunges their sword furiously towards you." , Attack.TYPE.DAMAGE,  Attack.SUBTYPE.SCISSOR);
		
		case 5:
			return new Attack ("Kick" , 1 , " gives you a vicious kick." , Attack.TYPE.DAMAGE  , Attack.SUBTYPE.ROCK);

		case 6:
			return new Attack ("Vine Attack" , 1 , " slashes you with their eerie tentacles." , Attack.TYPE.DAMAGE , Attack.SUBTYPE.PAPER);
		
		case 7:
			return new Attack ("Spear" , 1 , " pierces through you with their weapon." , Attack.TYPE.DAMAGE, Attack.SUBTYPE.SCISSOR);

		case 8:
			return new Attack ("Club Smash" , 2 , " smashes you with their huge club." , Attack.TYPE.DAMAGE , Attack.SUBTYPE.NORMAL);

		case 9:
			return new Attack ("Blob Heal" , 2 , " <b> heals </b> itself in a gruesome display." , Attack.TYPE.HEAL , Attack.SUBTYPE.NORMAL);
		
		case 10:
			return new Attack ("Toughen up" , 2 , " Prepares for enemy attacks and boosts up their defence." , Attack.TYPE.BUFF, Attack.SUBTYPE.NORMAL);
		



		}
		return null;
	}


}

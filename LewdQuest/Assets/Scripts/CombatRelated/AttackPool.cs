using UnityEngine;
using System.Collections;

public class AttackPool {


	public static Attack createAttack(int id){



		switch (id) {

		case 0:
			return new Attack ("Punch" , 1 , " hits you with their fists." , Attack.TYPE.DAMAGE);
			break;
		case 1:
			return new Attack ("Magic Blast" , 1 , " unleashes a bolt of magic towards you." , Attack.TYPE.DAMAGE);
			break;
		case 2:
			return new Attack ("Low Heal" , 1 , " <b>heals</b> their party with arcane magic." , Attack.TYPE.HEAL);
			break;
		case 3:
			return new Attack ("Masturbate" , 1 , " cannot contain themselves and masturbates furiously" , Attack.TYPE.LEWD);
			break;
		case 4:
			return new Attack ("Sword Lunge" , 1.3f , " lunges their sword furiously towards you" , Attack.TYPE.DAMAGE);
			break;
		case 5:
			return new Attack ("Kick" , 1 , " gives you a vicous kick." , Attack.TYPE.DAMAGE);
			break;
		case 6:
			return new Attack ("Vine Attack" , 1 , " slashes you with their eerie tentacles." , Attack.TYPE.DAMAGE);
			break;
		case 7:
			return new Attack ("Spear" , 1 , " pierces through you with their weapon." , Attack.TYPE.DAMAGE);
			break;


		}
		return null;
	}


}

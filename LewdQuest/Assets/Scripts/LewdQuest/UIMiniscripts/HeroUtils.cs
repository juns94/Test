using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public static class HeroUtils
{

	static int BASEHP 		= 200;
	static int BASEATTACK	= 5;


	// KNOWN ATRIBUTES:
	/*
	 * 
	 * - hpTotal
	 * - hp
	 * - attack
	 * - energy
	 * - gold
	 * - items
	 * - x ( party member id
	 * 
	 * 
	 * 
	 * 
	 **/


	public static void setHero(){
		saveHero (getHero ());
		PlayerPrefs.SetInt("hp",getTotalHP());
	}

	public static Character getHero(){

		return new Character (-1, "Yourself", getTotalHP(),getAttack(),getMagic(),"",false);

	}

	public static void saveHero( Character character ){
		PlayerPrefs.SetInt("hp" 	 , character.hp			);
		PlayerPrefs.SetInt("hpTotal" , character.totalHP	);
		PlayerPrefs.SetInt("attack"  , character.attack		);
		PlayerPrefs.SetInt("magic"   , character.magicPower );
		PlayerPrefs.SetInt("hpTotal" , character.getTotalHp());
		PlayerPrefs.Save ();
	}

	public static void clearEVERYTHING(){
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save();
	}

	public static int getTotalHP(){
		return PlayerPrefs.GetInt ("hpTotal", BASEHP);
	}


	public static int getAttack(){
		return PlayerPrefs.GetInt ("attack", BASEATTACK);
	}



	public static void AddAttack( int amount){
		PlayerPrefs.SetInt ("attack", PlayerPrefs.GetInt ("attack") + amount);
		PlayerPrefs.Save ();
	}

	public static void AddHp( int amount){
		PlayerPrefs.SetInt ("hpTotal", PlayerPrefs.GetInt ("hpTotal") + amount);
		PlayerPrefs.Save ();
	}



	public static int getMagic(){
		return PlayerPrefs.GetInt ("magic", BASEATTACK);
	}



	public static void AddMagic( int amount){
		PlayerPrefs.SetInt ("magic", PlayerPrefs.GetInt ("magic") + amount);
		PlayerPrefs.Save ();
	}
}


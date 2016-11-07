using UnityEngine;
using System.Collections;


[System.Serializable]
public class Attack  {

	string name;
	float damage;
	string flavorText { set; get; }
	public TYPE type { get; set; }


	public enum TYPE{
		DAMAGE,
		MAGIC,
		HEAL,
		BUFF,
		LEWD
	}


	public Attack(string name, float damage , string flavorText , TYPE type){

		this.name = name;
		this.damage = damage;
		this.flavorText = flavorText;
		this.type = type;
	}


	public string getName(){
		return name;
	}

	public float getDamage(){
		return damage;
	}

	public string getFlavorText() { return this.flavorText;}




}

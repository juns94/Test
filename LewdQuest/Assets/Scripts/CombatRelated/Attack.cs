using UnityEngine;
using System.Collections;

public class Attack  {

	string name;
	int damage;
	string flavorText;


	public Attack(string name, int damage , string flavorText){

		this.name = name;
		this.damage = damage;
		this.flavorText = flavorText;
	}


	public string getName(){
		return name;
	}

	public int getDamage(){
		return damage;
	}

	public string getFlavorText() { return this.flavorText;}


	public bool gone { get; set; }



}

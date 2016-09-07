using UnityEngine;
using System.Collections;

public class Character  {



	public int id;
	public string name;
	public int level;
	public int attack;
	public int magicPower;
	public int hp;
	public bool alive = true;
	public int totalHP;
	public bool gone;
	public bool alreadyAttacked { get; set; }


	public Character(int id, string name,int hp , int level, int attack , int magicPower){
		this.id = id;
		this.hp = hp;
		this.totalHP = hp;
		this.name = name;
		this.level = level;
		this.attack = attack;
		this.magicPower = magicPower;

	}

	public int getTotalHp(){

		return totalHP;
	}


	public string getName(){
		return name;
	}

	public int getHp(){
		return hp; }

	public void receiveDamage(int damage){
		hp -= damage;


			if (hp <= 0) {
				alive = false;
				hp = 0;
			}
		}

	public int getAttack(){
		return attack;
	}

	public int getMagicPower(){
		return magicPower;
	}


	public bool getAlive(){
		return alive;
	}




	
}

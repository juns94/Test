using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public string image { get; set; }
	public bool alreadyAttacked { get; set; }
	public bool female { get; set; }
	public int[] itemPool;

	public List<Attack> attackList { get; set; }




	/***
	 * SEX Traits.
	 **/
	public int obedience{ get; set; }
	public int horny{ get; set; }





	public Character(int id, string name,int hp , int level, int attack , int magicPower, string image , bool female){
		this.id = id;
		this.hp = hp;
		this.totalHP = hp;
		this.name = name;
		this.level = level;
		this.attack = attack;
		this.magicPower = magicPower;
		this.image = image;
		this.female = female;
	}


	public Character(int id, string name,int hp , int level, int attack , int magicPower, string image , bool female , string attacks){
		this.id = id;
		this.hp = hp;
		this.totalHP = hp;
		this.name = name;
		this.level = level;
		this.attack = attack;
		this.magicPower = magicPower;
		this.image = image;
		this.female = female;
		attackList = new List<Attack> ();

		string[] temp = attacks.Split(',');
		for (int x = 0; x < temp.Length; x++) {
			attackList.Add (AttackPool.createAttack(int.Parse(temp[x])));
		}

	}


	public Character(int id, string name,int hp , int level, int attack , int magicPower, string image , bool female , string attacks, int[] pool){
		this.id = id;
		this.hp = hp;
		this.totalHP = hp;
		this.name = name;
		this.level = level;
		this.attack = attack;
		this.magicPower = magicPower;
		this.image = image;
		this.female = female;
		attackList = new List<Attack> ();
		this.itemPool = pool;
		string[] temp = attacks.Split(',');
		for (int x = 0; x < temp.Length; x++) {
			attackList.Add (AttackPool.createAttack(int.Parse(temp[x])));
		}


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


	public void makeHorny( int amount){
		if (horny < 100) { 
			horny += amount;
		}
	}

	public void heal( int amount){
		if (hp < totalHP) { 
			hp += amount;

			if (hp > totalHP)
				hp = totalHP;
		}
	}


	public Attack getRandomAttack(){
		return attackList[Random.Range(0, attackList.Count)];
	}
}

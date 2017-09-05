using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character  {


//	public DialogHub dialogHub { get; set; }
	public int id;
	public string name;
	public int attack;
	public int magicPower;
	public int hp;
	public bool alive = true;
	public int totalHP;
	public bool gone;
	public string image { get; set; }
	public bool alreadyAttacked { get; set; }
	public bool female { get; set; }
	public bool recruitable;
	public int[] itemPool;
	public int armor;
	public Attack.SUBTYPE type{ get; set; }



	public List<Attack> attackList { get; set; }




	/***
	 * SEX Traits.
	 **/
	public int obedience{ get; set; }
	public int horny{ get; set; }
	public int love{ get; set; }

	public enum SIZE
	{
		SMALL,
		BIG
	}

	public SIZE size;




	public Character(int id, string name,int hp , int attack , int magicPower, string image , bool female){
		this.id = id;
		this.hp = hp;
		this.totalHP = hp;
		this.name = name;
		this.attack = attack;
		this.magicPower = magicPower;
		this.image = image;
		this.female = female;
		this.type = type;
		this.type = Attack.SUBTYPE.NORMAL;
	}


	public Character(int id, string name,int hp , int attack , int magicPower, string image , bool female, bool recruit , string attacks, Attack.SUBTYPE type){
		this.id = id;
		this.hp = hp;
		this.totalHP = hp;
		this.name = name;
		this.attack = attack;
		this.magicPower = magicPower;
		this.image = image;
		this.female = female;
		attackList = new List<Attack> ();
		this.type = type;

		string[] temp = attacks.Split(',');
		for (int x = 0; x < temp.Length; x++) {
			attackList.Add (AttackPool.createAttack(int.Parse(temp[x])));
		}

	}


	public Character(int id, string name,int hp , int attack , int magicPower, string image , bool female, bool recruit, string attacks, int[] pool, Attack.SUBTYPE type) {
		this.id = id;
		this.hp = hp;
		this.totalHP = hp;
		this.name = name;
		this.attack = attack;
		this.magicPower = magicPower;
		this.image = image;
		this.female = female;
		this.recruitable = recruit;
		attackList = new List<Attack> ();
		this.itemPool = pool;
		string[] temp = attacks.Split(',');
		for (int x = 0; x < temp.Length; x++) {
			attackList.Add (AttackPool.createAttack(int.Parse(temp[x])));
		}
		this.type = type;


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
		damage -= armor;
		if (damage > 0) {
			hp -= damage;

			if (horny >= 50)
				horny -= 10;


			if (hp <= 0) {
				alive = false;
				hp = 0;
			}
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

		alive = true;
	}

	public void addLove(int amount){
		love += amount;
	}
	public void addObd(int amount){
		obedience += amount;
	}



	public Attack getRandomAttack(){
		return attackList[Random.Range(0, attackList.Count)];
	}

	public DialogHub getDialogHub(){
		//if (dialogHub == null) {
		//	dialogHub =  DialogPool.get(id);
	//	}
		return DialogPool.get(id);
	}
}

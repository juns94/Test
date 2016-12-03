using UnityEngine;
using System.Collections;

public class Item {


	public enum TYPE{
		CONSUMABLE,
		GIFT,
		ARMOR,
		PERMANENT,
		QUEST
	}



	public int id;
	public string image { get; set; }
	public string name { get; set; }
	public string description { get; set; }
	public int amount { get; set; }
	public GameObject uiRef { get; set; }
	public TYPE type{ get; set; }


	public Item(int id, string name, int amount , string description){
		this.id = id;
		this.name = name;
		this.amount = amount;
		this.description = description;
		this.type = TYPE.CONSUMABLE;
	}

	public Item(int id, string name, int amount , string description, TYPE type){
		this.id = id;
		this.name = name;
		this.amount = amount;
		this.description = description;
		this.type = type;
	}

	public Item(int id, string name ,int amount){
		this.id = id;
		this.name = name;
		this.amount = amount;
		this.description = "";
		this.type = TYPE.CONSUMABLE;
	}




}

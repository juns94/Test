using UnityEngine;
using System.Collections;

public class Item {

	public int id;
	public string image { get; set; }
	public string name { get; set; }
	public int amount { get; set; }
	public GameObject uiRef { get; set; }

	public Item(int id, string name, string image ,int amount){
		this.id = id;
		this.name = name;
		this.image = image;
	
	}

	public Item(int id, string name , int amount){
		this.id = id;
		this.name = name;
		this.amount = amount;
	}



}

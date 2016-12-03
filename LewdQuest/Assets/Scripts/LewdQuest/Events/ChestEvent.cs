using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChestEvent : Event {


	GameObject buttonNo;
	GameObject buttonYes;
	GameObject buttonLidia;

	public override void prepareScene(){



			mainImage.GetComponent<Image>().sprite = Resources.Load <Sprite> ("chest");  
		string flavor = "  You encounter an old, forgotten, wooden chest troughout your trip on the mountain. You realize that the chest could be opened if you carefully picked the lock. What do you do? ";
		textPanel.GetComponentInChildren<Text>().text = flavor;

		buttonYes = instanceButton ("Prefabs/AttackBtn" , buttonPanel.transform);
		buttonYes.GetComponentInChildren<Button>().onClick.AddListener (() => {
			makeExit();
			pickTheLock();

		});
		buttonYes.GetComponentInChildren<Text> ().text = " Pick the Lock";


		buttonNo  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		buttonNo.GetComponentInChildren<Button>().onClick.AddListener (() => {
			SceneManager.LoadScene ("MapScene");
			Destroy(GameObject.Find("MapManager"));
		});
		buttonNo.GetComponentInChildren<Text> ().text = " Leave ";




		if (PlayerPrefs.GetInt ("2") > 0) {
			buttonNo  = instanceButton ("Prefabs/GreenBtn", buttonPanel.transform);
			buttonNo.GetComponentInChildren<Button>().onClick.AddListener (() => {
				useLidia();
			});
			buttonNo.GetComponentInChildren<Text> ().text = "Use Lidia";
		}




	}
		

	void pickTheLock(){


		makeExit();
		string reward = getRandomReward ();
		textPanel.GetComponentInChildren<Text> ().text = "You decide to try to open the chest yourself.";
		textPanel.GetComponentInChildren<Text> ().text += reward;
		PlayerPrefs.Save();
	}


	void useLidia(){


		makeExit();
		string reward = getRandomGoodReward ();
		textPanel.GetComponentInChildren<Text> ().text = " You use the help of your trusty thief and make her pick the lock for you. She simply smiles and unlocks the loot hidden behind the seal.";
		textPanel.GetComponentInChildren<Text> ().text += reward;
		PlayerPrefs.Save();
	}
	
	// Update is called once per frame
	void Update () {

	
	}


	public string getRandomReward(){


		switch(Random.Range(0,3)){


		case 0:{
				PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + 100);
				return "\n Inside the chest you find 100 gold pieces. You add them into your pouch happily";
				break;
			}
		case 1:{
				//TODO HACER QUE LE DE LO QUE DICE AHI
				//	PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + 100);
				itemManager.AddItemToInventory(4,1);
				return "\n Inside the chest you find a potion of strength. You carefully put it in your inventory.";
				break;
			}



		case 2:{
				int newHp = (PlayerPrefs.GetInt ("hp", 0) /2);
				if(newHp <= 0) newHp = 1;
					PlayerPrefs.SetInt ("hp", newHp);

				return "\n The chest was <b>rigged!</b> You trigger the trap mechanism and a toxic gas emanates from the edges of the wood. You suffer <b>HEAVY</b> damage. ( You lose half of your HP )";
					break;
				
			}

		}
		return "ERROR.IM SORRY";

	}

	public string getRandomGoodReward(){


		switch(Random.Range(0,3)){


		case 0:{
				PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + 200);
				return "\n Inside the chest you find 200 gold pieces. You add them into your pouch happily";
				break;
			}
		case 1:{
				itemManager.AddItemToInventory(4,1);
				return "\n Inside the chest you find a potion of strength. You carefully put it in your inventory.";
				break;
			}



		case 2:{
				//PlayerPrefs.SetInt ("hp", newHp);
				itemManager.AddItemToInventory(0,1);
				return "\n The chest was <b>rigged!</b> , but Lidia was cunning enough to see through this and she disabled the trap before anything could happen. She even discovers hidden loot " +
					"on a secret spot. The item being protected was nothing but an HP Potion.";
				break;

			}

		}
		return "ERROR.IM SORRY";

	}



	public void makeExit(){

		Debug.Log (" Entro");
		for (int x = 0; x < buttonPanel.transform.childCount; x++) {
			Destroy (buttonPanel.transform.GetChild (x).gameObject);

		}

		GameObject exitButton = null;
		/*
		exitButton = instanceButton ("Prefabs/RedButton", buttonPanel.transform);
		exitButton.GetComponentInChildren<Text> ().text = " Exit ";
		exitButton.GetComponent<Button>().onClick.AddListener (() => {
			SceneManager.LoadScene ("MapScene");
			Destroy(GameObject.Find("MapManager"));

		});
		exitButton.transform.localScale = Vector3.one;*/


		exitButton  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		exitButton.GetComponentInChildren<Button>().onClick.AddListener (() => {
			SceneManager.LoadScene ("MapScene");
			Destroy(GameObject.Find("MapManager"));
		});
		exitButton.GetComponentInChildren<Text> ().text = " Exit ";

	}

	public GameObject instanceButton( string path, Transform parent){
		GameObject container = null;
		container = Instantiate( (GameObject) Resources.Load(path));
		container.transform.SetParent(parent);
		container.transform.localScale = Vector3.one;
		container.transform.localPosition= Vector3.one;
		return container;
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class RunningEvent: Event {


	GameObject buttonNo;
	GameObject buttonYes;
	GameObject buttonLidia;

	GameData gameData;
	Character slut;
	public override void prepareScene(){


		mainImage.GetComponent<Image>().sprite = Resources.Load <Sprite> ("_wip");  	
		ArrayList temp = LewdUtilities.getPartyBitches ();
		try{
		gameData = LewdUtilities.getGameData (GameObject.Find("GameData"));
		if(temp.Count > 0)
		slut = gameData.getCharacterById (((int)temp[0]));
		}catch	(Exception e ){
			
		}
		string flavor;
		if (slut == null) {
			flavor = "You've ran for quite a bit during your trip through the plains. Your body becomes stronger and you've gained +5 hitpoints!";
		} else {
			flavor = "You've ran for quite a bit during your trip through the plains. You and " + slut.name + " have become stronger, gaining both +5 hitpoints!";
		}
		textPanel.GetComponentInChildren<Text>().text = flavor;
		buttonNo  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		buttonNo.GetComponentInChildren<Button>().onClick.AddListener (() => {

			if(slut != null)slut.totalHP += 5;
			DataAccess.Save(gameData);
			HeroUtils.AddHp(5);
			SceneManager.LoadScene ("MapScene");
			Destroy(GameObject.Find("MapManager"));
		});
		buttonNo.GetComponentInChildren<Text> ().text = " Leave ";

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

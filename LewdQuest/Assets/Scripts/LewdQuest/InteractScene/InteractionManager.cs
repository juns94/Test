using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour {

	public GameObject[] panels;
	public ScenePickerManager scenePickerManager;
	public InteractionContainer container;
	public GameObject 			detailPanel;
	public Text name;


	public Character currentCharacter;
	public GameData gameData;
	void Start () {
		gameData = DataAccess.Load ();
		currentCharacter = gameData.getCharacterById (container.getId());
		scenePickerManager.setup ();
	}

	void Update () {
		setDetails (currentCharacter);
	}

	public void setDetails(Character character){
		Text[] details;
		details = detailPanel.GetComponentsInChildren<Text> ();
		name.text = character.name;
		details [0].text = ""+character.obedience + "%";
		details [1].text = ""+character.horny + "%";
		details [2].text = ""+character.love + "%";
		details [3].text = ""+character.hp + "/" +character.totalHP;
		details [4].text = ""+character.attack;
		details [5].text = ""+character.magicPower;
	}

	public void hidePanel(int position){
		foreach (GameObject item in panels) {
			item.SetActive (false);
		}
		panels [position].SetActive (true);
	}



	public void goToParty(){
		DataAccess.Save (gameData);
		SceneManager.LoadScene ("PartyScene");
	}
}

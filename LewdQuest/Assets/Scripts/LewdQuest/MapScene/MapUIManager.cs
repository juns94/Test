using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MapUIManager : MonoBehaviour {



	int totalHP;
	int currentHP;
	int currentEnergy;
	int totalEnergy;

	public GameObject hpGUI;
	public GameObject energyGUI;
	public GameObject itemUI;
	public GameObject goldUI;
	public GameObject exploreGUI;
	public GameObject restGUI;
	public GameObject overMapButtons;
	public GameObject MapImage;



	GameObject itemPanel;
	void Start () {

		totalHP 	= PlayerPrefs.GetInt ("hpTotal", 230);
		currentHP 	= PlayerPrefs.GetInt ("hp", 230);

		totalEnergy = 100;

		currentEnergy 	= PlayerPrefs.GetInt ("energy", 100);
		//itemPanel = itemUI.transform.GetChild (1).gameObject;




		Button[] buttons = overMapButtons.GetComponentsInChildren<Button> ();
		foreach (Button button in buttons) {
			if (button.name.Contains ("!")) {
				Debug.Log (button.gameObject.name);
				if (PlayerPrefs.GetInt (button.gameObject.name, 0) == 1) {
					button.gameObject.SetActive (true);
				
				} else {
					button.gameObject.SetActive (false);
				}
			}

		}


	}
	
	// Update is called once per frame
	void Update () {
	


		hpGUI.GetComponentInChildren<HpDecreaseSlow> ().setDamage ((float)PlayerPrefs.GetInt ("hp", 0), totalHP);
		hpGUI.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt ("hp", 0) + "/" + totalHP;


		energyGUI.GetComponentInChildren<HpDecreaseSlow> ().setDamage ((float)PlayerPrefs.GetInt ("energy", 100), totalEnergy);
		energyGUI.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt ("energy", 100) + "/" + totalEnergy;


		goldUI.GetComponentsInChildren<Text> () [0].text = PlayerPrefs.GetInt ("gold", 0) + "";

	}


	public void showCity(){
		
	}




	public void setExploreButtonState(bool state){
		exploreGUI.SetActive (state);
	}
	public void setRestButtonState(bool state){
		restGUI.SetActive (state);
	}


	/*
	public void showItemPanel(){

		itemPanel.GetComponent<Animator>().Play ("itemPanelSlide");

	}
	public void hideItemPanel(){
	//	partyButtons.SetActive (true);
		itemPanel.GetComponent<Animator>().Play ("itemPanelSlideOut");
	}*/
}

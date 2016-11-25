using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StatManager : MonoBehaviour {


	public Text hpText,attackText,magicText;


	void Start(){
	}
	
	// Update is called once per frame
	void Update () {
		//

	//	Character character = getCharacter();
		hpText.text = "HP : " + PlayerPrefs.GetInt ("hpTotal", 0);
		attackText.text = "Attack : " + PlayerPrefs.GetInt ("attack", 0);
		magicText.text = "Magic : " + PlayerPrefs.GetInt ("magic", 0);

	
	}

	public void goToMap(){

		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("MapScene");

	}
}

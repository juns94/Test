using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AssassinEvent : Event {


	GameObject buttonNo;
	GameObject buttonYes;
	GameObject buttonLidia;

	public override void prepareScene(){


		mainImage.GetComponent<Image>().sprite = Resources.Load <Sprite> ("_wip");  

		string flavor = " You've been ambushed by a church assassin!!!";

		textPanel.GetComponentInChildren<Text>().text = flavor;

		makeExit();

		/*

		if (PlayerPrefs.GetInt ("2") > 0) {
			buttonNo  = instanceButton ("Prefabs/GreenBtn", buttonPanel.transform);
			buttonNo.GetComponentInChildren<Button>().onClick.AddListener (() => {
				//useLidia();
			});
			buttonNo.GetComponentInChildren<Text> ().text = "Use Lidia";
		}
/*/



	}


	void pickTheLock(){


		makeExit();
		textPanel.GetComponentInChildren<Text> ().text = "As you approach the light, a sudden eerie feeling crosses up your spine. You notice something move fast on the corner of your eye... A dark skinned elf appears in front of you along with a couple of animated tree stumps.";
		Invoke("taunt", 0.5f);
	}


	public void taunt(){
		dialogPanel.SetActive (true);
		dialogPanel.GetComponent<Animator> ().Play ("dialogPanelAnimation");
		dialogPanel.GetComponentInChildren<Text>().text = " Your adventure ends here, 'hero'. ";
		dialogPanel.GetComponentsInChildren<Image>()[1	].sprite = Resources.Load <Sprite> ("_aurelia_face");  

	}

	// Update is called once per frame
	void Update () {


	}







	public void makeExit(){

		for (int x = 0; x < buttonPanel.transform.childCount; x++) {
			Destroy (buttonPanel.transform.GetChild (x).gameObject);

		}

		GameObject exitButton = null;

		exitButton  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		exitButton.GetComponentInChildren<Button>().onClick.AddListener (() => {
			RiggedFight script = scriptFight.AddComponent<RiggedFight>();
			script.enemyIdArray = new int[]{200};
			DontDestroyOnLoad(scriptFight);
			SceneManager.LoadScene ("BattleSceneTest");
			Destroy(GameObject.Find("MapManager"));
		});
		exitButton.GetComponentInChildren<Text> ().text = " Fight! ";

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

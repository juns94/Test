using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AugustusEvent : Event {


	GameObject buttonNo;
	GameObject buttonYes;
	GameObject buttonLidia;

	public override void prepareScene(){
		npcImage.SetActive (true);
		npcImage.GetComponent<Image>().sprite  = Resources.Load <Sprite> ("Portraits/_augustus");  
		mainImage.GetComponent<Image>().sprite = Resources.Load <Sprite> ("_wip");  
		string flavor = "You meet an obese, shady knight at the entrance of the citiy.";
		textPanel.GetComponentInChildren<Text>().text = flavor;
		first();


	}


	void pickTheLock(){


		makeExit();
		textPanel.GetComponentInChildren<Text> ().text = "As you approach the light, a sudden eerie feeling crosses up your spine. You notice something move fast on the corner of your eye... A dark skinned elf appears in front of you along with a couple of animated tree stumps.";
	
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


	public void first(){

		for (int x = 0; x < buttonPanel.transform.childCount; x++) {
			Destroy (buttonPanel.transform.GetChild (x).gameObject);

		}

		GameObject exitButton = null;

		exitButton  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		exitButton.GetComponentInChildren<Button>().onClick.AddListener (() => {
			textPanel.GetComponentInChildren<Text> ().text = "Augustus:  You must be the exiled pervert everyone talks about!... Nyeheh.";
			second();
		});
		exitButton.GetComponentInChildren<Text> ().text = " Next ";

	}

	public void second(){

		for (int x = 0; x < buttonPanel.transform.childCount; x++) {
			Destroy (buttonPanel.transform.GetChild (x).gameObject);

		}

		GameObject exitButton = null;

		exitButton  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		exitButton.GetComponentInChildren<Button>().onClick.AddListener (() => {
			textPanel.GetComponentInChildren<Text> ().text = "Augustus: If I capture you right now, I'm sure they will give me something back in the church!";
			makeExit();
		});
		exitButton.GetComponentInChildren<Text> ().text = " Next ";

	}









	public void makeExit(){

		for (int x = 0; x < buttonPanel.transform.childCount; x++) {
			Destroy (buttonPanel.transform.GetChild (x).gameObject);

		}

		GameObject exitButton = null;

		exitButton  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		exitButton.GetComponentInChildren<Button>().onClick.AddListener (() => {
			RiggedFight script 		= scriptFight.AddComponent<RiggedFight>();
			script.enemyIdArray 	= new int[]{201};
			script.riggedFlagName 	= "Augustus";
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

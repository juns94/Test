using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenePickerManager : MonoBehaviour {


	public GameObject UIbutton;
	public InteractionManager manager;
	public SexSceneContainer sexSceneContainer;
	public DialogManager dialog;
	SceneHub sceneHub;


	Character character;
	public void setup () {
		character = manager.currentCharacter;
		sceneHub = LewdSceneManager.getHubFromId (character.id);
		if (sceneHub != null) {
			Debug.Log (" SCENE PICKER MANAGER LOADED: " + character.name + sceneHub.count);
			MakeButtons ();
		}	

	}

	void MakeButtons(){
		for (int x = 0; x < sceneHub.count; x++) {
		 	GameObject temp = Instantiate (UIbutton);
			temp.transform.parent = gameObject.transform;
			temp.transform.localScale = Vector3.one;
			temp.GetComponentInChildren<Text> ().text = sceneHub.names[x];
			temp.transform.localPosition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0);	
			temp.name = "" + x;

			temp.GetComponent<Button> ().onClick.AddListener (() => {	
				selectAction (sceneHub.get(int.Parse (temp.name)));
				dialog.showBoxAutoHide(new string[]{ manager.currentCharacter.name + ": "+ sceneHub.acceptText});
			});

		}


		for (int x = 0; x < 4; x++) {
			GameObject temp = Instantiate (UIbutton);
			temp.transform.parent = gameObject.transform;
			temp.transform.localScale = Vector3.one;
			temp.GetComponentInChildren<Text> ().text = "???";
			temp.transform.localPosition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0);	
			temp.name = "" + x;
			//temp.GetComponent<Image>().color = new Color(200,200,200);
			temp.GetComponent<Button> ().onClick.AddListener (() => {	
			//	selectAction (sceneHub.get(int.Parse (temp.name)));
			//	dialog.showBoxAutoHide(new string[]{ manager.currentCharacter.name + ": "+ sceneHub.acceptText});
			});

		}
	}

	void selectAction(Scene scene){
		sexSceneContainer.setScene (scene);
		Invoke ("goToSex",3f);
	}
	void goToSex(){
		SceneManager.LoadScene ("BattleSceneSex");
	}


	

}

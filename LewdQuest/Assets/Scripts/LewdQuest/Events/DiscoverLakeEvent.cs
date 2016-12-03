using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiscoverLakeEvent : Event {


	GameObject buttonNo;
	GameObject buttonYes;

	public override void prepareScene(){
		mainImage.GetComponent<Image>().sprite = Resources.Load <Sprite> ("_wip");  
		string flavor = "You decide to walk a little further than usual this time. As you walk by, you encounter a small puddle of water, followed by what seems to be a small" +
			" stream that slowly grows in size. You walk slowly along the line of the water to find a big mass of water. You've discovered the Lake.";
		textPanel.GetComponentInChildren<Text>().text = flavor;



		buttonNo  = instanceButton ("Prefabs/AttackBtn", buttonPanel.transform);
		buttonNo.GetComponentInChildren<Button>().onClick.AddListener (() => {
			PlayerPrefs.SetInt("btnLake!", 1 );
			PlayerPrefs.Save();
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

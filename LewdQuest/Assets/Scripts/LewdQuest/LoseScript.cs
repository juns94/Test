                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour {

	public DialogManager dialog;
	public CanvasGroup canvas;
	GameData gameData;
	bool isFading = false;
	float fadeAmount;

	void Start () {
		gameData = DataAccess.Load ();
		Invoke ("showText",2f);
	}

	void Update () {


		if (isFading) {
			canvas.alpha -= fadeAmount;
			fadeAmount += 0.000025f;

			if (canvas.alpha <= 0)
				goToMap ();
		}
	}


	public void showText(){
		if (Random.Range (0, 6) == 5)
			dialog.showBox (new string[]{ " Wubba lubba...dub dub" });
		else
			dialog.showBox (new string[]{ " You pass out on the ground, but your fight isn't over yet."});
	
	
		Invoke ("fade", 4f);
	}

	public void fade(){
		isFading = true;
	}


	public void goToMap(){
	//	DataAccess.Save (gameData);

		PlayerPrefs.SetInt("hp", HeroUtils.getTotalHP ());
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("MapScene");
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {


	Text text;

	CanvasGroup imageCanvas;
	CanvasGroup canvas;
	public Image image;
	bool fading = false;
	bool badprogramming = false;

	void Start () {
		imageCanvas = image.GetComponent<CanvasGroup> ();
		canvas = GetComponent<CanvasGroup> ();
		text = GetComponent<Text> ();
	


		Invoke("Dim", 6f);

		Invoke ("showImage", 8);
		Invoke("changeText", 10f);
		Invoke ("Dim",	16);
		Invoke ("DimPic", 16);
		Invoke("changeFinalText", 18f);
	}
	
	// Update is called once per frame
	void Update () {

		if (fading) {
			canvas.alpha -= 0.01f;
		} else {
			canvas.alpha += 0.01f;
		}


		if (image.isActiveAndEnabled && badprogramming == false) {

			imageCanvas.alpha += 0.01f;
		}
		if (badprogramming) {
			imageCanvas.alpha -= 0.01f;
		}

	
	}
	public void Dim(){
		fading = true;

	}

	public void showImage(){
		image.gameObject.SetActive (true);
	}


	void DimPic(){

		badprogramming = true;
	}

	void changeText(){
		fading = false;

		text.text = " You were framed for committing crimes against the state, debauchery and assault, all in a witch hunt for degeneracy running around in the Kingdom. ";

	}

	void changeFinalText(){
		fading = false;
		text.text = " You've been exiled from the Capital, now you're out on your own, spending the night in the old family cabin in the woods, secluded from people. This is the beginning of your Quest.";


		Invoke ("goToMap", 7f);
	}

	public void goToMap(){

	
			SceneManager.LoadScene ("MapScene");


	}
}

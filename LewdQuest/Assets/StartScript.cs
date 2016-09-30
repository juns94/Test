using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {


	public GameObject titleImage;
	public GameObject curtains;
	// Use this for initialization
	void Start () {
	
		Invoke ("startAnimation", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startAnimation(){

		titleImage.GetComponent<Animator> ().Play ("slideDownIntro");

	}

	public void GoToMap(){

		SceneManager.LoadScene ("MapScene");

	}
}

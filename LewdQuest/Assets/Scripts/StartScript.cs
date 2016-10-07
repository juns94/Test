﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {


	public GameObject titleImage;
	public GameObject curtains;
	public GameObject buttonsPanel;
	// Use this for initialization
	void Start () {
	
		Invoke ("startAnimation", 1f);
		Invoke ("displayButtons", 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startAnimation(){

		titleImage.GetComponent<Animator> ().Play ("slideDownIntro");
	
	}

	public void displayButtons(){
		
		buttonsPanel.GetComponent<Animator> ().Play ("startButtonAnimation");
	}

	public void GoToMap(){

		SceneManager.LoadScene ("MapScene");

	}
}
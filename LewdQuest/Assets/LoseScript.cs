﻿
                                                                                                                                                                                                                                                                                                                                                                           using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void retry (){

		SceneManager.LoadScene ("MapScene");
		PlayerPrefs.SetInt ("hp", 230);

	}
}

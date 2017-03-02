using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;


public class MapManager : MonoBehaviour {


	public int region = 0;
	private int currentRegionStepped;
	void Start () {
		DontDestroyOnLoad (this);
	}
	void Update () {

		if (Input.GetKeyDown(KeyCode.G)) {
			PlayerPrefs.SetInt ("gold", 600);
		}
	}

	public void setCurrentRegionStepped(int reg){
		currentRegionStepped = reg;
	}

	public int getEnemyNumber(){
		int number = 0;
		if (region == 2) {
			return 1;
		}
			
		switch (region) {
		case 1: ///FOREST
			number = (Random.Range (0, 2) + Random.Range (0, 2) + Random.Range (0, 2));
			break;

		case 2: ///MOUNTAINS
			break;


		case 3: ///PLAINS
			number = Random.Range (1, 2);
			break;

		case 4: ///LAKE
			number = Random.Range (1, 2);
			break;
		}


		if (number == 0)
			number = 1;
		return number;

	}


	public void rest(){
		GameObject.Find ("FadingObj").GetComponent<Fader> ().blink ();
		PlayerPrefs.SetInt ("hp",PlayerPrefs.GetInt("hpTotal",0	));
		PlayerPrefs.SetInt ("energy", 100);
		PlayerPrefs.Save ();
		LewdUtilities.getGameData (GameObject.Find ("GameData")).restSluts();
	}



	public void transition(){
	

		switch (Random.Range(0,6)) {

		case 0:var slices = new VerticalSlicesTransition()
			{
				nextScene = 2,
				divisions = Random.Range( 3, 20 )
			};
			TransitionKit.instance.transitionWithDelegate( slices );
			break;
		

		


		case 1:
			var slicestri = new TriangleSlicesTransition()
			{
				nextScene = 2,
				divisions = Random.Range( 2, 10 )
			};
			TransitionKit.instance.transitionWithDelegate( slicestri );
			break;


		case 2:
			var enumValues = System.Enum.GetValues (typeof(PixelateTransition.PixelateFinalScaleEffect));
			var randomScaleEffect = (PixelateTransition.PixelateFinalScaleEffect)enumValues.GetValue (Random.Range (0, enumValues.Length));

			var pixelater = new PixelateTransition () {
				nextScene = 2,
				finalScaleEffect = randomScaleEffect,
				duration = 1.0f
			};
			TransitionKit.instance.transitionWithDelegate (pixelater);
			break;


	



		case 3:
			var ripple = new RippleTransition () {
				nextScene = 2,
				duration = 1.0f,
				amplitude = 1500f,
				speed = 20f
			};
			TransitionKit.instance.transitionWithDelegate (ripple);
			break;


		case 4:
			var fishEye = new FishEyeTransition () {
				nextScene = 2,
				duration = 1.0f,
				size = 0.08f,
				zoom = 10.0f,
				colorSeparation = 3.0f
			};
			TransitionKit.instance.transitionWithDelegate (fishEye);
			break;


		case 5:
			var fishEye2 = new FishEyeTransition()
			{
				nextScene =2,
				duration = 2.0f,
				size = 0.2f,
				zoom = 100.0f,
				colorSeparation = 0.1f
			};
			TransitionKit.instance.transitionWithDelegate( fishEye2 );
			break;

			/*
		if( GUILayout.Button( "Doorway to Scene" ) )
		{
			var doorway = new DoorwayTransition()
			{
				nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
				duration = 1.0f,
				perspective = 1.5f,
				depth = 3f,
				runEffectInReverse = false
			};
			TransitionKit.instance.transitionWithDelegate( doorway );
		}


		if( GUILayout.Button( "Doorway (reversed) to Scene" ) )
		{
			var doorway = new DoorwayTransition()
			{
				nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
				duration = 1.0f,
				perspective = 1.1f,
				runEffectInReverse = true
			};
			TransitionKit.instance.transitionWithDelegate( doorway );
		}


		if( GUILayout.Button( "Wind to Scene" ) )
		{
			var wind = new WindTransition()
			{
				nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
				duration = 1.0f,
				size = 0.3f
			};
			TransitionKit.instance.transitionWithDelegate( wind );
		}


		if( GUILayout.Button( "Curved Wind to Scene" ) )
		{
			var wind = new WindTransition()
			{
				useCurvedWind = true,
				nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
				duration = 1.0f,
				size = 0.3f,
				windVerticalSegments = 300f
			};
			TransitionKit.instance.transitionWithDelegate( wind );
		}


		if( GUILayout.Button( "Mask to Scene" ) )
		{
			var mask = new ImageMaskTransition()
			{
				maskTexture = maskTexture,
				backgroundColor = Color.yellow,
				nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1
			};
			TransitionKit.instance.transitionWithDelegate( mask );
	
		}
	}*/
		}
	}


	public void encounterPlains(){
		if (hasEnergyCheck ()) {
			region = 3;
			if (Random.Range (1, 5) == 	4)
				SceneManager.LoadScene ("EncounterScene");
			else
				transition ();
		}

	}

	public void encounterLake(){
		if (hasEnergyCheck ()) {
			region = 4;
		//	if (Random.Range (0, 5) < 1)
		//		SceneManager.LoadScene ("EncounterScene");
		//	else
			transition ();
		}

	}


	public void encounterMountain(){
		if (hasEnergyCheck ()) {
			region = 2;
			if (Random.Range (0, 5) < 1)
				SceneManager.LoadScene ("EncounterScene");
			else
				transition ();
		}

	}


	public void encounterForest(){

		if (hasEnergyCheck ()) {
			region = 1;
			if (Random.Range (0, 5) < 1)
				SceneManager.LoadScene ("EncounterScene");
			else
				transition ();
		}
	}



	public void resetAllCharacters(){
		DataAccess.RESET ();
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
		HeroUtils.setHero ();


	}

	public void encounter(){

		switch (currentRegionStepped) {
		case 1:
			encounterForest ();
			break;
		case 2:
			encounterMountain ();
			break;
		case 3:
			encounterPlains ();
			break;
		case 4: 
			encounterLake ();
			break;
		case 100: 
			rest();
			break;	

		case 90: 
			goToGrandCity();
			break;


		
		}


	}


	public void goToParty(){
		GameObject.Find ("YOU").GetComponent<MapMovementController>().saveCurrentPosition();
		SceneManager.LoadScene ("PartyScene");
	}

	public void goToGrandCity(){

		if (LewdUtilities.getPartyBitches ().Count > 0 & PlayerPrefs.GetInt ("Augustus", 0) == 0) {
			region = 201;		
			SceneManager.LoadScene ("EncounterScene");
		} else {
			Destroy (GameObject.Find ("MapManager"));
			SceneManager.LoadScene ("GrandCityScene");
		}
	}

	public void goToMap(){
		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("MapScene");

	}

	public void goToStats(){
		GameObject.Find ("YOU").GetComponent<MapMovementController>().saveCurrentPosition();
		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("StatsScene");

	}



	public bool hasEnergyCheck(){
		return true;/*

		if (PlayerPrefs.GetInt ("energy", 100) > 0) {
			PlayerPrefs.SetInt ("energy", PlayerPrefs.GetInt ("energy", 100) - 10);
			PlayerPrefs.Save ();
			return true;

		}
			
		return false;*/
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GrandCityManager : MonoBehaviour {







	public void GoToGiftShop(){

		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("GiftShopScene");

	}


	public void goToShop(){

		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("ShopScene");

	}
}

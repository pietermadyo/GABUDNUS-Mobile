using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CaraBermain : MonoBehaviour {

	//string GABUDNUS_ADDRESS = "http://gabudnus.firebaseapp.com";

	//public void InfoGabudnus()
	//{
	//	Application.OpenURL (GABUDNUS_ADDRESS);
	//}

	public void backtoMenu()
	{
		SceneManager.LoadScene ("Menu");
	}
}

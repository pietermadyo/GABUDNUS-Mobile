using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class RondeController : MonoBehaviour {

	public string rondeDipilih;
	public Button R1;
	public Button R2;
	public Button R3;
	public Button R4;
	public Button R5;
	public GameObject k2,k3,k4,k5;

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		CheckRonde ();
	}

	void awake()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(EventSystem.current.currentSelectedGameObject)
		{
			pilihronde ();
		}

		//CheckRonde ();
	}

	private void CheckRonde ()
	{
		Debug.Log("cekronde");
		R1.enabled = (true);
		R2.enabled = (false);
		R3.enabled = (false);
		R4.enabled = (false);
		R5.enabled = (false);

		if (PlayerPrefs.GetInt ("r1") == 0) 
		{
			R1.enabled = (true);
			Debug.Log ("Ronde 1 Aktif");

		}
		if (PlayerPrefs.GetInt ("r2") == 1) 
		{
			R2.enabled = (true);
			Debug.Log ("Ronde 2 Aktif");
			k2.SetActive(false);

		} 
		if (PlayerPrefs.GetInt ("r3") == 1) 
		{
			R3.enabled = (true);
			Debug.Log ("Ronde 3 Aktif");
			k3.SetActive(false);

		} 
		if (PlayerPrefs.GetInt ("r4") == 1) 
		{
			R4.enabled = (true);
			Debug.Log ("Ronde 4 Aktif");
			k4.SetActive(false);

		} 
		if (PlayerPrefs.GetInt ("r5") == 1) 
		{
			R5.enabled = (true);
			Debug.Log ("Ronde 5 Aktif");
			k5.SetActive(false);

		} 
	}

	public void pilihronde()
	{
		rondeDipilih = EventSystem.current.currentSelectedGameObject.name;

	}


}

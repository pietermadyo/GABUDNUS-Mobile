using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
	public GameObject panelLogout;
	public Button masuk;
	public AudioMixer mySound;
	public Button suara;
	public Sprite Mute;
	public Sprite UnMute;
	public string user;
	public AudioSource btnklik;
	//public GameObject panelCekInternet;
	public Datakuis dk;

	public void Start()
	{		
		//dk = new Datakuis ();
		DontDestroyOnLoad (gameObject);
		dk = FindObjectOfType<Datakuis> ();
		if(dk.DataSoal.Count == 0)
		{
			//if (Application.internetReachability == NetworkReachability.NotReachable) 
			//{
			//	panelCekInternet.SetActive (true);
			//} else {
				dk.getData ();
			//}
		}



		float backsound = PlayerPrefs.GetFloat ("backsound");
		mySound.SetFloat("volume", backsound);
		if (backsound == 0.0f)
		{
			suara.image.sprite = UnMute;
		} 
		else
		{
			suara.image.sprite = Mute;
		}
		user = PlayerPrefs.GetString("email");
		Debug.Log (user);

		if (user == "")
		{
			masuk.gameObject.GetComponentInChildren<Text> ().text = "Masuk";
		} 
		else
		{
			//user = user.Split('@'); 
			string[] name= user.Split('@');

			//if(name.Length > 10)
			//{
			//	name = name.Substring (0,10).ToUpper().ToString();
			//}
			masuk.gameObject.GetComponentInChildren<Text> ().text = name[0];
		}
	}


		
	public void tekanbtnSuara()
	{
		//ketika menekan untuk nyalakan suara
		if (suara.image.sprite == Mute)
		{
			suara.image.sprite = UnMute;
			mySound.SetFloat ("volume", 0.0f);
			PlayerPrefs.SetFloat ("backsound",0.0f);
		}
		else if (suara.image.sprite == UnMute)
		{
			//ketika menekan untuk matikan suara
			suara.image.sprite = Mute;
			mySound.SetFloat("volume", -80.0f);
			PlayerPrefs.SetFloat ("backsound",-80.0f);
		}
	}

	public void LevelGame()
	{
		
		SceneManager.LoadScene ("Level");
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("Game");
	}

	public void StartGameCarabermain()
	{
		SceneManager.LoadScene ("CaraBermain");
	}

	public void StartGamePeringkat()
	{
		SceneManager.LoadScene ("Peringkat");
	}

	public void pindahscenesuaraDaftarakun()
	{
		string daftarAkun = "DaftarAkun";
		StartCoroutine (SceneChangerDaftarAkun (btnklik, daftarAkun));
	}

	public IEnumerator SceneChangerDaftarAkun(AudioSource audioSource,string daftarAkun)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) {
			SceneManager.LoadScene (daftarAkun);
		}
	}

	public void Daftar()
	{
		user = PlayerPrefs.GetString("email");
		if (user == "") 
		{
			//PlayerPrefs.SetInt ("score",0);
			pindahscenesuaraDaftarakun ();
			//SceneManager.LoadScene ("DaftarAkun");
			Debug.Log (user);
		}
		else
		{
			panelLogout.SetActive (true);
		}	
	}

	public void logout()
	{
		PlayerPrefs.SetString("email","");
		PlayerPrefs.SetInt ("score",0);
		panelLogout.SetActive (false);
		masuk.gameObject.GetComponentInChildren<Text> ().text = "Masuk";
		Debug.Log (user);
		unsetRonde ();
	}

	public void batal()
	{
		panelLogout.SetActive(false);
	}
		
	public void unsetRonde()
	{
		Debug.Log ("App unset");
		PlayerPrefs.SetInt ("CountSoal",0);
		PlayerPrefs.SetInt ("r1",0);
		PlayerPrefs.SetInt ("r2",0);
		PlayerPrefs.SetInt ("r3",0);
		PlayerPrefs.SetInt ("r4",0);
		PlayerPrefs.SetInt ("r5",0);
		PlayerPrefs.SetInt ("jb",0);
		PlayerPrefs.SetInt ("Jbronde",0);
		PlayerPrefs.SetString("email","");
		PlayerPrefs.SetInt ("score",0);
		PlayerPrefs.SetString ("datakuis", "");
	}

}
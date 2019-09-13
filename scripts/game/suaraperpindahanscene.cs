using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class suaraperpindahanscene : MonoBehaviour {

	//public AudioSource suara;
	public AudioSource btnklik;
	public AudioSource mulaigame;
	public AudioSource jwbBener;
	public AudioSource jwbSalah;
	//public AudioSource masukApp;
	public AudioSource keluarApp;

	public Button btnDisbleronde1;
	public Button btnDisbleronde2;
	public Button btnDisbleronde3;
	public Button btnDisbleronde4;
	public Button btnDisbleronde5;

	public Button menuAyomain;
	public Button menuCarabermain;
	public Button menuPeringkat;
	public Button menuDaftar;
	public Button menuKeluar;



	//string GABUDNUS_Peringkat = "http://gabudnus.firebaseapp.com/leaderboard.html";
	string GABUDNUS_Peringkat = "https://gabudnus.000webhostapp.com/tampilperingkat.php";//"http://localhost:88/gabud/tampilperingkat.php";
	string GABUDNUS_ADDRESS = "https://gabudnus.000webhostapp.com/";//"http://localhost:88/gabud/index.php";

	public void pindahscenesuaraKeluar()
	{
		StartCoroutine (SceneChangerKeluar (keluarApp));
		menuKeluar.enabled = false;
	}

	public void pindahscenesuaraPeringkatUrl()
	{
		StartCoroutine (SceneChangerUrl (btnklik, GABUDNUS_Peringkat));
	}

	public void pindahscenesuaraCaraBermainUrl()
	{
		StartCoroutine (SceneChangerUrl (btnklik, GABUDNUS_ADDRESS));
	}

	public void pindahscenesuaraMenu()
	{
		string menu = "Menu";
		StartCoroutine (SceneChangerMenu (btnklik, menu));
	}

	public void pindahscenesuaraCarabermain()
	{
		string caraBermain = "CaraBermain";
		StartCoroutine (SceneChangerCaraBermain (btnklik, caraBermain));
		menuCarabermain.enabled = false;
	}

	public void pindahscenesuaraDaftarakun()
	{
		string daftarAkun = "DaftarAkun";
		StartCoroutine (SceneChangerDaftarAkun (btnklik, daftarAkun));
		menuDaftar.enabled = false;
	}

	public void pindahscenesuaraLevel()
	{
		string level = "Level";
		StartCoroutine (SceneChangerLevel (btnklik, level));
		menuAyomain.enabled = false;
	}

	public void pindahscenesuaraGame()
	{
		string game = "Game";
		StartCoroutine (SceneChangerGame (mulaigame, game));
		btnDisbleronde1.enabled = false;
		btnDisbleronde2.enabled = false;
		btnDisbleronde3.enabled = false;
		btnDisbleronde4.enabled = false;
		btnDisbleronde5.enabled = false;
	}

	public void pindahscenesuaraPeringkat()
	{
		string peringkat = "Peringkat";
		StartCoroutine (SceneChangerPeringkat (btnklik, peringkat));
		menuPeringkat.enabled = false;
	}

	public IEnumerator SceneChangerMenu(AudioSource audioSource,string menu)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) {
			SceneManager.LoadScene (menu);
		}
	}
		
	public IEnumerator SceneChangerCaraBermain(AudioSource audioSource,string carabermain)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) {
			SceneManager.LoadScene (carabermain);
		}
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

	public IEnumerator SceneChangerLevel(AudioSource audioSource,string level)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
	 	{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) {
			SceneManager.LoadScene (level);
		}
	}

	public IEnumerator SceneChangerGame(AudioSource audioSource,string game)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) {
			SceneManager.LoadScene (game);
		}
	}
		
	public IEnumerator SceneChangerPeringkat(AudioSource audioSource,string peringkat)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) {
			SceneManager.LoadScene (peringkat);
		}
	}

	public IEnumerator SceneChangerKeluar(AudioSource audioSource)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) 
		{
			PlayerPrefs.SetInt ("score",0);
			Application.Quit();
		}
	}

	public IEnumerator SceneChangerUrl(AudioSource audioSource,string url)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
		if (!audioSource.isPlaying) {
			Application.OpenURL(url);
		}
	}
}

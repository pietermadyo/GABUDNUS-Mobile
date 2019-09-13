using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PeringkatFirebase : MonoBehaviour {
	

	string GABUDNUS_Peringkat = "http://gabudnus.firebaseapp.com/leaderboard.html";

	public GameObject playerScore;
	public GameObject Peringkat;
	public AudioSource suara;

	public void backtomenu()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void PeringkatGabudnus()
	{
		Application.OpenURL (GABUDNUS_Peringkat);
	}

	public void pindahsuarascenewaktubtn()
	{
		string game = "Menu";
		StartCoroutine (SceneChanger (suara, game));
	}

	public IEnumerator SceneChanger(AudioSource audioSource,string game)
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

	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://gabudnus.firebaseio.com/");
	}

	void Update()
	{
		
		while (this.transform.childCount > 0)
		{
			Transform c = this.transform.GetChild (0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}

		FirebaseDatabase.DefaultInstance.GetReference("User").OrderByChild("order").ValueChanged += (object sender, ValueChangedEventArgs e) => 
		{
			if(e.DatabaseError!=null)
			{
				Debug.LogError(e.DatabaseError.Message);
				return;
			}
			if(e.Snapshot !=null && e.Snapshot.ChildrenCount>0)
			{
				foreach(var childSnapshot in e.Snapshot.Children)
				{
					if(childSnapshot.Child("score")==null || childSnapshot.Child("score").Value==null) 
					{
						Debug.Log("Terjadi Error");
						break;
					}
					else
					{
						GameObject go = (GameObject)Instantiate(playerScore);
						go.transform.SetParent(this.transform);
						go.transform.Find("txtEmail").GetComponent<Text>().text = childSnapshot.Child("email").Value.ToString();
						go.transform.Find("txtNilai").GetComponent<Text>().text = childSnapshot.Child("score").Value.ToString();
					}
				}
			}
		};
	}
} 
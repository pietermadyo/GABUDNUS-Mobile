using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dbgetScorePeringkat : MonoBehaviour {


	string CreateUserUrlgetscore = "https://gabudnus.000webhostapp.com/getscore.php";//"localhost:88/gabudnus/getscore.php";
	//string CreateUserUrlrank = "https://gabudnus.000webhostapp.com/ranking.php";//"localhost:88/gabudnus/ranking.php";

	public GameObject playerScoreEntryPrefab;
	public GameObject leaderboardPanel;

	//public GameObject letakno;
	//public GameObject letakemail;
	//public GameObject letaknilai;

	// Use this for initialization
	void Start () {
		StartCoroutine (GetTopScores());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator GetTopScores()
	{
		WWW GetScoresAttempt = new WWW (CreateUserUrlgetscore);
		yield return GetScoresAttempt;

		while(this.transform.childCount>0){
			Transform c = this.transform.GetChild (0);
			c.SetParent (null);
			Destroy (c.gameObject);
		}

		if (GetScoresAttempt.error == null) {
			string[] textlist = GetScoresAttempt.text.Split (new string[] { "\n", "\t" }, System.StringSplitOptions.RemoveEmptyEntries);

			string[] Names = new string[Mathf.FloorToInt (textlist.Length / 2)];
			string[] Scores = new string[Names.Length];

			for (int i = 0; i < textlist.Length; i++) {
				if (i % 2 == 0) {     
					Names [Mathf.FloorToInt (i / 2)] = textlist [i];
				} 
				else Scores [Mathf.FloorToInt (i / 2)] = textlist [i];
			}

			for (int i = 0; i < Names.Length; i++) {
				GameObject go = Instantiate (playerScoreEntryPrefab) as GameObject;
				go.transform.SetParent (this.transform);

				//string nama = "";
				//nama = Names [i].Substring (0,name.IndexOf ("@") + 1).ToString();
				//\r\n\t\t\t\t//name = name.SubString (0, name.IndexOf ("@") + 1);

				//string name = Names [i].ToString(); 
				//if(Names [i].Length >= 22)
				//{
					 //name = Names [i].Substring (0,22).ToString();
				//}
				//go.transform.Find ("txtNo").GetComponent<Text> ().rectTransform.position = letakno.transform.position;
				//go.transform.Find("txtEmail").GetComponent<Text> ().rectTransform.position = letakemail.transform.position;
				//go.transform.Find ("txtNilai").GetComponent<Text> ().rectTransform.position = letaknilai.transform.position;

				go.transform.Find ("txtNo").GetComponent<Text> ().text = "" + (i + 1);
				go.transform.Find("txtEmail").GetComponent<Text> ().text = Names[i].ToString();
				go.transform.Find("txtNilai").GetComponent<Text> ().text = Scores[i].ToString();
			}

		}
		else
		{
			Debug.Log ("Salah di Top Scores");
		}
	}
}

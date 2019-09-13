using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
using UnityEngine.SceneManagement;
using LitJson;

[System.Serializable]
public class Item
{
	public int id_pertanyaan;
	public string teks;
	public string url;
	public string gambar;
	public string pilihan1;
	public string pilihan2;
	public string pilihan3;
	public string pilihan4;
	public int pilihanBenar;
	public int nilai;

	public Item (int id_Pertanyaan, string teksBaru,string urlBaru, string _gambar,string pil1,string pil2, string pil3, string pil4,  int nilaiBaru, int pilbenar)
	{
		id_pertanyaan = id_Pertanyaan;
		teks = teksBaru;
		url = urlBaru;
		gambar = _gambar;
		pilihan1 = pil1;
		pilihan2 = pil2;
		pilihan3 = pil3;
		pilihan4 = pil4;
		pilihanBenar = pilbenar;
		nilai = nilaiBaru;
	}
}


public class Datakuis : MonoBehaviour {


	//public float barDisplay; //current progress
	//public GameObject DisplayProgress;
	//public GameObject backgroundProgress;
	//public Text textnilaiprogress; 
	//public Vector2 pos = new Vector2(0,100);
    //public Vector2 size = new Vector2(0,0);
    //public Texture2D emptyTex;
    //public Texture2D fullTex;

	public List<Item> DataSoal;
	public List<Item> DataSoalTampung;
	public JsonData itemdata;
	string pil1, pil2, pil3, pil4;
	public GameObject panelLoading;
	public GameObject panelCekInternet;
	public GameObject panelError;
	public GameObject panellastData;
	public Text errortext;
	//public Slider slideBar;
	//public Text loadingTeks;

	//public MenuController menuController;


	void Start () 
	{
		Debug.Log ("start" + DataSoal.Count);
		//if(DataSoal.Count == 0)
		//{
			Debug.Log ("ambil data");
			DontDestroyOnLoad (gameObject);
			DataSoal = new List<Item> ();
			DataSoalTampung = new List<Item> ();
			//menuController = new MenuController ();
			//SceneManager.LoadScene ("Loading");

			StartCoroutine (GetDataKuis ());
			//StartCoroutine (LoadNewScene ());
		//}

	

	}



	public void getData()
	{
		hideLastdata ();
		hideError ();
		DontDestroyOnLoad (gameObject);
		DataSoal = new List<Item> ();
		DataSoalTampung = new List<Item> ();
		//menuController = new MenuController ();
		//SceneManager.LoadScene ("Loading");

		StartCoroutine (GetDataKuis ());
	}

	public void showpanelCekInternet()
	{
		panelCekInternet.SetActive(true);
	}

	public void hidepanelCekInternet()
	{
		panelCekInternet.SetActive(false);
	}

	public void showLoading()
	{
		panelLoading.SetActive(true);
	}

	public void hideLoading()
	{
		panelLoading.SetActive(false);
	}
	/*
	void Update()
	{
        //barDisplay = Time.time*0.05f;
 		//barDisplay = MyControlScript.staticHealth;
     }*/

	IEnumerator GetDataKuis()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			panelCekInternet.SetActive (true);
			yield return null;
		} 
		else 
		{
			
		string url = "https://gabudnus.000webhostapp.com/GetQues.php";//"http://localhost:88/gabudnus/GetQues.php";
		WWW www = new WWW (url);
		//StartCoroutine(ShowProgress(www));
		while (!www.isDone)
		{
			showLoading ();
			//Debug.Log(string.Format("Downloaded {0:P1}", www.progress));
			yield return null;
		}

		yield return www;
		
		if (www.error == null)
        {
			Debug.Log ("Ambil data baru");
            Processjson(www.data);
			string data = PlayerPrefs.GetString ("datakuis");
			if (string.IsNullOrEmpty (www.text)) {
				PlayerPrefs.SetString ("datakuis", www.data);
			} 
			else
			{
				PlayerPrefs.SetString ("datakuis", "");
				PlayerPrefs.SetString ("datakuis", www.data);
			}

			hideLoading ();
        }
        else
        {
			
            Debug.Log("ERROR: " + www.error);
			//getLastData ();
			hideLoading ();
			showError(www.error.ToString());
			
         }   
		}
	}
		

	public void CheckLastData()
	{
		hidepanelCekInternet ();
		string data = PlayerPrefs.GetString ("datakuis");

		if (Application.internetReachability == NetworkReachability.NotReachable && !string.IsNullOrEmpty (data)) {
			showLastdata ();
		} 
		else if (Application.internetReachability == NetworkReachability.NotReachable) {
			showpanelCekInternet ();
		} 
		else {
			getData ();
		}
	}

	public void showLastdata()
	{
		panellastData.SetActive(true);
	}

	public void hideLastdata()
	{
		panellastData.SetActive(false);
	}
		
	public void getLastData()
	{
		hideLastdata ();
		string data = PlayerPrefs.GetString ("datakuis");
		if (!string.IsNullOrEmpty (data)) {
			Processjson (data);
			Debug.Log ("Ambil data terakhir");
		} 
		else 
		{
			Debug.Log("Data kosong");
		}
	}

	public void showError(string error)
	{
		errortext.text = error;
		panelError.SetActive(true);
	}

	public void hideError()
	{
		panelError.SetActive(false);
	}
	
	private void Processjson(string jsonString)
    {
        JsonData jsonvale = JsonMapper.ToObject(jsonString);
		//string pil1, pil2, pil3, pil4;
        for(int i = 0; i<jsonvale["data"].Count; i++)
        {
			int id_pertanyaan = Convert.ToInt32(jsonvale["data"][i]["ID_PERTANYAAN"].ToString());
			//Debug.Log ("id: " + id_pertanyaan);
			string teks = jsonvale["data"][i]["TEKS_PERTANYAAN"].ToString();
			//Debug.Log ("Teks: " + teks);
			string url = jsonvale["data"][i]["URL_PERTANYAAN"].ToString();
			//Debug.Log ("Url: " + url);
			string gambar = jsonvale["data"][i]["GAMBAR"].ToString();
			//Debug.Log ("Gambar: " + gambar);
			string jawaban = jsonvale["data"][i]["PILIHAN_JAWABAN"].ToString();
			//Debug.Log ("jawaban: " + jawaban);

			string[] pilihan = jawaban.Split (new String[] { "," }, StringSplitOptions.None);
			for (int j = 0; j < pilihan.Length; j++) 
			{
				if (j % 4 == 0) {
					pil1 = pilihan [j].ToString ();
				}
				if (j % 4 == 1) {
					pil2 = pilihan [j].ToString ();
				}
				if (j % 4 == 2) {
					pil3 = pilihan [j].ToString ();
				}
				if (j % 4 == 3) {
					pil4 = pilihan [j].ToString ();
				}
				//Debug.Log (j + "jawaban : " + pilihan [j]);
			}

			int pilihanBenar = Convert.ToInt32(jsonvale["data"][i]["JAWABAN_BENAR"].ToString());
			//Debug.Log ("PilBenar: " + pilihanBenar);
			int nilai = Convert.ToInt32(jsonvale["data"][i]["NILAI_PERTANYAAN"].ToString());
			//Debug.Log ("Nilai: " + nilai);

			DataSoal.Add (new Item (id_pertanyaan, teks, url, gambar, pil1, pil2, pil3, pil4, nilai, pilihanBenar));
			DataSoalTampung.Add (new Item (id_pertanyaan, teks, url, gambar, pil1, pil2, pil3, pil4, nilai, pilihanBenar));
        }    
    }



	public void loadingGetdata(int sceneIndex)
	{
		//StartCoroutine (getdatascene(sceneIndex));
	}


	/*
	private IEnumerator ShowProgress(WWW www)
	{
    	while (!www.isDone)
		{
			//barDisplay = www.progress;
			//DisplayProgress.SetActive(true);
			//backgroundProgress.SetActive(true);
			//textnilaiprogress = barDisplay.ToString(); 
	        Debug.Log(string.Format("Downloaded {0:P1}", www.progress));
	        yield return new WaitForSeconds(.1f);
    	}
    	Debug.Log("Done");
	}

	void OnGUI()
	{
         //draw the background:
         GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
             GUI.Box(new Rect(10,10, size.x, size.y), emptyTex);
         
             //draw the filled-in part:
             GUI.BeginGroup(new Rect(10,10, size.x * barDisplay, size.y));
                 GUI.Box(new Rect(10,10, size.x, size.y), fullTex);
             GUI.EndGroup();
         GUI.EndGroup();
     }

*/

}

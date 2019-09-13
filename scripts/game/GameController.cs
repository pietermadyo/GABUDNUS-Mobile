using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	public AudioSource SuarajwbBener;
	public AudioSource SuarajwbSalah;

	public Text skor;
	public Text questionDisplayText;
	public Text scoreDisplayTeks;
	public Text timeRemainingDisplayTeks;
	public Transform answerButtonParent;

	public Button jwb1,jwb2,jwb3,jwb4;
	public string jawabanDipilih;
	public static int nilaiPemain;
	public static int totalscore;

	public GameObject questionDisplay;
	public GameObject roundEndDisplay;
	public GameObject benar;

	private Datakuis dataKuis;
	private Item dataSoal;
	private LoadGambar loadGambar;
	private RondeController rc; 

	private bool isRoundActive;
	private float sisawaktu = (float)0; 
	private int questionIndex;

	private int count = 0;
	private int c = 0;
	int id_soal;
	private bool pertanyaan;
	public List<Item> DataSoaltemp;
	private string rondeDipilih;
	private int nilai;
	private string currImage;

	string CreateUserUrl = "https://gabudnus.000webhostapp.com/addscore.php";//"localhost:88/gabudnus/addscore.php";
	string CreatePertanyaanUrl = "https://gabudnus.000webhostapp.com/addtoppertanyaan.php";//"localhost:88/gabudnus/addtoppertanyaan.php";

	public RawImage img;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		//img = this.gameObject.GetComponent<RawImage> ();
		dataKuis = FindObjectOfType<Datakuis> ();
		//loadGambar = FindObjectOfType<LoadGambar> ();
		rc = FindObjectOfType<RondeController> ();
		Debug.Log(dataKuis.DataSoal.Count);
		Debug.Log("di gameController : "+rc.rondeDipilih);
		rondeDipilih = rc.rondeDipilih;
		//pertanyaan = true;
		nilaiPemain = 0;
		scoreDisplayTeks.text = "Nilai: " + nilaiPemain.ToString();
		setJbronde (rondeDipilih);
		sisawaktu = (float)ronde(rondeDipilih);
		Debug.Log(sisawaktu);
		//sisawaktu = (float)10;
		//TampilPertanyaan (questionIndex);
		TampilPertanyaanSelanjutnya ();
		//UpdateTimeRemainingDisplay ();
		isRoundActive = true;
	}

	void Update()
	{
		//Debug.Log ("update");
		//UpdateTimeRemainingDisplay (true);
		//rondeDipilih = rc.rondeDipilih;

		if (pertanyaan) {
			
			//Debug.Log ("update : "+sisawaktu);
			sisawaktu -= Time.deltaTime;
			//Debug.Log (":"+pertanyaan);
			if(sisawaktu != null)
			{
				UpdateTimeRemainingDisplay (sisawaktu);
			}

			if (sisawaktu <= 0f) {
				//sisawaktu = (float) 10;
				pertanyaan = false;

				jwb1.enabled = false;
				jwb2.enabled = false;
				jwb3.enabled = false;
				jwb4.enabled = false;
				TampilPertanyaanSelanjutnya ();

				//UpdateTimeRemainingDisplay (sisawaktu);
			}
		} 

		if(dataKuis.DataSoal.Count == 0)
		{
			dataKuis.DataSoal.AddRange (dataKuis.DataSoalTampung);
		}
	}

	public void run(bool run)
	{
		pertanyaan = run;
	}

	public void UpdateTimeRemainingDisplay(float waktu)
	{
		timeRemainingDisplayTeks.text = "Waktu: " + Mathf.Round (waktu).ToString ();	
	}

	public void refreshImage()
	{
		showGambar(currImage);
		sisawaktu = ronde(rondeDipilih);
	}

	private void TampilPertanyaan(int index)
	{
		
		dataSoal = dataKuis.DataSoal[index];

		id_soal = dataSoal.id_pertanyaan;
		addtopPertanyaan (id_soal);
		//loadGambar.showGambar (dataSoal.url);
		currImage = dataSoal.url;


		//show gambar pake url
		//showGambar(dataSoal.url);

		GetImage (dataSoal.gambar);

		questionDisplayText.text = dataSoal.teks;
		jwb1.gameObject.GetComponentInChildren<Text> ().text = dataSoal.pilihan1;
		jwb2.gameObject.GetComponentInChildren<Text> ().text = dataSoal.pilihan2;
		jwb3.gameObject.GetComponentInChildren<Text> ().text = dataSoal.pilihan3;
		jwb4.gameObject.GetComponentInChildren<Text> ().text = dataSoal.pilihan4;
		nilai = dataSoal.nilai;
		//pertanyaan = true;

		//pertanyaan = ;
		//Debug.Log(pertanyaan);
		questionDisplay.SetActive (true);
		roundEndDisplay.SetActive (false);
	
		dataKuis.DataSoal.RemoveAt(index);
	}
	
	private float ronde(string rondeDipilih)
	{
		float waktu = 0;
		//rondeDipilih = EventSystem.current.currentSelectedGameObject.name;

		if (String.Compare (rondeDipilih, "Ronde 1") == 0) 
		{
			waktu = 20;
		} 
		else if (String.Compare (rondeDipilih, "Ronde 2") == 0)
		{
			waktu = 17;
		}
		else if (String.Compare (rondeDipilih, "Ronde 3") == 0) 
		{
			waktu = 14;
		}
		else if (String.Compare (rondeDipilih, "Ronde 4") == 0)
		{
			waktu = 11;
		}
		else if (String.Compare (rondeDipilih, "Ronde 5") == 0) 
		{
			waktu = 8;
		}

		return waktu;
	}


	private void setJbronde(string rondeDipilih)
	{

		if (String.Compare (rondeDipilih, "Ronde 1") == 0) 
		{
			PlayerPrefs.SetInt ("Jbronde",0);
			Debug.Log ("awaljb1 - "+PlayerPrefs.GetInt("Jbronde"));
		} 
		else if (String.Compare (rondeDipilih, "Ronde 2") == 0)
		{
			PlayerPrefs.SetInt ("Jbronde",10);
			Debug.Log ("awaljb2 - "+PlayerPrefs.GetInt("Jbronde"));
		}
		else if (String.Compare (rondeDipilih, "Ronde 3") == 0) 
		{
			PlayerPrefs.SetInt ("Jbronde",20);
			Debug.Log ("awaljb3 - "+PlayerPrefs.GetInt("Jbronde"));
		}
		else if (String.Compare (rondeDipilih, "Ronde 4") == 0)
		{
			PlayerPrefs.SetInt ("Jbronde",30);
			Debug.Log ("awaljb4 - "+PlayerPrefs.GetInt("Jbronde"));
		}
		else if (String.Compare (rondeDipilih, "Ronde 5") == 0) 
		{
			PlayerPrefs.SetInt ("Jbronde",40);
			Debug.Log ("awaljb5 - "+PlayerPrefs.GetInt("Jbronde"));
		}
	}


	public void TampilPertanyaanSelanjutnya()
	{
		//Debug.Log(sisawaktu);
		//pertanyaan = true;
		sisawaktu = (float)ronde(rondeDipilih);
		//Debug.Log(sisawaktu);
		//pertanyaan = true;
		UpdateTimeRemainingDisplay (sisawaktu);
		questionIndex = UnityEngine.Random.Range (0, dataKuis.DataSoal.Count);


		jwb1.gameObject.GetComponent<Image> ().color = Color.yellow;
		jwb2.gameObject.GetComponent<Image> ().color = Color.yellow;
		jwb3.gameObject.GetComponent<Image> ().color = Color.yellow;
		jwb4.gameObject.GetComponent<Image> ().color = Color.yellow;
		jwb1.enabled = true;
		jwb2.enabled = true;
		jwb3.enabled = true;
		jwb4.enabled = true;
		if (count < 10) 
		{
			TampilPertanyaan (questionIndex);
			count++;
			Debug.Log (count);
		}
		else 
		{
			int jbronde = PlayerPrefs.GetInt ("Jbronde");
			PlayerPrefs.SetInt ("jb",c+jbronde);
			EndRound ();
			aktifRonde();
		} 
		sisawaktu = (float)ronde(rondeDipilih);
		Debug.Log ("diTampilPertanyaansel :"+sisawaktu);
	}

	public void aktifRonde()
	{
		int count = PlayerPrefs.GetInt ("jb");
		Debug.Log ("JB" + count);
		if(count == 10)
		{
			PlayerPrefs.SetInt ("r2",1);
			Debug.Log ("Ronde 2 aktif habis ronde 1");
		}

		if(count >= 20 && count<30)
		{
			PlayerPrefs.SetInt ("r3",1);
			Debug.Log ("Ronde 3 aktif habis ronde 2");
		}

		if(count >= 30 && count<40)
		{
			PlayerPrefs.SetInt ("r4",1);
			Debug.Log ("Ronde 4 aktif habis ronde 3");
		}

		if(count >= 40 )
		{
			PlayerPrefs.SetInt ("r5",1);
			Debug.Log ("Ronde 5 aktif habis ronde 4");
		}
	}

	public void AnswerButtonClicked()
	{
		jawabanDipilih = EventSystem.current.currentSelectedGameObject.name;
		jwb1.enabled = false;
		jwb2.enabled = false;
		jwb3.enabled = false;
		jwb4.enabled = false;
		pertanyaan = false;

		if (String.Compare (jawabanDipilih, "btnJawaban"+dataSoal.pilihanBenar) == 0) 
		{
			c++;
			suarajawabanBenar ();
			nilaiPemain += nilai;

			PlayerPrefs.SetInt ("jb",c);
			Debug.Log ("Jb benar klik: "+PlayerPrefs.GetInt ("jb"));
	
			EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().color = Color.green;
		} 
		else
		{
			suarajawabanSalah ();
			EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().color = Color.red;
			benar = GameObject.Find("btnJawaban" + dataSoal.pilihanBenar);
			benar.gameObject.GetComponent<Image> ().color = Color.green;
		}
		scoreDisplayTeks.text = "Nilai: " + nilaiPemain.ToString ();
	}

	public void EndRound()
	{
		
		if(c < 10)
		{
			PlayerPrefs.SetInt ("jb",0);
			c = 0;
		}
		enabled = false;

		string email = PlayerPrefs.GetString ("email");
		if(email!="")
		{
			addscore (email,nilaiPemain);
		}
		int getlastscore = PlayerPrefs.GetInt("score");

		totalscore = getlastscore + nilaiPemain;
		PlayerPrefs.SetInt ("score",totalscore);
		skor.text = totalscore.ToString ();

		sisawaktu = (float) 0 ;
		UpdateTimeRemainingDisplay (sisawaktu);
		pertanyaan = false;
		isRoundActive = false;
		questionDisplay.SetActive (false);
		roundEndDisplay.SetActive (true);
	}

	public void suarajawabanBenar()
	{
		StartCoroutine (ChangersuaraJawaban (SuarajwbBener));
	}

	public void suarajawabanSalah()
	{
		StartCoroutine (ChangersuaraJawaban (SuarajwbSalah));
	}
	public IEnumerator ChangersuaraJawaban(AudioSource audioSource)
	{
		audioSource.PlayOneShot (audioSource.clip);
		while(audioSource.isPlaying)
		{
			yield return new WaitForSeconds (audioSource.clip.length);
		}
	}

	public void showGambar(string url)
	{
		StartCoroutine (loadGambarUrl(url)); 
	}



	public void addscore(string email,int score)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("email",email);
		form.AddField ("score",score);
		WWW www = new WWW (CreateUserUrl,form);
	}

	public void addtopPertanyaan(int id_pertanyaan)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("id_pertanyaan",id_pertanyaan);
		WWW www = new WWW (CreatePertanyaanUrl,form);
	}

	//load gambar 



	public IEnumerator loadGambarUrl(string urlz)
	{
		string url = urlz;

		if (Application.internetReachability == NetworkReachability.NotReachable)
			yield return null;

		WWW www = new WWW (url);
		Debug.Log ("Proses Download");
		yield return www;

		if (string.IsNullOrEmpty (www.text))
		{
			Debug.Log ("Download Gagal");
			setDisable ();

		} 
		else
		{
			Debug.Log ("Download Berhasil");
			Debug.Log (www.text);
			//img.gameObject.GetComponent<RawImage> ().texture = www.texture;
			//img.texture = www.texture;
			GameObject rawImage = GameObject.Find("RawImage");
			rawImage.GetComponent<RawImage> ().texture = www.texture;
			setEnable ();
		}

	}

	public void GetImage(string gmbr)
	{
		Texture2D newPhoto = new Texture2D (1, 1);
		newPhoto.LoadImage(Convert.FromBase64String(gmbr)); 
		newPhoto.Apply ();
		//newUserPhoto = newPhoto;
		
		//var tex = new Texture2D(64,64);
		//byte[] img = (byte[])gmbr;

		//tex.LoadImage(img);
		// image 266x199
		//GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 266, 199), new Vector2(0.5f, 0.5f));
		GameObject rawImage = GameObject.Find("RawImage");
		rawImage.GetComponent<RawImage> ().texture = newPhoto;
		setEnable ();
	}

	public void setDisable()
	{
		pertanyaan = false;
		enabled = false;
		jwb1.enabled = false;
		jwb2.enabled = false;
		jwb3.enabled = false;
		jwb4.enabled = false;
	}

	public void setEnable()
	{
		pertanyaan = true;
		enabled = true;
		jwb1.enabled = true;
		jwb2.enabled = true;
		jwb3.enabled = true;
		jwb4.enabled = true;

	}
}
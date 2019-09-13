using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGambar : MonoBehaviour {

	//private Datakuis dataKuis;
	//private Item dataSoal;
	private int questionIndex;
	public RawImage img;
	private GameController gm;

	private bool run = true;
	//string url = "https://2.bp.blogspot.com/-Fc2aDbCkHb0/WA36iOi6L8I/AAAAAAAADWc/9Wy3qX-_RdwWQZMKOcY5qMlMe5fMyppygCLcB/s1600/gambar%2Brumah%2Bsederhana%2B-%2B4.jpg";

	// Use this for initialization


	void Awake()
	{
		img = this.gameObject.GetComponent<RawImage> ();
	}
	 
	void Start () {

		//dataKuis = FindObjectOfType<Datakuis> ();
		//dataSoal = dataKuis.DataSoal[questionIndex];
		//gm = new GameController ();
		gm = FindObjectOfType<GameController> ();
		//StartCoroutine (loadGambarUrl()); 
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void showGambar(string url)
	{
		StartCoroutine (loadGambarUrl(url)); 


		//return run;
	}




	IEnumerator loadGambarUrl(string urlz)
	{
		string url = urlz;

		if (Application.internetReachability == NetworkReachability.NotReachable)
			yield return null;

		var www = new WWW (url);
		Debug.Log ("Proses Download");
		yield return www;

		if (string.IsNullOrEmpty (www.text))
		{
			Debug.Log ("Download Gagal");
			//run = false;
		} 
		else
		{
			//run = true;
			Debug.Log ("Download Berhasil");
			//gm = new GameController ();
			//gm.run (true);

			img.texture = www.texture;
		
			//DestroyObject(gm);

			//float waktu = (float)10;
			//pertanyaan = true;
			//gm.run (true);

			/*
			Texture2D texture = new Texture2D(1,1);
			www.LoadImageIntoTexture (texture);
			Sprite sprite = Sprite.Create (texture,
				new Rect(0,0,texture.width/2,texture.height),
				Vector2.one/2);

			GetComponent<SpriteRenderer> ().sprite = sprite;*/
		}

	}

}

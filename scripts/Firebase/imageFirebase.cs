using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Storage;
using Firebase.Unity.Editor;
using System.Threading.Tasks;
using System.Threading;

public class imageFirebase : MonoBehaviour {

	// Get a reference to the storage service, using the default Firebase App
	FirebaseStorage storage = FirebaseStorage.DefaultInstance;

	// Use this for initialization
	void Start ()
	{
		StorageReference imgRef = 
			FirebaseStorage.DefaultInstance.GetReferenceFromUrl ("gs://gabudnus.appspot.com/gambar/wacana.co_Rumah-Adat-Joglo-1170x780.jpg");
		imgRef.GetDownloadUrlAsync ().ContinueWith ((Task<Uri> task) => {
			if(!task.IsFaulted && !task.IsCanceled)
			{
				string url = task.Result.ToString();
				Debug.Log(url);
				StartCoroutine(LoadURL(url));
			}	
		});
	}

	IEnumerator LoadURL(string url)
	{
		WWW www = new WWW (url);
		yield return www;
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = www.texture;
		renderer.material.mainTexture.filterMode = FilterMode.Point;
	}
}

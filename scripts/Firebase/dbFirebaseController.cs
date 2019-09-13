using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class dbFirebaseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Set this before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://gabudnus.firebaseio.com/");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

		string email = PlayerPrefs.GetString ("email");
		string date = System.DateTime.Now.ToLongDateString ();
		string time = System.DateTime.Now.ToLongTimeString ();
		int skor = PlayerPrefs.GetInt ("skor");
		int order = -1 * skor;

		userFirebase user = new userFirebase (email, date, time, skor, order);
		string json = JsonUtility.ToJson (user);
		reference.Child ("User").Push ().SetRawJsonValueAsync (json);
	}
}

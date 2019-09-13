using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class daftarakun : MonoBehaviour,IPointerClickHandler 
{
	private FirebaseAuth auth;
	public InputField UserNameInput, PasswordInput;
	public Button SignupButton, LoginButton;
	public Text ErrorText;
	public GameObject panelerror;
	public Text link2;
	TextMeshProUGUI LinkT;

	string CreateUserUrl = "https://gabudnus.000webhostapp.com/adduser.php";//"localhost:88/gabudnus/adduser.php";

	void Start()
	{
		auth = FirebaseAuth.DefaultInstance;
		LinkT = GetComponent<TextMeshProUGUI> ();
		//Just an example to save typing in the login form
		SignupButton.onClick.AddListener(() => Signup(UserNameInput.text, PasswordInput.text));
		LoginButton.onClick.AddListener(() => Login(UserNameInput.text, PasswordInput.text));

	}

	public void Signup(string email, string password)
	{
		if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
		{
			//Error handling
			return;
		}


		auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
			{
				if (task.IsCanceled)
				{
					Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
					return;
				}
				if (task.IsFaulted)
				{
					Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
					if (task.Exception.InnerExceptions.Count > 0)
						UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
					return;
				}

				FirebaseUser newUser = task.Result; // Firebase user has been created.

				newUser.SendEmailVerificationAsync().ContinueWith(t=>
					{
						UpdateErrorMessage("Mengirim Email Verifikasi");
					});
				Debug.LogFormat("Firebase user created successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);
				createuser(email, password);
				UpdateErrorMessage("Pendaftaran Berhasil");
			});
	}

	private void UpdateErrorMessage(string message)
	{
		panelerror.SetActive (true);
		ErrorText.text = message;
		Invoke("ClearErrorMessage", 3);
	}

	void ClearErrorMessage()
	{
		ErrorText.text = "";
		panelerror.SetActive (false);
	}


		
	public void Login(string email, string password)
	{
		auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
			{
				if (task.IsCanceled)
				{
					Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
					return;
				}
				if (task.IsFaulted)
				{
					Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
					if (task.Exception.InnerExceptions.Count > 0)
						UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
					return;
				}

				FirebaseUser user = task.Result;

				if(!user.IsEmailVerified){
					UpdateErrorMessage( email + " ini belum terverifikasi!");
					auth.SignOut();
					return;
				}
				else
				{
					Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
					PlayerPrefs.SetString("email",email);
					PlayerPrefs.SetInt ("score",0);
				}


				SceneManager.LoadScene("Menu");
			});
	}

	public void backtomenu()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void OnPointerClick(PointerEventData data)
	{
		if (LinkT.text == "<link>Daftar</link>") {
			LinkT.text = "<link>Masuk</link>".ToString();
			link2.text = "Sudah punya akun?";
			LoginButton.gameObject.SetActive (false);
			SignupButton.gameObject.SetActive (true);
		} 
		else
		{
			LinkT.text = "<link>Daftar</link>".ToString();
			link2.text = "Belum punya akun?";
			LoginButton.gameObject.SetActive (true);
			SignupButton.gameObject.SetActive (false);
		}
	}

	//db hosting

	public void createuser(string email, string pass)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("email",email);
		form.AddField ("password",pass);
		form.AddField ("idrole", 2);															
		WWW www = new WWW (CreateUserUrl,form);
		//PlayerPrefs.SetString ("email",email);
	}
}

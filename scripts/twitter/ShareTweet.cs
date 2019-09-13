using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareTweet : MonoBehaviour {

	string TWITEER_ADDRESS = "http://twitter.com/intent/tweet";
	string TWEET_LANGUAGE = "en";
	string teksTampil = "Hei teman-teman, Gabung di game GABUDNUS dan cek nilai saya:";

	string Caption = "Cek nilai terbaru saya: ";
	string Description = "Bergabung dengan game ini, dipastikan kamu tidak akan bosan..";


	public void shareOnTwiteer()
	{
		Application.OpenURL (TWITEER_ADDRESS + "?text=" + WWW.EscapeURL (teksTampil) + GameController.totalscore
		+ "&amp;lang=" + WWW.EscapeURL (TWEET_LANGUAGE));
	}
}

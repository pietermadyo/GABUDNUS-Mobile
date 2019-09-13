using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userFirebase {

	public string email;
	public string date;
	public string time;
	public int score;
	public int order;

	public userFirebase() {
	}

	public userFirebase(string email, string date, string time, int score, int order) {
		this.email = email;
		this.date = date;
		this.time = time;
		this.score = score;
		this.order = order;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
	public Sprite[] frames;
	public Image explosion;
	public float frameRate = 0.1f;

	private int currentImage;

	// Use this for initialization
	void Start ()
	{
		currentImage = 0;
		//Debug.Log ("curImg:"+ currentImage);
		InvokeRepeating ("ChangeImage",0.1f, frameRate);
	}

	public void ChangeImage()
	{
		if(currentImage == frames.Length-1)
		{
			currentImage = 0;
		}
		currentImage += 1;
		//Debug.Log ("curImg:"+ currentImage);
		explosion.sprite = frames[currentImage];
	}
}

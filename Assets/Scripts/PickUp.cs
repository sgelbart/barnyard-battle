﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour {

	public Text countText;
	public float targetTime = 60.0f;

	public static int count;

	void Start ()
	{
		count = 0;
		SetCountText ();
	}

	void Update ()
	{
		SetCountText ();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			ProjectileShooter.carrotCount = count;
			SetCountText ();
		}
		if (other.gameObject.CompareTag ("Super Carrot")) 
		{
			other.gameObject.SetActive (false);
			count = count + 5;
			ProjectileShooter.carrotCount = count;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString () + "/34";
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootingController : MonoBehaviour {

	public GameObject farmer;

	public bool timerActive;
	public float targetTime;

	public AnimationClip mad;
	public AnimationClip walking;
	public AnimationClip die;

	public float startHealth;
	public float currentHealth;
	public double healthPercent;
	public Text healthText;
	public bool isDead;

	public static bool winDelay;
	public float delayTime;

	public void Start ()
	{
		winDelay = false;
		isDead = false;
		healthPercent = 100;
		currentHealth = startHealth;
	}

	public void Update ()
	{
		if (timerActive)
		{
			targetTime -= Time.deltaTime;
		}

		if (targetTime <= 0.0f) 
		{
			timerActive = false;
			if (healthPercent > 0)
			{
				PathFollower.speed = 2.0f;
			}
			targetTime = 2.0f;
		}

		if (winDelay == true)
		{
			delayTime = delayTime - Time.deltaTime;
		}

		if (delayTime <= 0.0f) 
		{
			SceneManager.LoadScene ("character_selection");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("carrot"))
		{
			GetAngry ();
		}
	}

	void GetAngry()
	{
		//takes a health point away
		if (healthPercent > 0)
		{
			currentHealth--;
			healthPercent = Math.Round (currentHealth / startHealth * 100, 0, MidpointRounding.AwayFromZero);
		}

		healthText.text = "Farmer Health: " + healthPercent + "%";

		if (healthPercent <= 0) 
		{
			if (isDead == false)
			{
				PathFollower.speed = 0;
				die.legacy = true;
				GetComponent<Animation> ().Play ("Death2");
			}
			isDead = true;
			winDelay = true;
		}

		//makes him stop moving
		PathFollower.speed = 0.0f;

		if (healthPercent > 0)
		{
			//plays angry anim
			mad.legacy = true;
			GetComponent<Animation> ().Play ("Angry");
		}

		//starts timer
		if ( targetTime > 0.0f)
		{
			timerActive = true;
		}
	}
}
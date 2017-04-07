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
	public AnimationClip running;

	public float startHealth;
	public float currentHealth;
	public double healthPercent;
	public Text healthText;
	public bool isDead;

	public void Start ()
	{
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
				running.legacy = true;
				GetComponent<Animation> ().Play ("run");
				PathFollower.speed = 10.0f;
			}
			targetTime = 2.0f;
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
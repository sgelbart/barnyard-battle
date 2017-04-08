using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour {

	public static float delayTime;


	// Use this for initialization
	void Start () {
		delayTime = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (delayTime > 0.0f) 
		{
			delayTime = delayTime - Time.deltaTime;
		}

		if (delayTime <= 0)
		{
			SceneManager.LoadScene ("character_selection");
		}

	}
}

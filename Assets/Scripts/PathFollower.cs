using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PathFollower : MonoBehaviour {

	//variables up here
	public Transform[] path; 			//holds path
	public static float speed = 2.0f;	//holds speed of obj
	public float reachDist = 1.0f;
	public int currentPoint = 0;		//tells index within path
	public int[] leftTurns;
	public int[] rightTurns;

	public AnimationClip idle;
	public AnimationClip walk;

	public Image loseBackg;
	public Text loseText;
	public static bool sceneDelay;
	public float delayTime;
	//public float currentDelayTime;

	void Start ()
	{
		walk.legacy = true;
		GetComponent <Animation> ().Play ("walk");
		loseText.text = " ";
		loseBackg.enabled = false;
		sceneDelay = false;
	}

	void Update () {
		float dist = Vector3.Distance (path [currentPoint].position, transform.position);

		transform.position = Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime*speed);

		if (dist <= reachDist)
		{
			currentPoint++;
			if (leftTurns.Contains(currentPoint))
			{
				transform.Rotate(0,-90,0);
			}
			if (rightTurns.Contains(currentPoint))
			{
				transform.Rotate (0,90,0);
			}
		}

		if (sceneDelay == true)
		{
			delayTime = delayTime - Time.deltaTime;
		}

		if (delayTime <= 0.0f)
		{
			SceneManager.LoadScene ("character_selection");
		}

		if (sceneDelay == false)
		{
			if (currentPoint == 4) 
			{
				loseBackg.enabled = true;
				loseText.text = "Poor Bessie!";
				sceneDelay = true;
			}
		}

		if (currentPoint >= path.Length)
		{
			currentPoint = 0;
			speed = 0;

			idle.legacy = true;
			GetComponent <Animation> ().Play ("Standby");
		}
	}

	void OnDrawGizmos () {
		for(int i = 0; i < path.Length; i++)
		{
			if (path[i] != null)
			{
				Gizmos.DrawSphere(path[i].position,reachDist);
			}
		}
	}
}

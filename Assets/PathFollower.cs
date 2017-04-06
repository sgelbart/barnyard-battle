using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

	//variables up here
	public Transform[] path; 			//holds path
	public float speed;		 			//holds speed of obj
	public float reachDist = 1.0f;
	public int currentPoint = 0;		//tells index within path

	void Update () {
		float dist = Vector3.Distance (path [currentPoint].position, transform.position);

		transform.position = Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime*speed);

		if (dist <= reachDist)
		{
			currentPoint++;
		}

		if (currentPoint >= path.Length)
		{
			currentPoint = 0;
			speed = 0;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public GameObject player;
	public float thrust = 2.0f;

	void FixedUpdate()
	{
		player.transform.position += this.transform.forward * thrust * Time.deltaTime;
	}

}

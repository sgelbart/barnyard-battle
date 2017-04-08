using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileShooter : MonoBehaviour {

	//just for shooting purposes
	public /*static*/ GameObject player;

	GameObject prefab;

	// Use this for initialization
	void Start () {

		prefab = Resources.Load("projectile") as GameObject;

	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name != "character_selection")
		{
			if (Input.GetMouseButtonDown (0)) {
				Throw ();
			}
		}
		 
	}

	void Throw() {

		GameObject projectile = Instantiate (prefab) as GameObject;
		projectile.transform.position = transform.position + player.transform.forward + player.transform.up;
		Rigidbody rb = projectile.GetComponent<Rigidbody> ();
		rb.velocity = player.transform.forward * 20 + player.transform.up * 5;

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ( "farmer"))
		{
			other.gameObject.SetActive (false);
		}
	}
}

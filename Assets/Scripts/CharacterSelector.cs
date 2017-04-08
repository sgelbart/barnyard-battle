using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour {

	private GameObject[] characterList;
	private int index;

	private void Start()
	{
		PathFollower.speed = 2.0f;
		ShootingController.currentHealth = 10;
		PathFollower.farmerDelay = true;
		PathFollower.farmDelayTime = 5.0f;

		//sets value that chooses the char to the one you picked earlier
		index = PlayerPrefs.GetInt ("CharacterSelected");

		//create char list array
		characterList = new GameObject[transform.childCount];

		//fill array with models
		for (int i = 0; i < transform.childCount; i++)
		{
			characterList[i] = transform.GetChild(i).gameObject;
		}

		//toggle off renderer
		foreach (GameObject go in characterList)
		{
			go.SetActive(false);
		}

		//toggle on selected character
		if(characterList[index])
		{
			characterList[index].SetActive(true);
			//sets player var in camera controller
			CameraController.player = characterList[index];
			//sets player in proj shooter
			//ProjectileShooter.player = characterList[index];
		}

	}

	public void ToggleLeft()
	{
		//toggle off current model 
		characterList[index].SetActive(false);

		//find previous model
		index--;//index - 1
		if(index < 0)
		{
			index = characterList.Length - 1;
		}

		//Show previous model
		characterList[index].SetActive(true);
	
	}

	public void ToggleRight()
	{
		//toggle off current model 
		characterList[index].SetActive(false);

		//find previous model
		index++;//index + 1
		if(index == characterList.Length)
		{
			index = 0;
		}
		//Show previous model
		characterList[index].SetActive(true);
	}

	public void ConfirmButton()
	{
		PlayerPrefs.SetInt ("CharacterSelected", index);
		SceneManager.LoadScene ("main");
	}

}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
//	public Text restartText;
	public Text gameOverText;
	public GameObject restartButton;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start ()
	{
		gameOver = false;
		restart = false;
//		restartText.text = "";
		gameOverText.text = "";
		restartButton.SetActive (false);
		score = 0;
		UpdateScore (); 
		StartCoroutine(SpawnWaves ());
	}

//	void Update () 
//	{
//		if (restart) 
//		{
//			if (Input.GetKeyDown (KeyCode.R)) 
//			{
//				Application.LoadLevel (Application.loadedLevel);
//			}
//		}
//	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++) 
			{
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				//comments: idea for ships coming from the bottom 
				/* bool flag = (Random.value > 0.5f);
				 if (flag) 
				 {
					  spawnValue.z (16 or -16)
				 }
				*/ 
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				//GameObject clone = Instantiate (hazard, spawnPosition, spawnRotation);
				//ReverseDirection(clone);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver) 
			{
//				restartText.text = "Press 'R' for Restart";
				restartButton.SetActive (true);
				restart = true;
				break;
			}
		}
	}

	/*void ReverseDirection ()
	 * {
	 * 		clone.transform.rotation.y = 0;
	 * 		clone.GetComponent<Mover>().speed = 5;
	 * }
	 */
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public void RestartGame() {
		Application.LoadLevel (Application.loadedLevel);
	}
}
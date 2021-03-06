﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {

	public int lives = 3;
	public int bricks= 20;
	public float resetDelay = 1f;
	public Text livesText;

	private Vector3 bricksPos = new Vector3(1.8f,1,0);
	
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject bricksPrefab;
	public GameObject paddle;
	public GameObject ball;
	public GameObject deathParticles;

	public static GM instance = null;
	private GameObject clonePaddle;

	// Use this for initialization
	void Start () {
		if( instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}
		
		Setup();
	}
	
	//Update is called once per frame
	void Update () {
		
	}
	
	public void Setup () {
		clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
		//Instantiate(bricksPrefab, transform.position,Quaternion.identity);
		Instantiate(bricksPrefab, bricksPos ,Quaternion.identity);
	}
	
	public void CheckGameOver()
	{
		if(bricks < 1)
		{
			youWon.SetActive(true);
			Time.timeScale = 0.25f;
			Invoke ("Reset", resetDelay);
		}
		if(lives < 1)
		{
			gameOver.SetActive(true);
			Time.timeScale = 0.25f;
			Invoke ("Reset", resetDelay);
		}
	}
	
	void Reset()
	{
		Time.timeScale = 1f;
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void LoseLife()
	{
		lives--;
		livesText.text = "Lives:" + lives;
		Instantiate(deathParticles, clonePaddle.transform.position, 
		            Quaternion.identity);
		Destroy(clonePaddle);
		Invoke("SetupPaddle", resetDelay);
		CheckGameOver();
	}
	
	void SetupPaddle()
	{
		clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
	}
	
	public void DestroyBrick()
	{
		bricks--;
		CheckGameOver();
	}
}

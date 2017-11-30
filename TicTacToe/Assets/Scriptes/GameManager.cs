using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public Canvas page1;
	public Button btn1P;
	public Button btn2P;

	public GameObject audio1;

	public int nbrOfPlayers = 1;


	// Use this for initialization
	void Start () {
		page1.enabled = true;
		btn1P.onClick.AddListener (OnClick1P);
		btn2P.onClick.AddListener (OnClick2P);
		DontDestroyOnLoad (audio1);
		DontDestroyOnLoad (this.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClick1P()
	{
		nbrOfPlayers = 1;
		SceneManager.LoadScene ("tictactoescene01");
	}

	void OnClick2P()
	{
		nbrOfPlayers = 2;
		SceneManager.LoadScene ("tictactoescene01");
	}
}

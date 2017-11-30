using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardscript : MonoBehaviour {
	const int gridSize = 3;
	int selectPos = 0;
	bool isTurnPlayerOne = true;

	int posHor = 0;
	int posVer = 0;

	int nbrOfPlayers = 0;

	bool[,] select_B = new bool[gridSize,gridSize];
	bool[,] cross_B = new bool[gridSize,gridSize];
	bool[,] circle_B = new bool[gridSize,gridSize];

	GameObject[,] select_D = new GameObject[gridSize, gridSize];
	GameObject[,] cross_D = new GameObject[gridSize, gridSize];
	GameObject[,] circle_D = new GameObject[gridSize, gridSize];

	float timerIA = 0;

	private GameManager gameMana;


	// Use this for initialization
	void Start () {

		gameMana = GameObject.Find ("Main").GetComponent<GameManager>();
		nbrOfPlayers = gameMana.nbrOfPlayers;

		selectPos = 0;

		//clear game array
		for(int i=0; i<gridSize;i++)
		{
			for (int j = 0; j < gridSize; j++) {
				select_B [i, j] = false;
				cross_B [i, j] = false;
				circle_B [i, j] = false;
			}
		}


		// Associate Gameobject to display Arrays

		select_D[0,0] = GameObject.Find ("select_0");
		select_D[0,1] = GameObject.Find ("select_1");
		select_D[0,2] = GameObject.Find ("select_2");
		select_D[1,0] = GameObject.Find ("select_3");
		select_D[1,1] = GameObject.Find ("select_4");
		select_D[1,2] = GameObject.Find ("select_5");
		select_D[2,0] = GameObject.Find ("select_6");
		select_D[2,1] = GameObject.Find ("select_7");
		select_D[2,2] = GameObject.Find ("select_8");

		cross_D[0,0] = GameObject.Find ("crosses_0");
		cross_D[0,1] = GameObject.Find ("crosses_1");
		cross_D[0,2] = GameObject.Find ("crosses_2");
		cross_D[1,0] = GameObject.Find ("crosses_3");
		cross_D[1,1] = GameObject.Find ("crosses_4");
		cross_D[1,2] = GameObject.Find ("crosses_5");
		cross_D[2,0] = GameObject.Find ("crosses_6");
		cross_D[2,1] = GameObject.Find ("crosses_7");
		cross_D[2,2] = GameObject.Find ("crosses_8");

		circle_D[0,0] = GameObject.Find ("circles_0");
		circle_D[0,1] = GameObject.Find ("circles_1");
		circle_D[0,2] = GameObject.Find ("circles_2");
		circle_D[1,0] = GameObject.Find ("circles_3");
		circle_D[1,1] = GameObject.Find ("circles_4");
		circle_D[1,2] = GameObject.Find ("circles_5");
		circle_D[2,0] = GameObject.Find ("circles_6");
		circle_D[2,1] = GameObject.Find ("circles_7");
		circle_D[2,2] = GameObject.Find ("circles_8");


		// Disable all display components

		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				select_D[i,j].GetComponent<SpriteRenderer> ().enabled = false;
			}
		}

		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				cross_D[i,j].GetComponent<SpriteRenderer> ().enabled = false;
			}
		}

		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				circle_D[i,j].GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}

	
	// Update is called once per frame
	void Update () {

		if (nbrOfPlayers == 1) {
			if (isTurnPlayerOne) {
				ShowSelection ();
				CheckMoves ();
				ActionPress1P ();
				ActualiseView ();
				DetectWin ();

			} else {
				// Computer Plays
				timerIA += Time.deltaTime;
				if (timerIA > 0.5f) {
					print ("Start to play???");

					IA_IGOR ();
					isTurnPlayerOne = true;
					ShowSelection ();
					timerIA = 0;
				}

			}
		}

		if (nbrOfPlayers == 2) {
			CheckMoves ();
			ActionPress ();
			ActualiseView ();
			DetectWin ();
		}


	}

	void ActionPress1P(){
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {
			
			if (cross_B [posVer, posHor] || circle_B [posVer, posHor]) {
				print ("Field is already taken!");
			} else {
				cross_B [posVer, posHor] = true;
				HideSelection ();
				isTurnPlayerOne = false;
			}
		}
	}



	void ActionPress(){
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {
			print ("Lol");

			if (cross_B [posVer, posHor] || circle_B [posVer, posHor]) {
				print ("Field is already taken!");
			} else {
				if (isTurnPlayerOne) {
					cross_B [posVer, posHor] = true;
					isTurnPlayerOne = false;
				} else {
					circle_B [posVer, posHor] = true;
					isTurnPlayerOne = true;
				}
			}
		}
	}

	void ActualiseView(){

		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				if (cross_B [i, j]) {
					cross_D [i, j].GetComponent<SpriteRenderer> ().enabled = true;
				} else {
					cross_D [i, j].GetComponent<SpriteRenderer> ().enabled = false;
				}

				if (circle_B [i, j]) {
					circle_D [i, j].GetComponent<SpriteRenderer> ().enabled = true;
				} else {
					circle_D [i, j].GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		}




	}


	int[,] AddGridElements(int[,] grid1, int[,] grid2)
	{
		int[,] grid3 = new int[gridSize, gridSize];

		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				grid3[i, j] = grid1[i, j] + grid2[i, j];
			}
		}

		return grid3;
	}

	void CheckMoves(){
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			switch (posHor) {
			case 0:
				posHor = 0;
				break;
			case 1:
				posHor = 0;
				break;
			case 2:
				posHor = 1;
				break;
			default:
				posHor = 0;
				break;				
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			switch (posHor) {
			case 0:
				posHor = 1;
				break;
			case 1:
				posHor = 2;
				break;
			case 2:
				posHor = 2;
				break;
			default:
				posHor = 0;
				break;				
			}
		}


		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			switch (posVer) {
			case 0:
				posVer = 1;
				break;
			case 1:
				posVer = 2;
				break;
			case 2:
				posVer = 2;
				break;
			default:
				posVer = 0;
				break;				
			}
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			switch (posVer) {
			case 0:
				posVer = 0;
				break;
			case 1:
				posVer = 0;
				break;
			case 2:
				posVer = 1;
				break;
			default:
				posVer = 0;
				break;				
			}
		}


		HideSelection ();

		ShowSelection ();

	}

	void ClearGrid(GameObject[,] element)
	{
		
	}

	int[,] ConvertGrid (bool[,] grid_Bool)
	{
		int[,] grid_int = new int[gridSize, gridSize];

		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				if (grid_Bool [i, j]) {
					grid_int[i, j] = 1;
				} else {
					grid_int[i, j] = 0;
				}
			}
		}

		return grid_int;
	}

	void DetectWin(){

		bool isCrossWin = false;

		if (cross_B[0,0] && cross_B[0,1] && cross_B[0,2]) {
			isCrossWin = true;
		}
		else if(cross_B[1,0] && cross_B[1,1] && cross_B[1,2]) {
			isCrossWin = true;
		}
		else if(cross_B[2,0] && cross_B[2,1] && cross_B[2,2]) {
			isCrossWin = true;
		}
		else if(cross_B[0,0] && cross_B[1,0] && cross_B[2,0]) {
			isCrossWin = true;
		}
		else if(cross_B[0,1] && cross_B[1,1] && cross_B[2,1]) {
			isCrossWin = true;
		}
		else if(cross_B[0,2] && cross_B[1,2] && cross_B[2,2]) {
			isCrossWin = true;
		}
		else if(cross_B[0,0] && cross_B[1,1] && cross_B[2,2]) {
			isCrossWin = true;
		}
		else if(cross_B[0,2] && cross_B[1,1] && cross_B[2,0]) {
			isCrossWin = true;
		}


		bool isCircleWin = false;

		if (circle_B[0,0] && circle_B[0,1] && circle_B[0,2]) {
			isCircleWin = true;
		}
		else if(circle_B[1,0] && circle_B[1,1] && circle_B[1,2]) {
			isCircleWin = true;
		}
		else if(circle_B[2,0] && circle_B[2,1] && circle_B[2,2]) {
			isCircleWin = true;
		}
		else if(circle_B[0,0] && circle_B[1,0] && circle_B[2,0]) {
			isCircleWin = true;
		}
		else if(circle_B[0,1] && circle_B[1,1] && circle_B[2,1]) {
			isCircleWin = true;
		}
		else if(circle_B[0,2] && circle_B[1,2] && circle_B[2,2]) {
			isCircleWin = true;
		}
		else if(circle_B[0,0] && circle_B[1,1] && circle_B[2,2]) {
			isCircleWin = true;
		}
		else if(circle_B[0,2] && circle_B[1,1] && circle_B[2,0]) {
			isCircleWin = true;
		}


		if (isCrossWin) {
			print ("Cross ez win!");
		}
		if (isCircleWin) {
			print ("Circle is cool and wins against noob!");
		}
	}

	void HideSelection()
	{
		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				select_D[i,j].GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}

	void ShowSelection()
	{
		select_D[posVer,posHor].GetComponent<SpriteRenderer> ().enabled = true;
	}

	void IA_IGOR()
	{
		int tempHor, tempVer;
		tempHor = 0;
		tempVer = 0;
		bool didIAFound = false;


		int[,] cross_int = new int[gridSize, gridSize];
		cross_int = ConvertGrid (cross_B);

		int[,] circle_int = new int[gridSize, gridSize];
		circle_int = ConvertGrid (circle_B);

		int[,] mark_int = new int[gridSize, gridSize];
		mark_int = AddGridElements(cross_int,circle_int);

		FindGape (ref mark_int, ref circle_int, ref tempHor, ref tempVer, ref didIAFound);

		if (didIAFound) {
			circle_B [tempVer, tempHor] = true;
		} else {
			FindGape (ref mark_int, ref cross_int, ref tempHor, ref tempVer, ref didIAFound);
			if (didIAFound) {
				circle_B [tempVer, tempHor] = true;
			} else {
				int tempOccupied = 0;

				// count occupied fields
				for (int i = 0; i < gridSize; i++) {
					for (int j = 0; j < gridSize; j++) {
						tempOccupied += mark_int [i, j];
					}
				}

				// determine random number for the unoccupied fields
				int tempUnoccupied = gridSize * gridSize - tempOccupied;
				int randCounter = Random.Range (0, tempUnoccupied);

				// place circle at random free place
				for (int i = 0; i < gridSize; i++) {
					for (int j = 0; j < gridSize; j++) {
						if (!didIAFound) {
							if (mark_int [i, j] == 0) {
								if (randCounter <= 0) {
									circle_B [i, j] = true;
									didIAFound = true;
								} else {
									randCounter--;
								}
							}
						}
					}
				}
			}
		}
	}


	void FindGape(ref int[,] mark_i, ref int[,] player_i, ref int tempH, ref int tempV, ref bool IAFound){

		for (int i = 0; i < gridSize; i++) {
			if (mark_i[i,0] + mark_i[i,1] + mark_i[i,2] < 3) {
				if(player_i[i,0] + player_i[i,1] + player_i[i,2] == 2)
				{
					for (int j = 0; j < gridSize; j++) {
						if (player_i[i, j] == 0)
						{
							tempH = j;
							tempV = i;
							IAFound = true;
						}
					}
				}
			}
		}

		for (int j = 0; j < gridSize; j++) {
			if (mark_i[0,j] + mark_i[1,j] + mark_i[2,j] < 3) {
				if(player_i[0,j] + player_i[1,j] + player_i[2,j] == 2)
				{
					for (int i = 0; i < gridSize; i++) {
						if (player_i[i, j] == 0)
						{
							tempH = j;
							tempV = i;
							IAFound = true;
						}
					}
				}
			}
		}

		if (mark_i[0,0] + mark_i[1,1] + mark_i[2,2] < 3) {
			if(player_i[0,0] + player_i[1,1] + player_i[2,2] == 2)
			{
				for (int i = 0; i < gridSize; i++) {
					if (player_i[i,i] == 0)
					{
						tempH = i;
						tempV = i;
						IAFound = true;
					}
				}
			}
		}

		if (mark_i[0,2] + mark_i[1,1] + mark_i[2,0] < 3) {
			if(player_i[0,2] + player_i[1,1] + player_i[2,0] == 2)
			{
				for (int i = 0; i < gridSize; i++) {
					int j = gridSize - i - 1;
					if (player_i[i,j] == 0)
					{
						tempH = j;
						tempV = i;
						IAFound = true;
					}
				}
			}
		}
	}
}
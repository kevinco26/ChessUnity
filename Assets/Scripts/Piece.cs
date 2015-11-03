using UnityEngine;
using System.Collections;



public class Piece : MonoBehaviour {

	int  [] pawns = new int[8];
	
	bool pawnSelected = false;
	bool towerSelected = false;
	int pawnNumber ;

	GameObject pawn;
	GameObject tower;

	
	float tileZ = 0;
	float tileX = 0;

	bool pawnMovedFirstTime = false;

	// Use this for initialization
	void Start () {

		object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
		  foreach (object o in obj)
		  {
		       GameObject g = (GameObject) o;
		   	   if(g.name.IndexOf("Tile")!=-1)
		   	   {
		   	   		g.transform.tag = "tile";			// Me daba como que fastidio set todas a "tile"
		   	   }
		  }
		//MainClass.getBoard(boardAcquired);

	
	// The pawn array will serve as ocurrences. All will have 0. If one has 1 then that means it was selected and moves so that pawn can't move two tiles.
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		 // Casts the ray and get the first game object hit
		Physics.Raycast(ray, out hit);
		Debug.Log("This hit at " + hit.transform.name );

		if(hit.transform.tag == "pawn")	
		{
			// Select the tile that you want to use. Display that in a text, for now in debug	
			towerSelected = false;

			pawnSelected = true;

			pawnNumber = int.Parse(hit.transform.name.Substring(4)); 	// so pawn 0 was selected or pawn 1 or whatever was selected
			pawn = hit.transform.gameObject;
			
			//boardAcquired = MainClass.getBoard();
			//Debug.Log(boardAcquired[0][0]);

			Debug.Log("Select the tile that you would like to move this " + hit.transform.name.Substring(0,4));



			// Now player will click a tile. Check if the tile click is valid for the corresponding character move, in this case, the pawn.
			// CheckifValid(Tile t, Pawn p)	// This will get the current position of pawn P in the array, check if this + 1 or + 2 in the vertical axis is what the tile was clicked. False otherwise
			// If it is true, move and update array.
			// move(Pawn p)?

		}
		else if(hit.transform.tag == "tower")
		{
			// Select the tile that you want to use. Display that in a text, for now in debug	
			pawnSelected = false;
			towerSelected = true;
			tower = hit.transform.gameObject;
			
			//boardAcquired = MainClass.getBoard();
			//Debug.Log(boardAcquired[0][0]);

			Debug.Log("Select the tile that you would like to move this " + hit.transform.name.Substring(0,5));



			// Now player will click a tile. Check if the tile click is valid for the corresponding character move, in this case, the tower.
			// CheckifValid(Tile t, tower p)	// This will get the current position of tower P in the array, check if this + 1 or + 2 in the vertical axis is what the tile was clicked. False otherwise
			// If it is true, move and update array.
			// move(tower p)?
		}
		else if(hit.transform.tag == "tile")
		{

			if(pawnSelected){		
				Debug.Log(pawnNumber);

				Debug.Log("Current selection of current pawn in array is:" + pawns[pawnNumber]);
				pawns[pawnNumber]++;

				if(pawns[pawnNumber] > 1)
					pawnMovedFirstTime = true;


				tileZ = hit.transform.position.z;
				tileX = hit.transform.position.x;
				// Handles: pawn can only move one up in the x direction (based on our unity setup). Can move 2 tiles up on the first try (will handle that later). Can't move back. And can't move to the sides.

				if((tileZ - pawn.transform.position.z <=1 || (tileZ - pawn.transform.position.z <=2 && !pawnMovedFirstTime)) && tileX == pawn.transform.position.x && tileZ >= pawn.transform.position.z)
				{									// If I selected a pawn, check to see if the selected tile is valid for pawn.
					pawn.transform.position = new Vector3(pawn.transform.position.x,pawn.transform.position.y,tileZ);
					pawnSelected = false;
					pawnMovedFirstTime = true;
				}
				else
				{
					Debug.Log("Sorry, not a valid move for Pawn");
					pawnSelected = false;
				}

				pawnMovedFirstTime = false;
			}
			else if(towerSelected)
			{
				tileZ = hit.transform.position.z;
				tileX = hit.transform.position.x;

				// Handles tower movement (only in x or z direction)
				if(tower.transform.position.x == tileX || tower.transform.position.z == tileZ)
				{									// If I selected a pawn, check to see if the selected tile is valid for pawn.
					tower.transform.position = new Vector3(tileX,tower.transform.position.y,tileZ);
					towerSelected = false;
				
				}
				else
				{
					Debug.Log("Sorry, not a valid move for Tower");
					towerSelected = false;
				}
			}
			else
			{
				Debug.Log("Please select a Chess piece first");		// Display a text to the user/player, not a debug log
			}
		}
	}
	
	}
}

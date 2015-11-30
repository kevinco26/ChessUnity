using UnityEngine;
using System.Collections;



public class Piece : MonoBehaviour {

	int  [] pawns = new int[8];

	

	
	bool pawnSelected = false;
	bool towerSelected = false;
	bool bishopSelected = false;

	int pawnNumber;

	GameObject pawn;
	GameObject tower;
	GameObject bishop;

	
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

	
	// The pawn array will serve as ocurrences. All will have 0. If one has 1 then that means it was selected and moves so that pawn can't move two tiles.
	}
	///*
/*
	object [] findObjectsInterferingPawn()
	{
		object[] objects;



		return objects;
	}
	*/

	// FOR ENEMIES, Check if i can find a gameobject in a given coordinate.


	// WORKS for tower and pawn. It handles vertical and horizontal movement.(Add it to the tower piece when implemented)
	// Will create another method for diagonal as well for bishop and queen. Horse will be the only special piece
	bool checkGameObjectPath(float tilePositionZ, float pawnPositionZ, float pawnPositionX)
	{
		object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));	

		foreach(object o in obj)
		{
		    GameObject g = (GameObject) o;

		  	//Its the same object dont include it
			
			  
			   if(g.transform.tag != "tile" && g.transform.tag !="MainCamera")		// If its not a tile then it is a game object, check to see if its on the way
			   {
			   		if(!(pawnPositionX == g.transform.position.z && g.transform.position.z == pawnPositionZ))
			   	   	{
			   	   		
			   	   		if(pawnPositionX == g.transform.position.x)		// If the piece is on the same x axis (same column per say)
			   	   		{												// Then check if its in between the tile selected and the pawn's z position.
			   	   			if(g.transform.position.z <= tilePositionZ && g.transform.position.z>pawnPositionZ)
			   	   			{
			   	   				//Debug.Log("Epale");
			   	   				return true;
			   	   			}
			   	   		}
			   	   	}
			   }
			

		}
		//console.log("Epale");
		return false;		// False --> good to go, no game object (piece) in the path of the desired move
							// True --> There is a game object in the pawn's path, handle it when moving the pawn.
	}
	//*/
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
			bishopSelected = false;

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
			bishopSelected = false;

			tower = hit.transform.gameObject;
		

			Debug.Log("Select the tile that you would like to move this " + hit.transform.name.Substring(0,5));


		}

		if(hit.transform.tag == "bishop")	
		{
			// Select the tile that you want to use. Display that in a text, for now in debug	
			towerSelected = false;
			pawnSelected = false;
			bishopSelected = true;

			bishop = hit.transform.gameObject;
			
		
			Debug.Log("Select the tile that you would like to move this " + hit.transform.name.Substring(0,6));



			// Now player will click a tile. Check if the tile click is valid for the corresponding character move, in this case, the pawn.
			// CheckifValid(Tile t, Pawn p)	// This will get the current position of pawn P in the array, check if this + 1 or + 2 in the vertical axis is what the tile was clicked. False otherwise
			// If it is true, move and update array.
			// move(Pawn p)?

		}
		else if(hit.transform.tag == "tile")
		{

			if(pawnSelected){		
				//Debug.Log(pawnNumber);

				//Debug.Log("Current selection of current pawn in array is:" + pawns[pawnNumber]);
				pawns[pawnNumber]++;

				if(pawns[pawnNumber] > 1)
					pawnMovedFirstTime = true;


				tileZ = hit.transform.position.z;
				tileX = hit.transform.position.x;
				// Handles: pawn can only move one up in the x direction (based on our unity setup). Can move 2 tiles up on the first try (will handle that later). Can't move back. And can't move to the sides.

				
				// Now check if there was another game object in the pawn's way (another piece in front of the pawn)
				if((tileZ - pawn.transform.position.z <=1 || (tileZ - pawn.transform.position.z <=2 && !pawnMovedFirstTime)) && tileX == pawn.transform.position.x && tileZ >= pawn.transform.position.z && !checkGameObjectPath(tileZ,pawn.transform.position.z,pawn.transform.position.x))
				{									// If I selected a pawn, check to see if the selected tile is valid for pawn.
					
					
					pawn.transform.position = new Vector3(pawn.transform.position.x,pawn.transform.position.y,tileZ);
					pawnSelected = false;
					pawnMovedFirstTime = true;
				}
				else
				{
					Debug.Log("Sorry, not a valid move for Pawn");
					pawnSelected = false;
					pawns[pawnNumber]--;
				}

				pawnMovedFirstTime = false;
			}
			else if(towerSelected)
			{
				tileZ = hit.transform.position.z;
				tileX = hit.transform.position.x;

				// Handles tower movement (only in x or z direction)
				if((tower.transform.position.x == tileX || tower.transform.position.z == tileZ) && !checkGameObjectPath(tileZ,tower.transform.position.z,tower.transform.position.x))
				{									
					tower.transform.position = new Vector3(tileX,tower.transform.position.y,tileZ);
					towerSelected = false;
				
				}
				else
				{
					Debug.Log("Sorry, not a valid move for Tower");
					towerSelected = false;
				}
			}
			else if(bishopSelected)
			{
				tileZ = hit.transform.position.z;
				tileX = hit.transform.position.x;

				// Handles tower movement (only in x or z direction)
				if(tileX != bishop.transform.position.x && tileZ != bishop.transform.position.z)
				{									
					bishop.transform.position = new Vector3(tileX,bishop.transform.position.y,tileZ);
					bishopSelected = false;
				
				}
				else
				{
					Debug.Log("Sorry, not a valid move for Bishop");
					bishopSelected = false;
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

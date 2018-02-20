using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour {

    public AudioSource myAudioSource;
    public bool doorOpenPlayed;

    public SpriteRenderer mySpriteRenderer;

    TextMesh myTextMesh;
    
    public Transform square;
    public Transform instructionSquare;
    public GameObject smallDoor;
    public smallDoorController smallDoorScript;
    public mapController mapScript;
    public AudioClip[] musicBits;
    public AudioClip doorOpen;

    public int numberOfSmallDoors;

    public bool[] squarePlayed;
    int[] correctSquare;
    public bool[] correctSquarePlayed;
    public int nextSquareInCorrectOrder;
    public int currentCorrectSquareIndex;

    public bool levelCompleted;
    public int currentLevel;
    public int[] mapPosition;
    public string myDirection;
    public bool readyForLevelChange;
    public bool goodbyeSquares;
    public bool timeForLevelChange;
    public int squaresDestroyed;
    public float timeToDoorChange;


    //HELLO HI IF YOU WANT TO EDIT THE MELODY/SOLUTION ONLY EDIT THE ARRAYS WITH LEVEL[number]
    //IN FRONT OF THEM THANK YOU
    //this is because we set all other arrays based on this one so things could get confusing otherwise
    public int[] level1SquareOrder;
    public AudioClip level1Solution;

    public int[] level2SquareOrder;
    public AudioClip level2Solution;

    public int[] level3SquareOrder;
    public AudioClip level3Solution;

    public int[] level4SquareOrder;
    public AudioClip level4Solution;

    public int[] level5SquareOrder;
    public AudioClip level5Solution;

    public int[] level6SquareOrder;
    public AudioClip level6Solution;

    public int[] level7SquareOrder;
    public AudioClip level7Solution;

    public int[] level8SquareOrder;
    public AudioClip level8Solution;

    public int[] level9SquareOrder;
    public AudioClip level9Solution;

    public int[] level10SquareOrder;
    public AudioClip level10Solution;

    public int[] level11SquareOrder;
    public AudioClip level11Solution;

    public int[] level12SquareOrder;
    public AudioClip level12Solution;

    public int[] level13SquareOrder;
    public AudioClip level13Solution;

    public int[] level14SquareOrder;
    public AudioClip level14Solution;

    public int[] level15SquareOrder;
    public AudioClip level15Solution;

    public int[] level16SquareOrder;
    public AudioClip level16Solution;

    public int[] level17SquareOrder;
    public AudioClip level17Solution;

    public float[] squareX;
    public float[] squareY;

    public float[] Level1SquareXPosition;
    public float[] Level1SquareYPosition;

    public float[] Level2SquareXPosition;
    public float[] Level2SquareYPosition;

    public float[] Level3SquareXPosition;
    public float[] Level3SquareYPosition;

    public float[] Level4SquareXPosition;
    public float[] Level4SquareYPosition;

    public float[] Level5SquareXPosition;
    public float[] Level5SquareYPosition;

    public float[] squareXPositionSmall;
    public float[] squareYPositionSmall;

    public float[] squareXPositionMedium;
    public float[] squareYPositionMedium;

    public float[] squareXPositionBig;
    public float[] squareYPositionBig;


    public int testMapX;
    public int testMapY;

	// Use this for initialization
    void Start () {
        //We start on the first level,
        currentLevel = 1;
        //Which is at this position on the map array
        mapPosition[0] = 0; mapPosition[1] = 9;

        doorOpen = musicBits[musicBits.Length - 1];
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = doorOpen;
        doorOpenPlayed = false;

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        myTextMesh = GetComponentInChildren<TextMesh>();

        smallDoorScript = smallDoor.GetComponent<smallDoorController>();

        mapScript = GameObject.Find("map").GetComponent<mapController>();

        newLevel();
	}
	
	// Update is called once per frame
	void Update () {
        /* use this to check positions on the map array
        if (Input.GetKeyDown(KeyCode.N)) {
            testMapX--;
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            testMapX++;
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            testMapY--;

        }
        if (Input.GetKeyDown(KeyCode.J)) {
            testMapY++;
        } 
        Debug.Log(testMapX + ", " + testMapY);
        Debug.Log(mapScript.mapArray[testMapX, testMapY]);
        */

        //Label us with the current level
        myTextMesh.text = currentLevel + "";

        //ARE WE PLAYING THE SQUARES IN ORDER????

        //Check all the squares to see if they are being played
        for (int x = 0; x < squarePlayed.Length; x++){
            if (squarePlayed[x]){
                //if they are, check to see if it is the next square in the correct order
                //If it is,
                if (x == nextSquareInCorrectOrder)
                {
                    //Record we played that square
                    correctSquarePlayed[currentCorrectSquareIndex] = true;
                    //If there are still more correct squares to go
                    if (currentCorrectSquareIndex < correctSquare.Length - 1)
                    {
                        //The next correct square is one up in the correct square index
                        currentCorrectSquareIndex++;

                        //Record what actual number that square is
                        nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

                    } else  {
                        //Otherwise, we beat the level! congrats!
                        levelCompleted = true;
                    }
                } else {
                    //if it isn't, restart the order
                    for (int n = 0; n < correctSquarePlayed.Length; n++)
                    {
                        correctSquarePlayed[n] = false;
                    }
                    currentCorrectSquareIndex = 0;
                    nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];
                }
            }
        }


        ////////////******End of level stuff******////////////

        //if we've completed the level and haven't played the door success noise
        if (levelCompleted && doorOpenPlayed == false) {
            //Start playing the door success noise after the last square note has ended
            myAudioSource.PlayDelayed(musicBits[correctSquare[correctSquare.Length - 2]].length);
            //Record this (so we don't play the sound every frame
            doorOpenPlayed = true;
        }

        //If we've started playing the success sound
        if (doorOpenPlayed) {
            //Count down until both the square note and the door success noise have ended
            if (timeToDoorChange < myAudioSource.clip.length + musicBits[correctSquare[correctSquare.Length - 2]].length){
                timeToDoorChange += Time.deltaTime;
            } else {
                //When they have, change to the door success color
                mySpriteRenderer.color = new Color(0, 1, 0);
                //And
                readyForLevelChange = true;
            }
        }

        if (timeForLevelChange) {
            if (goodbyeSquares == false){
                goodbyeSquares = true;
            } else {
                if (squaresDestroyed >= squarePlayed.Length && numberOfSmallDoors <= 0) {
                    newLevel();
                }
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //If we collide with the player and we are ready for a level change
        if (coll.gameObject.tag == "Player" && readyForLevelChange){

            findNextLevel();

            //Prepare to change to the next level
            timeForLevelChange = true;
        }
    }

    public void makeSmallDoor(string direction, int[] squareOrder, int numberOfSquares, string label, bool solved) {
        //Make sure our correct square order is set to the correct level
        smallDoorScript.correctSquare = squareOrder;
        //Make sure the array that checks if all squares were pressed in the correct order
        //Is as long as the number of squares there are in the melody
        smallDoorScript.correctSquarePlayed = new bool[squareOrder.Length];
        //Start us off at the first square
        smallDoorScript.currentCorrectSquareIndex = 0;
        smallDoorScript.nextSquareInCorrectOrder = smallDoorScript.correctSquare[currentCorrectSquareIndex];
        //Make each square have a bool to turn on/off if they get played
        //(we can also use this to check how many squares there are currently
        smallDoorScript.squarePlayed = new bool[numberOfSquares];

        //Make sure we know which direction we're facing
        smallDoorScript.myDirection = direction;

        //If we have a label, give us a label
        smallDoorScript.myTextMesh.text = label;

        //Are we open? (used to make doors that are just open all the time
        smallDoorScript.readyForLevelChange = solved;

        //add one to the number of small doors
        numberOfSmallDoors++;

        //Make a door
        Instantiate(smallDoor, new Vector3(8f, 1f, 0f), Quaternion.identity);
    }

    public void findNextLevel() {
        //For the third time, we have changed how we switch levels
        //We now use our direction to find the next level (using the map array in the map object)
        Vector2Int directionVector = mapScript.findNextRoom(myDirection);

        //we then change our current position to the next level
        mapPosition[0] = directionVector.x;
        mapPosition[1] = directionVector.y;

        //And then we set the current level to the corresponding place on the map
        currentLevel = mapScript.mapArray[mapPosition[0], mapPosition[1]];

        Debug.Log(mapPosition[0] + " " + mapPosition[1]);
    }

    public void newLevel()
    //Do this every time we go to a new level
    {

        //We haven't played the level completion sound
        doorOpenPlayed = false;
        //The door needs to wait until all sound stops before becoming enterable 
        timeToDoorChange = 0;
        //We have not completed this new level
        levelCompleted = false;
        //Thus we are not ready for a level change
        readyForLevelChange = false;
        //Thus we should not get rid of the squares
        goodbyeSquares = false;
        squaresDestroyed = 0;
        timeForLevelChange = false;
        //Reset our color
        mySpriteRenderer.color = new Color(1, 0, 0);

        //Grab the square controller cuz we're gonna need to reset the squares
        squareController squareScript = square.GetComponent<squareController>();
        //Grab the instruction square controller cuz we're gonna need to change
        //its sound depending on the level
        instructionSquareController instructionSquareScript = instructionSquare.GetComponent<instructionSquareController>();
        //Grab the player script here for the sake of consistancy
        //(we only need it to reset their position at the start of each level
        youController youScript = GameObject.FindWithTag("Player").GetComponent<youController>();


        //Depending on what level we're on
        if (currentLevel == 0) {
            //If we're at level 0 this is a problem so reset to level 12
            currentLevel = 12;

            Debug.Log("Oops! You're on level 0! Probably should fix that, buddy!");
        }
        if (currentLevel == 1){
            //Make the instruction square play the correct sound for the solution
            instructionSquareScript.mySound = level1Solution;

            //Set the square's position arrays to that of the correct level
            squareX = squareXPositionSmall;
            squareY = squareYPositionSmall;

            correctSquare = level1SquareOrder;
            correctSquarePlayed = new bool[level1SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionSmall.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 2){
            instructionSquareScript.mySound = level2Solution;

            squareX = squareXPositionSmall;
            squareY = squareYPositionSmall;

            correctSquare = level2SquareOrder;
            correctSquarePlayed = new bool[level2SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionSmall.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 3){
            instructionSquareScript.mySound = level3Solution;

            squareX = squareXPositionMedium;
            squareY = squareYPositionMedium;

            correctSquare = level3SquareOrder;
            correctSquarePlayed = new bool[level3SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionMedium.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 4){
            instructionSquareScript.mySound = level4Solution;

            squareX = squareXPositionMedium;
            squareY = squareYPositionMedium;

            correctSquare = level4SquareOrder;
            correctSquarePlayed = new bool[level4SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionMedium.Length];



            myDirection = "RIGHT";
        } else if (currentLevel == 5)
        {
            instructionSquareScript.mySound = level5Solution;

            squareX = squareXPositionMedium;
            squareY = squareYPositionMedium;

            correctSquare = level5SquareOrder;
            correctSquarePlayed = new bool[level5SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionMedium.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 6)
        {
            instructionSquareScript.mySound = level6Solution;

            squareX = squareXPositionMedium;
            squareY = squareYPositionMedium;

            correctSquare = level6SquareOrder;
            correctSquarePlayed = new bool[level6SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionMedium.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 7)
        {
            instructionSquareScript.mySound = level7Solution;

            squareX = squareXPositionMedium;
            squareY = squareYPositionMedium;

            correctSquare = level7SquareOrder;
            correctSquarePlayed = new bool[level7SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionMedium.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 8)
        {
            instructionSquareScript.mySound = level8Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level8SquareOrder;
            correctSquarePlayed = new bool[level8SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 9)
        {
            instructionSquareScript.mySound = level9Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level9SquareOrder;
            correctSquarePlayed = new bool[level9SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 10)
        {
            instructionSquareScript.mySound = level10Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level10SquareOrder;
            correctSquarePlayed = new bool[level10SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 11)
        {
            instructionSquareScript.mySound = level11Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level11SquareOrder;
            correctSquarePlayed = new bool[level11SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 12)
        {
            instructionSquareScript.mySound = level12Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level12SquareOrder;
            correctSquarePlayed = new bool[level12SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];

            makeSmallDoor("UP", level1SquareOrder, squareXPositionBig.Length, "1", false);


            myDirection = "RIGHT";
        } else if (currentLevel == 13)
        {
            instructionSquareScript.mySound = level13Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level13SquareOrder;
            correctSquarePlayed = new bool[level13SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 14)
        {
            instructionSquareScript.mySound = level14Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level14SquareOrder;
            correctSquarePlayed = new bool[level14SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 15)
        {
            instructionSquareScript.mySound = level15Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level15SquareOrder;
            correctSquarePlayed = new bool[level15SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];


            myDirection = "RIGHT";
        } else if (currentLevel == 16)
        {
            instructionSquareScript.mySound = level15Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level16SquareOrder;
            correctSquarePlayed = new bool[level16SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length + 1];


            myDirection = "UP";
        } else if (currentLevel == 17)
        {
            instructionSquareScript.mySound = level15Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level17SquareOrder;
            correctSquarePlayed = new bool[level17SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];
        }


        //Make the squares
        for (int x = 0; x < squareX.Length; x++)
        {
            squareScript.mySound = musicBits[x];
            squareScript.myNumber = x;
            Instantiate(square, new Vector3(squareX[x], squareY[x], 0f), Quaternion.identity);
        }

        Vector3 pos = new Vector3();


        if (currentLevel < 13) {
            //move the player back to the start
            pos = new Vector3(-6f, 1f, -.15f);
        } else if (currentLevel < 15){
            //move the player back to the start
            pos = new Vector3(-7f, 1f, -.15f);
        } else {
            //move the player back to the start
            pos = new Vector3(-6f, 1f, -.15f);
        }

        //The z is -.15 so they're in front of everything
        youScript.transform.position = pos;


        //Make the instruction square
        if (currentLevel < 15) {
            Instantiate(instructionSquare, new Vector3(-5f, 1f, 0f), Quaternion.identity);
        } else {
            Instantiate(instructionSquare, new Vector3(-4f, 1f, 0f), Quaternion.identity);
        }

        //Move us to the correct location
        if (myDirection == "RIGHT") {
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour {
    
    //We assign both of these in the edirtor
    public RenderTexture fakeRoomTexture;
    public RenderTexture nullTexture;
    public Camera mainCamera;
    public Camera fakeRoomCamera;
    public bool readyForCameraSwitch;
    public bool firstLevelLoad;
    public bool playerMoved;

    public int currentBeat;
    public float beatTimer;
    public float ourBPM;

    public Color openDoorColor;
    public Color closedDoorColor;

    public Vector3 playerMoveTo;

    public bool levelChangeable;

    public AudioSource myAudioSource;
    public bool doorOpenPlayed;

    public SpriteRenderer mySpriteRenderer;

    TextMesh myTextMesh;
    public GameObject myOutline;

    public Color[] CurrentColorPalette;
    public Color[] ColorPalette1;
    public Color[] ColorPalette2;
    public Color[] ColorPalette3;
    public Color[] ColorPalette4;
    public Color[] ColorPalette5;
    
    public Transform square;
    public Transform instructionSquare;
    public GameObject smallDoor;
    public smallDoorController smallDoorScript;
    public youController youScript;
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
    public bool[] mainDoorOpened;


    //Menu stuff
    public bool pauseMenu;


    //HELLO HI IF YOU WANT TO EDIT THE MELODY/SOLUTION ONLY EDIT THE ARRAYS WITH LEVEL[number]
    //IN FRONT OF THEM THANK YOU
    //this is because we set all other arrays based on this one so things could get confusing otherwise
    public int[] levelNullSquareOrder;

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

    //Small Door Solutions
    public int[] smallDoor1;
    public int[] smallDoor2;

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
        
        ourBPM = 90f;
        beatTimer = ourBPM / 60f;
        currentBeat = 1;

        fakeRoomCamera.aspect = 1.3333f;

        //We start on the first level,
        currentLevel = 1;
        //Which is at this position on the map array
        mapPosition[0] = 0; mapPosition[1] = 3;

        doorOpen = musicBits[musicBits.Length - 1];
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = doorOpen;
        doorOpenPlayed = false;
        mainDoorOpened = new bool[17];

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        myTextMesh = GetComponentInChildren<TextMesh>();

        smallDoorScript = smallDoor.GetComponent<smallDoorController>();

        mapScript = GameObject.Find("map").GetComponent<mapController>();

        youScript = GameObject.FindWithTag("Player").GetComponent<youController>();

        pauseMenu = false;

        newLevel();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu = !pauseMenu;
        }

        if (pauseMenu)
        {

        }
        else
        {

            if (beatTimer - Time.deltaTime > 0) {
                beatTimer -= Time.deltaTime;
            } else {
                beatTimer = ourBPM / 60f;
                if (currentBeat < 4)
                {
                    currentBeat++;
                    Debug.Log(currentBeat);
                }
                else
                {
                    currentBeat = 1;
                    Debug.Log(currentBeat);
                }
            }


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
            for (int x = 0; x < squarePlayed.Length; x++)
            {
                if (squarePlayed[x])
                {
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

                        }
                        else
                        {
                            //Otherwise, we beat the level! congrats!
                            levelCompleted = true;
                        }
                    }
                    else
                    {
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
            if (levelCompleted && doorOpenPlayed == false)
            {
                //Start playing the door success noise after the last square note has ended
                myAudioSource.PlayDelayed(musicBits[correctSquare[correctSquare.Length - 2]].length);
                //Record this (so we don't play the sound every frame
                doorOpenPlayed = true;

                //Record that we've competed the main door of the level
                mainDoorOpened[currentLevel] = true;
            }

            //If we've started playing the success sound
            if (doorOpenPlayed)
            {
                //Count down until both the square note and the door success noise have ended
                if (timeToDoorChange < myAudioSource.clip.length + musicBits[correctSquare[correctSquare.Length - 2]].length)
                {
                    timeToDoorChange += Time.deltaTime;
                }
                else
                {
                    //If we're done, we are ready to change levels
                    readyForLevelChange = true;
                }
            }

            if (readyForLevelChange)
            {
                //if we're ready to change colors, open the door
                mySpriteRenderer.color = openDoorColor;
            }
            else
            {
                mySpriteRenderer.color = closedDoorColor;
            }

            if (timeForLevelChange)
            {
                if (playerMoved == false)
                {
                    Debug.Log("youMoved" + Time.frameCount);
                    youScript.transform.position = new Vector3(20f, 20f, -0.15f);
                    playerMoved = true;
                }
                else
                {
                    //Move the main camera to the fakeRoom
                    if (firstLevelLoad == false)
                    {

                        firstLevelLoad = true;
                    }
                    else
                    {
                        if (myDirection == "LEFT")
                        {
                            mainCamera.transform.position = new Vector3(13.82f, 1f, -10f);
                        }
                        else if (myDirection == "RIGHT")
                        {
                            mainCamera.transform.position = new Vector3(-12.835f, 1f, -10f);
                        }
                        else if (myDirection == "UP")
                        {
                            mainCamera.transform.position = new Vector3(0.5f, -9f, -10f);
                        }
                        else if (myDirection == "DOWN")
                        {
                            mainCamera.transform.position = new Vector3(0.5f, 11f, -10f);
                        }
                    }
                    //freeze the fake room camera
                    fakeRoomCamera.targetTexture = nullTexture;
                    Debug.Log("FreezeCamera:" + Time.frameCount);
                    if (goodbyeSquares == false)
                    {
                        goodbyeSquares = true;
                    }
                    else
                    {


                        if (squaresDestroyed >= squarePlayed.Length && numberOfSmallDoors <= 0)
                        {

                            newLevel();
                        }
                        else
                        {
                            Debug.Log("oops: " + squaresDestroyed + "/" + squarePlayed.Length + ", " + numberOfSmallDoors);
                        }

                    }
                }
            }

            if (readyForCameraSwitch)
            {
                //Debug.Log("readyForCameraSwitch:" + Time.frameCount);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (pauseMenu)
        {

        }
        else
        {
            //If we collide with the player and we are ready for a level change
            if (coll.gameObject.tag == "Player" && readyForLevelChange)
            {
                findNextLevel();

                //Prepare to change to the next level
                timeForLevelChange = true;

                Debug.Log("Coll:" + Time.frameCount);
            }
        }
    }

    public void makeSmallDoor(string direction, int[] squareOrder, string label, bool solved) {
        //Make sure our correct square order is set to the correct level
        smallDoorScript.correctSquare = squareOrder;
        //Make sure the array that checks if all squares were pressed in the correct order
        //Is as long as the number of squares there are in the melody
        smallDoorScript.correctSquarePlayed = new bool[squareOrder.Length];
        //Start us off at the first square
        smallDoorScript.currentCorrectSquareIndex = 0;
        smallDoorScript.nextSquareInCorrectOrder = smallDoorScript.correctSquare[currentCorrectSquareIndex];

        //Make sure we know which direction we're facing
        smallDoorScript.myDirection = direction;

        //If we have a label, give us a label
        //smallDoorScript.myTextMesh.text = label;

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
        Debug.Log(currentLevel);
    }

    public void setColorPalette(Color[] colorPaletteToSwitchTo) {
        //Set Color Palette
        CurrentColorPalette = colorPaletteToSwitchTo;
        openDoorColor = CurrentColorPalette[2];
        closedDoorColor = CurrentColorPalette[3];
        mainCamera.backgroundColor = CurrentColorPalette[4];
        fakeRoomCamera.backgroundColor = CurrentColorPalette[4];
    }

    public void newLevel()
    //Do this every time we go to a new level
    {

        Debug.Log("newLevel():" + Time.frameCount);

        //Grab the square controller cuz we're gonna need to reset the squares
        squareController squareScript = square.GetComponent<squareController>();
        //Grab the instruction square controller cuz we're gonna need to change
        //its sound depending on the level
        instructionSquareController instructionSquareScript = instructionSquare.GetComponent<instructionSquareController>();


        if (currentLevel == 15)
        {
            //If we're on level 15 we need a slightly different position
            playerMoveTo = new Vector3(5f, 0f, -.15f);
            Debug.Log("15");
        }
        else if (myDirection == "LEFT")
        {
            //If we leave from left, spawn on right
            playerMoveTo = new Vector3(5f, 1f, -.15f);
            Debug.Log("LEFT");
        }
        else if (myDirection == "RIGHT")
        {
            //If we leave from right, come in on left
            playerMoveTo = new Vector3(-4f, 1f, -.15f);
            Debug.Log("RIGHT");
        }
        else if (myDirection == "UP")
        {
            //If we leave from top, spawn on bottom
            playerMoveTo = new Vector3(1f, -1f, -.15f);
            Debug.Log("UP");
        }
        else if (myDirection == "DOWN")
        {
            //If we leave from bottom, spawn on top
            playerMoveTo = new Vector3(1f, 3f, -.15f);
            Debug.Log("DOWN");
        }

        //The z is -.15 so they're in front of everything
        youScript.transform.position = playerMoveTo;


        //Record the spot the player needs to move to so that we can do it after/while the camera moves
        //We do this at the top before we change myDirection to the correct direction for the next level

        /*
        if (currentLevel == 15)
        {
            //If we're on level 15 we need a slightly different position
            playerMoveTo = new Vector3(5f, 0f, -.15f);
            Debug.Log("15");
        }
        else if (myDirection == "LEFT")
        {
            //If we leave from left, spawn on right
            playerMoveTo = new Vector3(5f, 1f, -.15f);
            Debug.Log("LEFT");
        }
        else if (myDirection == "RIGHT")
        {
            //If we leave from right, come in on left
            playerMoveTo = new Vector3(-5f, 1f, -.15f);
            Debug.Log("RIGHT");
        }
        else if (myDirection == "UP")
        {
            //If we leave from top, spawn on bottom
            playerMoveTo = new Vector3(1f, -1f, -.15f);
            Debug.Log("UP");
        }
        else if (myDirection == "DOWN")
        {
            //If we leave from bottom, spawn on top
            playerMoveTo = new Vector3(1f, 3f, -.15f);
            Debug.Log("DOWN");
        }
        */


        //Camera stuff
        /*
        if (firstLevelLoad == false)
        {

            firstLevelLoad = true;
        } else {
            if (myDirection == "LEFT")
            {
                mainCamera.transform.position = new Vector3(16.5f, 1f, -10f);
            }
            else if (myDirection == "RIGHT")
            {
                mainCamera.transform.position = new Vector3(-15.5f, 1f, -10f);
            }
            else if (myDirection == "UP")
            {
                mainCamera.transform.position = new Vector3(0.5f, -9f, -10f);
            }
            else if (myDirection == "DOWN")
            {
                mainCamera.transform.position = new Vector3(0.5f, 11f, -10f);
            }
        }
        */

        readyForCameraSwitch = true;

        //If we've already solved the main puzzle
        if (mainDoorOpened[currentLevel]) {
            //We have played the level completion sound
            doorOpenPlayed = true;
            //The door should already be enterable
            timeToDoorChange = 5;
            //we have completed the level
            levelCompleted = true;
            //Thus we are ready for a level change
            readyForLevelChange = true;
            //But we should not get rid of the squares
            goodbyeSquares = false;
            squaresDestroyed = 0;
            timeForLevelChange = false;
            // or reset our color
            mySpriteRenderer.color = openDoorColor;
            //If     not,
        } else {
            //We haven't played the level completion sound
            doorOpenPlayed = false;
            //The door needs to wait until all sound stops before becoming enterable 
            timeToDoorChange = 0;
            //if we have not already completed this level
            //We have not completed this new level
            levelCompleted = false;
            //Thus we are not ready for a level change
            readyForLevelChange = false;
            //Thus we should not get rid of the squares
            goodbyeSquares = false;
            squaresDestroyed = 0;
            timeForLevelChange = false;
            //Reset our color
            mySpriteRenderer.color = closedDoorColor;
        }

        playerMoved = false;



        //Depending on what level we're on
        if (currentLevel == 0) {
            //If we're at level 0 this is a problem so reset to level 12
            currentLevel = 12;

            //reset our map position to level 12
            mapPosition[0] = 11;
            mapPosition[1] = 9;

            Debug.Log("Oops! You're on level 0! Probably should fix that, buddy!");
        }
        if (currentLevel == 1){
            setColorPalette(ColorPalette5);

            //Make the instruction square play the correct sound for the solution
            instructionSquareScript.mySound = level1Solution;

            //Set the square's position arrays to that of the correct level

            float[] squarePosX;
            float[] squarePosY;
            if (mainDoorOpened[6])
            {
                squarePosX = squareXPositionMedium;
                squarePosY = squareYPositionMedium;
            }
            else
            {
                squarePosX = squareXPositionSmall;
                squarePosY = squareYPositionSmall;
            }

            squareX = squarePosX;
            squareY = squarePosY;

            correctSquare = level1SquareOrder;
            correctSquarePlayed = new bool[level1SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squarePosX.Length];

            if (mainDoorOpened[6])
            {
                makeSmallDoor("DOWN", levelNullSquareOrder, "", true);
                makeSmallDoor("UP", level6SquareOrder, "TRANSPOSER", false);
            } else {
                makeSmallDoor("DOWN", levelNullSquareOrder, "", false);
                makeSmallDoor("UP", level6SquareOrder, "TRANSPOSER", false);
            }


            myDirection = "RIGHT";
        } else if (currentLevel == 2){
            setColorPalette(ColorPalette3);

            instructionSquareScript.mySound = level2Solution;

            squareX = squareXPositionSmall;
            squareY = squareYPositionSmall;

            correctSquare = level2SquareOrder;
            correctSquarePlayed = new bool[level2SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionSmall.Length];

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);


            myDirection = "RIGHT";
        } else if (currentLevel == 3){
            setColorPalette(ColorPalette4);

            instructionSquareScript.mySound = level3Solution;

            squareX = squareXPositionMedium;
            squareY = squareYPositionMedium;

            correctSquare = level3SquareOrder;
            correctSquarePlayed = new bool[level3SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionMedium.Length];

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);


            myDirection = "DOWN";
        } else if (currentLevel == 4){
            setColorPalette(ColorPalette1);

            instructionSquareScript.mySound = level4Solution;

            squareX = squareXPositionMedium;
            squareY = squareYPositionMedium;

            correctSquare = level4SquareOrder;
            correctSquarePlayed = new bool[level4SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionMedium.Length];

            makeSmallDoor("UP", levelNullSquareOrder, "", true);



            myDirection = "LEFT";
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

            makeSmallDoor("RIGHT", levelNullSquareOrder, "", true);


            myDirection = "LEFT";
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

            makeSmallDoor("RIGHT", levelNullSquareOrder, "", true);


            myDirection = "UP";
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

            makeSmallDoor("DOWN", levelNullSquareOrder, "", true);


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

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);


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

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);


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

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);


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

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);


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

            makeSmallDoor("UP", level1SquareOrder, "1", false);

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);

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

            makeSmallDoor("LEFT", levelNullSquareOrder, "", true);



            myDirection = "DOWN";
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

            makeSmallDoor("UP", levelNullSquareOrder, "", true);


            myDirection = "LEFT";
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

            makeSmallDoor("RIGHT", levelNullSquareOrder, "", true);


            myDirection = "UP";
        } else if (currentLevel == 16)
        {
            instructionSquareScript.mySound = level15Solution;

            squareX = squareXPositionBig;
            squareY = squareYPositionBig;

            correctSquare = level16SquareOrder;
            correctSquarePlayed = new bool[level16SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];

            squarePlayed = new bool[squareXPositionBig.Length];

            makeSmallDoor("DOWN", levelNullSquareOrder, "", true);


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



        //always make the instruction square on the left side of the screen
        Instantiate(instructionSquare, new Vector3(-3f, 1f, 0f), Quaternion.identity);


            Vector3 pos;
        pos = transform.position;
        Vector3 size = transform.localScale;

        //Move us to the correct location
        if (myDirection == "RIGHT") {
            pos = new Vector3(7.15f, 1f, 0f);
            size = new Vector3(1f, 3f, 1f);

            myTextMesh.transform.localScale = new Vector3(1f, 0.33f, 1f);
            myOutline.transform.localScale = new Vector3(1.1f, 1.03f, 1f);
        } else if (myDirection == "LEFT") {
            pos = new Vector3(-6.16f, 1f, 0f);
            size = new Vector3(1f, 3f, 1f);

            myTextMesh.transform.localScale = new Vector3(1f, 0.33f, 1f);
            myOutline.transform.localScale = new Vector3(1.1f, 1.03f, 1f);
        } else if (myDirection == "UP") {
            pos = new Vector3(1f, 6f, 0f);
            size = new Vector3(3f, 1f, 1f);

            myTextMesh.transform.localScale = new Vector3(0.33f, 1f, 1f);
            myOutline.transform.localScale = new Vector3(1.03f, 1.1f, 1f);
        } else if (myDirection == "DOWN") {
            pos = new Vector3(1f, -4f, 0f);
            size = new Vector3(3f, 1f, 1f);

            myTextMesh.transform.localScale = new Vector3(0.33f, 1f, 1f);
            myOutline.transform.localScale = new Vector3(1.03f, 1.1f, 1f);
        }

        transform.position = pos;
        transform.localScale = size;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour {

    public Transform square;
    public Transform instructionSquare;
    public AudioClip[] musicBits;
    public AudioClip doorOpen;

    public bool[] squarePlayed;
    int[] correctSquare;
    public bool[] correctSquarePlayed;
    int nextSquareInCorrectOrder;
    int currentCorrectSquareIndex;

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

    bool levelCompleted;
    public int currentLevel;
    public bool readyForLevelChange;

    public AudioSource myAudioSource;
    public bool doorOpenPlayed;

    public SpriteRenderer mySpriteRenderer;

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

    public float timeToDoorChange;

	// Use this for initialization
    void Start () {
        //We start on the first level,
        //newLevel() increments the level count
        currentLevel = 0;

        doorOpen = musicBits[musicBits.Length - 1];
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = doorOpen;
        doorOpenPlayed = false;

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        newLevel();
	}
	
	// Update is called once per frame
	void Update () {

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
                    }
                    else
                    {
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
                mySpriteRenderer.color = new Color(0, 255, 0);
                //And
                readyForLevelChange = true;
            }
        }

        Debug.Log(musicBits[musicBits.Length - 2].length);
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //If we collide with the player and we are ready for a level change
        if (coll.gameObject.tag == "Player" && readyForLevelChange){
            //go to the next level
            newLevel();
        }
    }

    void newLevel()
    //Do this every time we go to a new level
    {
        //go up a level!
        currentLevel++;

        //We haven't played the level completion sound
        doorOpenPlayed = false;
        //The door needs to wait until all sound stops before becoming enterable 
        timeToDoorChange = 0;
        //We have not completed this new level
        levelCompleted = false;
        //Thus we are not ready for a level change
        readyForLevelChange = false;
        //Reset our color
        mySpriteRenderer.color = new Color(255, 0, 0);

        //Grab the square controller cuz we're gonna need to reset the squares
        squareController squareScript = square.GetComponent<squareController>();
        //Grab the instruction square controller cuz we're gonna need to change
        //its sound depending on the level
        instructionSquareController instructionSquareScript = instructionSquare.GetComponent<instructionSquareController>();
        //Grab the player script here for the sake of consistancy
        //(we only need it to reset their position at the start of each level
        youController youScript = GameObject.FindWithTag("Player").GetComponent<youController>();


        //Depending on what level we're on
        if (currentLevel == 1){
            //Make the instruction square play the correct sound for the solution
            instructionSquareScript.mySound = level1Solution;

            //Set the square's position arrays to that of the correct level
            squareX = Level1SquareXPosition;
            squareY = Level1SquareYPosition;

            //Make sure our correct squarae order is set to the correct level
            correctSquare = level1SquareOrder;
            //Make sure the array that checks if all squares were pressed in the correct order
            //Is as long as the number of squares there are in the melody
            correctSquarePlayed = new bool[level1SquareOrder.Length];
            //Start us off at the first square
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];
        } else if (currentLevel == 2){
            instructionSquareScript.mySound = level2Solution;

            squareX = Level2SquareXPosition;
            squareY = Level2SquareYPosition;

            correctSquare = level2SquareOrder;
            correctSquarePlayed = new bool[level2SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];
        } else if (currentLevel == 3){
            instructionSquareScript.mySound = level3Solution;

            squareX = Level3SquareXPosition;
            squareY = Level3SquareYPosition;

            correctSquare = level3SquareOrder;
            correctSquarePlayed = new bool[level3SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];
        } else if (currentLevel == 4){
            instructionSquareScript.mySound = level4Solution;

            squareX = Level4SquareXPosition;
            squareY = Level4SquareYPosition;

            correctSquare = level4SquareOrder;
            correctSquarePlayed = new bool[level4SquareOrder.Length];
            currentCorrectSquareIndex = 0;
            nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];
        }


        //Make the squares
        for (int x = 0; x < squareX.Length; x++)
        {
            squareScript.mySound = musicBits[x];
            squareScript.myNumber = x;
            Instantiate(square, new Vector3(squareX[x], squareY[x], 0f), Quaternion.identity);
        }

        //move the player back to the start
        Vector3 pos = new Vector3(-5f, 1f, -.15f);
        //The z is -.15 so they're in front of everything
        youScript.transform.position = pos;


        //Make the instruction square
        Instantiate(instructionSquare, new Vector3(-4f, 1f, 0f), Quaternion.identity);


    }
}

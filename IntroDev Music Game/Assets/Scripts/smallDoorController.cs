using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallDoorController : MonoBehaviour {

    public bool mySolutionCompleted;
    public bool[] squarePlayed;
    public int[] correctSquare;
    public bool[] correctSquarePlayed;
    public int nextSquareInCorrectOrder;
    public int currentCorrectSquareIndex;


    public AudioClip doorOpen;
    public bool doorOpenPlayed;
    public float timeToDoorChange;
    public bool readyForLevelChange;

    public int theLevelIGoTo;

    AudioSource myAudioSource;
    public SpriteRenderer mySpriteRenderer;
    public TextMesh myTextMesh;

    doorController doorScript;

	// Use this for initialization
	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myTextMesh = GetComponentInChildren<TextMesh>();

        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        mySolutionCompleted = false;
        doorOpenPlayed = false;
        myAudioSource.clip = doorOpen;
	}
	
	// Update is called once per frame
	void Update () {

        //ARE WE PLAYING THE SQUARES IN ORDER????

        //Check all the squares to see if they are being played
        for (int x = 0; x < doorScript.squarePlayed.Length; x++)
        {
            if (doorScript.squarePlayed[x])
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
                        mySolutionCompleted = true;
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

        ///*** LEVEL END STUFF ***\\\

        //if we've completed the level and haven't played the door success noise
        if (mySolutionCompleted && doorOpenPlayed == false)
        {
            //Start playing the door success noise after the last square note has ended
            myAudioSource.PlayDelayed(doorScript.musicBits[correctSquare[correctSquare.Length - 2]].length);
            //Record this (so we don't play the sound every frame
            doorOpenPlayed = true;
        }

        //If we've started playing the success sound
        if (doorOpenPlayed)
        {
            //Count down until both the square note and the door success noise have ended
            if (timeToDoorChange < myAudioSource.clip.length + doorScript.musicBits[correctSquare[correctSquare.Length - 2]].length)
            {
                timeToDoorChange += Time.deltaTime;
            }
            else
            {
                //When they have, change to the door success color
                mySpriteRenderer.color = new Color(0, 1, 0);
                //And
                readyForLevelChange = true;
            }
        }

        //If we get a message from the door that the level is going to change
        if (doorScript.goodbyeSquares) {
            //Record we destroyed ourselves
            doorScript.numberOfSmallDoors--;
            //destroy ourselves
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //If we collide with the player and we are ready for a level change
        if (coll.gameObject.tag == "Player" && readyForLevelChange)
        {
   
            //go to our level
            doorScript.currentLevel = theLevelIGoTo;

            //Prepare to change to it
            doorScript.timeForLevelChange = true;

            //Record we're about to destory ourselves
            doorScript.numberOfSmallDoors--;

            //Delete ourselves
            Destroy(gameObject);
        }
    }
}

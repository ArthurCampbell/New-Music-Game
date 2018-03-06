using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallDoorController : MonoBehaviour {

    public bool mySolutionCompleted;
    public int[] correctSquare;
    public bool[] correctSquarePlayed;
    public int nextSquareInCorrectOrder;
    public int currentCorrectSquareIndex;


    public AudioClip doorOpen;
    public bool doorOpenPlayed;
    public float timeToDoorChange;
    public bool readyForLevelChange;

    public string myDirection;

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

        //NEW CODE
        Vector3 pos = transform.position;
        Vector3 size = transform.localScale;

        //Move us to the correct location
        //Move us to the correct location
        if (myDirection == "RIGHT")
        {
            pos = new Vector3(6f, 1f, 0f);
            size = new Vector3(1f, 3f, 1f);

            myTextMesh.transform.localScale = new Vector3(1f, 0.33f, 1f);
        }
        else if (myDirection == "LEFT")
        {
            pos = new Vector3(-5f, 1f, 0f);
            size = new Vector3(1f, 3f, 1f);

            myTextMesh.transform.localScale = new Vector3(1f, 0.33f, 1f);
        }
        else if (myDirection == "UP")
        {
            pos = new Vector3(1f, 4f, 0f);
            size = new Vector3(3f, 1f, 1f);

            myTextMesh.transform.localScale = new Vector3(0.33f, 1f, 1f);
        }
        else if (myDirection == "DOWN")
        {
            pos = new Vector3(1f, -2f, 0f);
            size = new Vector3(3f, 1f, 1f);

            myTextMesh.transform.localScale = new Vector3(0.33f, 1f, 1f);
        }

        transform.position = pos;
        transform.localScale = size;
        //END NEW CODE
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
                //When they have
                readyForLevelChange = true;
            }
        }

        //If we're ready for a level change
        if (readyForLevelChange == true) {
            mySpriteRenderer.color = new Color(0, 1, 0);
        }

        //If we get a message from the door that the level is going to change
        if (doorScript.goodbyeSquares) {
            Debug.Log("smallDoor Destroyed!" + Time.frameCount);
            //Record we destroyed ourselves
            doorScript.numberOfSmallDoors--;
            //destroy ourselves
            Destroy(gameObject);
        }
        Debug.Log("test" + Time.frameCount);
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //If we collide with the player and we are ready for a level change
        if (coll.gameObject.tag == "Player" && readyForLevelChange)
        {
   
            //This is real fucked up but in order to change levels if the player has
            //gone through this door we change the main door's direction to our direction
            //And then tell the main door to change levels -- effectively meaning any time
            //we go through this door we're actually just changing where the main door goes
            //and then going through the main door
            doorScript.myDirection = myDirection;

            doorScript.findNextLevel();

            //Prepare to change to it
            doorScript.timeForLevelChange = true;

            //Record we're about to destory ourselves
            doorScript.numberOfSmallDoors--;

            //Don't destroy ourselves cuz we get destroyed when we go thru the main door :P
        }
    }
}

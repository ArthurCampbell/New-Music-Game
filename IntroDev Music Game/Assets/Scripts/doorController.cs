using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour {

    public Transform square;
    public AudioClip[] musicBits;
    public AudioClip doorOpen;

    public bool[] squarePlayed;
    public int[] correctSquare;
    public bool[] correctSquarePlayed;
    public int nextSquareInCorrectOrder;
    public int currentCorrectSquareIndex;

    //HELLO HI IF YOU WANT TO EDIT THE MELODY/SOLUTION ONLY EDIT THE ARRAYS WITH LEVEL[number]
    //IN FRONT OF THEM THANK YOU
    //this is because we set all other arrays based on this one so things could get confusing otherwise
    public int[] level1SquareOrder;

    bool levelCompleted;

    public AudioSource myAudioSource;
    public bool doorOpenPlayed;

    public SpriteRenderer mySpriteRenderer;

    public float[] squareX;
    public float[] squareY;

    public float timeToDoorChange;

	// Use this for initialization
    void Start () {
        squareController squareScript = square.GetComponent<squareController>();
        for (int x = 0; x < musicBits.Length - 1; x++)
        {
            squareScript.mySound = musicBits[x];
            squareScript.myNumber = x;
            Instantiate(square, new Vector3(squareX[x], squareY[x], 0f), Quaternion.identity);
        }

        correctSquare = level1SquareOrder;
        correctSquarePlayed = new bool[level1SquareOrder.Length];
        currentCorrectSquareIndex = 0;
        nextSquareInCorrectOrder = correctSquare[currentCorrectSquareIndex];


        doorOpen = musicBits[musicBits.Length - 1];
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = doorOpen;
        doorOpenPlayed = false;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
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


        //End of level stuff
        if (levelCompleted && doorOpenPlayed == false) {
            myAudioSource.PlayDelayed(musicBits[musicBits.Length-2].length);
            doorOpenPlayed = true;

        }

        if (doorOpenPlayed) {
            if (timeToDoorChange < myAudioSource.clip.length){
                timeToDoorChange += Time.deltaTime;
            } else {
                mySpriteRenderer.color = new Color(0, 255, 0);
            }

        }
	}
}

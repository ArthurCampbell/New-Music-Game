using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour {

    public Transform square;
    public AudioClip[] musicBits;
    public AudioClip doorOpen;

    public bool[] squarePlayed;

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

        doorOpen = musicBits[musicBits.Length - 1];
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = doorOpen;
        doorOpenPlayed = false;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (squarePlayed[0] && squarePlayed[1] && squarePlayed[2] && squarePlayed[3]
            && squarePlayed[4] && squarePlayed[5] && doorOpenPlayed == false) {
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

        //Debug.Log(squarePlayed[0] + ", " + squarePlayed[1] + ", " + squarePlayed[2] + ", " + 
        //          squarePlayed[3] + ", " + squarePlayed[4] + ", " + squarePlayed[5]);

	}
}

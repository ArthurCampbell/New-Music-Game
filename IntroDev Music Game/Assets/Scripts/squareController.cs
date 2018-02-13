using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareController : MonoBehaviour {

    AudioSource myAudioSource;
    public AudioClip mySound;
    public SpriteRenderer mySpriteRenderer;

    public Color pressedColor;

    public int myNumber;
    public doorController doorScript;

    public youController youScript;

	// Use this for initialization
	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = mySound;
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
        youScript = GameObject.FindWithTag("Player").GetComponent<youController>();
	}
	
	// Update is called once per frame
	void Update () {
        //If our audio is playing
        if (myAudioSource.isPlaying) {
            //And the player is on top of us
            if (Vector3.Distance(youScript.transform.position, transform.position) < 0.6f) {
                //Keep playing, we're fine!
            } else {
                //If the player is not on top of us, stop playing so we don't get any
                //Weird audio overlap
                myAudioSource.Stop();
            }
        }

        //If we have just been played
        if (doorScript.squarePlayed[myNumber]) {
            //record we are not being played, because we only need to record we have 
            //been played for a frame
            doorScript.squarePlayed[myNumber] = false;
        }

        //If we are going to go to the next level, we need to destory ourselves so we can 
        //be created again in the proper place in the next level
        if (doorScript.goodbyeSquares) {
            doorScript.squaresDestroyed++;
            Destroy(gameObject);
        }

        Color myColor = mySpriteRenderer.color;
        if (myColor != new Color(1, 1, 1)) {
            if (myColor.r < 1f) {
                myColor.r += (1 - myColor.r) * Time.deltaTime * 2;
            }
            if (myColor.g < 1f)
            {
                myColor.g += (1 - myColor.g) * Time.deltaTime * 2;
            }
            if (myColor.b < 1f)
            {
                myColor.b += (1 - myColor.b) * Time.deltaTime * 2;
            }
        }
        mySpriteRenderer.color = myColor;
	}

    //If we run into something
    void OnCollisionEnter2D(Collision2D coll)
    {
        //and that thing is the player
        if (coll.gameObject.tag == "Player")
        {
            //if the door open sound is playing
            if (doorScript.doorOpenPlayed && doorScript.timeToDoorChange < doorScript.myAudioSource.clip.length)
            {
                //don't play our sound
            }
            else
            {
                //play our sound
                myAudioSource.Play();

                //Record we have been played!
                doorScript.squarePlayed[myNumber] = true;

                //Change our color, because we have been played!
                mySpriteRenderer.color = pressedColor;
            }
        }
    }
}

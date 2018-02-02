using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareController : MonoBehaviour {

    AudioSource myAudioSource;
    public AudioClip mySound;
    public SpriteRenderer mySpriteRenderer;

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

        if (myAudioSource.isPlaying) {


            if (Vector3.Distance(youScript.transform.position, transform.position) < 0.6f){

            } else {
                myAudioSource.Stop();
            }

        }

        if (doorScript.squarePlayed[myNumber]){
            mySpriteRenderer.color = new Color(0, 255, 0);
        } else {
            mySpriteRenderer.color = new Color(255, 255, 255);
        }
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
            }
            //If we are the first square
            if (myNumber == 0){
                //if the second square in the order has been played
                if (doorScript.squarePlayed[1] == true)
                {
                    //reset all squares as having not been played in the correct order
                    for (int x = 0; x < doorScript.squarePlayed.Length; x++)
                    {
                        doorScript.squarePlayed[x] = false;
                    }
                }

                //No matter what, record us as having been played in the correct order
                //(We are the first square, we are always played in the correct order)
                doorScript.squarePlayed[0] = true;


                //If we are not the first square
                //And the square before us has been played
            } else if (doorScript.squarePlayed[myNumber - 1] == true) {
                //And we haven't been played
                if (doorScript.squarePlayed[myNumber] == false)
                {
                    if (myNumber < 6)
                    {
                        //Record us as having been played in the correct order
                        doorScript.squarePlayed[myNumber] = true;
                    }
                } else {
                    //Otherwise, reset everything
                    for (int x = 0; x < doorScript.squarePlayed.Length; x++)
                    {
                        doorScript.squarePlayed[x] = false;
                    }
                }
                //If the square before us hasn't been played (and we are not the first square)
            } else {
                //Reset all the squares
                for (int x = 0; x < doorScript.squarePlayed.Length; x++){
                    doorScript.squarePlayed[x] = false;
                }
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionSquareController : MonoBehaviour
{

    AudioSource myAudioSource;
    public AudioClip mySound;
    public SpriteRenderer mySpriteRenderer;

    public doorController doorScript;

    public youController youScript;

    // Use this for initialization
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = mySound;
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
        youScript = GameObject.FindWithTag("Player").GetComponent<youController>();

        mySpriteRenderer.color = doorScript.CurrentColorPalette[1];
    }

    // Update is called once per frame
    void Update()
    {
        //If our audio is playing
        if (myAudioSource.isPlaying)
        {
            //And the player is on top of us
            if (Vector3.Distance(youScript.transform.position, transform.position) < 0.6f)
            {
                //Keep playing, we're fine!
            }
            else
            {
                //If the player is not on top of us, stop playing so we don't get any
                //Weird audio overlap
                myAudioSource.Stop();
            }
            transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        } else {
            transform.localScale = new Vector3(0.85f, 0.85f, 1f);
        }

        //If we are going to go to the next level, we need to destory ourselves so we can 
        //be created again with the proper sound in the next level
        if (doorScript.goodbyeSquares)
        {
            Destroy(gameObject);
        }

        /*
        if (doorScript.correctSquarePlayed[correctSquaremyNumber]){
            mySpriteRenderer.color = new Color(0, 255, 0);
        } else {
            mySpriteRenderer.color = new Color(255, 255, 255);
        }
        */
    }

    //If we run into something
    void OnCollisionEnter2D(Collision2D coll)
    {
        //and that thing is the player
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Drumstick")
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
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressBarController : MonoBehaviour {

    //SpriteRenderer mySpriteRenderer;

    doorController doorScript;

    SpriteRenderer mySpriteRenderer;

    GameObject progressBarCover;
    SpriteRenderer progressBarCoverSpriteRenderer;

	// Use this for initialization
	void Start () {
        //mySpriteRenderer = GetComponent<SpriteRenderer>();
        doorScript = GameObject.FindGameObjectWithTag("Door").GetComponent<doorController>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        progressBarCover = GameObject.Find("Progress Bar Cover");
        progressBarCoverSpriteRenderer = progressBarCover.GetComponent<SpriteRenderer>();


        //Make the progress bar 8 long
        transform.localScale = new Vector3(8f, 0.4f, 1f);

	}
	
	// Update is called once per frame
	void Update () {

        //Make us the appropriate color
        mySpriteRenderer.color = doorScript.CurrentColorPalette[0];

        //Make the progress bar cover the same color as the background
        Color coverColor = Camera.main.backgroundColor;
        coverColor.a = 1f;
        progressBarCoverSpriteRenderer.color = coverColor;

        //We do the above things every frame because the progress bar doesn't respawn on room change
        //Could probably change this for optimization if it runs bad
        

        //Find out how far we are in the progress of finishing the melody
        float progress;
        progress = doorScript.currentCorrectSquareIndex * 1.0f / doorScript.correctSquarePlayed.Length;

        //That previous variable only works if we're not one square from completing the melody
        //Because of some array things
        //So we see if we've completed the level and then if we have draw a completed progress bar
        if (doorScript.levelCompleted)
        {
            progressBarCover.transform.localPosition = new Vector3(1f, 0f, -0.01f);
        }
        //If we haven't, use the progress variable we made earlier
        else
        {
            progressBarCover.transform.localPosition = new Vector3(
                progress, 0f, -0.5f);
        }


	}
}

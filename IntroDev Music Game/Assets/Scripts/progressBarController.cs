using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressBarController : MonoBehaviour {

    //SpriteRenderer mySpriteRenderer;

   public doorController doorScript;

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
        float ourColorH, ourColorS, ourColorV;
        Color.RGBToHSV(progressBarCoverSpriteRenderer.color, out ourColorH, out ourColorS, out ourColorV);
        ourColorV -= .08f;
        progressBarCoverSpriteRenderer.color = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);


        //We do the above things every frame because the progress bar doesn't respawn on room change
        //Could probably change this for optimization if it runs bad
        

        //Find out how far we are in the progress of finishing the melody
        float progressLeft;
        progressLeft = (doorScript.correctSquarePlayed.Length - doorScript.currentCorrectSquareIndex * 1.0f) / doorScript.correctSquarePlayed.Length;

        Vector3 coverSize = progressBarCover.transform.localScale;
        Vector3 coverPosition = progressBarCover.transform.localPosition;

        //That previous variable only works if we're not one square from completing the melody
        //Because of some array things
        //So we see if we've completed the level and then if we have draw a completed progress bar
        if (doorScript.levelCompleted)
        {
            coverSize = new Vector3(0f, 1f, 1f);
        }
        //If we haven't, use the progress variable we made earlier
        else
        {
            coverSize = new Vector3(progressLeft, 1f, 1f);
            coverPosition = new Vector3(0.5f - progressLeft * 0.5f, 0f, -0.5f);
        }

        progressBarCover.transform.localScale = coverSize;
        progressBarCover.transform.localPosition = coverPosition;

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backdropController : MonoBehaviour {

    //We'll only use these three if we end up changing backgrounds
    //randomly (at a time unrelated to what the player is doing)
    public Sprite[] squareBackrounds;
    public Sprite[] sphereBackgrounds;
    public Sprite[] capsuleBackgrounds;

    //We'll use this if, for example, the background changes whenever the player hits a square
    public Sprite[] Backgrounds;
    public Sprite currentBackground;
    public int currentBackgroundNumber;

    public bool squarePressed;

    SpriteRenderer mySpriteRenderer;

    doorController doorScript;


    public float framesToBackgroundChange;

	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
	}
	
	// Update is called once per frame
	void Update () {

        Color backgroundColor = doorScript.CurrentColorPalette[4];
        float ourColorH, ourColorS, ourColorV;
        Color.RGBToHSV(backgroundColor, out ourColorH, out ourColorS, out ourColorV);
        Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
        ourColorV += .031f;
        ourColorS -= .004f;
        Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
        Color ourColor = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);
        mySpriteRenderer.color = ourColor;

        

        /*
        //change what background we're using if we press a square
        for (int x = 0; x < doorScript.squarePlayed.Length; x++) {
            if (doorScript.squarePlayed[x]) {
                if (currentBackgroundNumber < Backgrounds.Length - 1) {
                    currentBackgroundNumber++;
                } else {
                    currentBackgroundNumber = 0;
                }
                squarePressed = false;
            }
        }
        */

        if (framesToBackgroundChange == 0) {
            framesToBackgroundChange += Mathf.Round(Random.Range(3, 15));
            if (framesToBackgroundChange < 9)
            {
                currentBackgroundNumber++;
            }
            else
            {
                currentBackgroundNumber--;
            }
        } else {
            framesToBackgroundChange--;
        }
        if (currentBackgroundNumber > Backgrounds.Length - 1) {
            currentBackgroundNumber = Backgrounds.Length - 1;
        } else if (currentBackgroundNumber < 0) {
            currentBackgroundNumber = 0;
        }
        currentBackground = Backgrounds[currentBackgroundNumber];
        mySpriteRenderer.sprite = currentBackground;
	}
}

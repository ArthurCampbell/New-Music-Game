using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareController : MonoBehaviour {

    AudioSource myAudioSource;
    public AudioClip mySound;
    public SpriteRenderer mySpriteRenderer;
    public TextMesh myTextMesh;

    public Sprite[] numbers;
    public GameObject myNumberDisplay;

    public Color unpressedColor;
    public Color pressedColor;
    Color lightColor;
    Color heavyColor;
    Color middleColor;
    Color currentColor;
    float colorCycle;

    public int myNumber;
    public doorController doorScript;
    public smallDoorController smallDoorScript;
    public backdropController backdropScript;

    public youController youScript;

	// Use this for initialization
	void Start () {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = mySound;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myTextMesh = GetComponentInChildren<TextMesh>();

        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
        youScript = GameObject.FindWithTag("Player").GetComponent<youController>();
        backdropScript = GameObject.Find("backdrop").GetComponent<backdropController>();

        int myDisplayNumber = myNumber + 1;

        myTextMesh.text = myDisplayNumber + "";

        squareNumberController myNumberDisplayController = myNumberDisplay.GetComponent<squareNumberController>();
        myNumberDisplayController.mySpriteRenderer.sprite = numbers[myNumber];
	}
	
	// Update is called once per frame
	void Update () {

        if (doorScript.pauseMenu)
        {
            
        }
        else
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

            //If we have just been played
            if (doorScript.squarePlayed[myNumber])
            {
                //record we are not being played, because we only need to record we have 
                //been played for a frame
                doorScript.squarePlayed[myNumber] = false;
            }

            //If we are going to go to the next level, we need to destory ourselves so we can 
            //be created again in the proper place in the next level
            if (doorScript.goodbyeSquares)
            {
                doorScript.squaresDestroyed++;
                Destroy(gameObject);
            }

            /*
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
            */

            lightColor = doorScript.CurrentColorPalette[0];
            float ourColorH, ourColorS, ourColorV;
            Color.RGBToHSV(lightColor, out ourColorH, out ourColorS, out ourColorV);
            //Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
            ourColorH -= .02f;
            ourColorS += .05f;
            ourColorV -=  0f;
            if (ourColorH > 1) { ourColorH = 1; }
            if (ourColorS > 1) { ourColorS = 1; }
            if (ourColorV > 1) { ourColorV = 1; }
            if (ourColorH < 0) { ourColorH = 0; }
            if (ourColorS < 0) { ourColorS = 0; }
            if (ourColorV < 0) { ourColorV = 0; }
            //Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
            heavyColor = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);

            lightColor = doorScript.CurrentColorPalette[0];
            Color.RGBToHSV(lightColor, out ourColorH, out ourColorS, out ourColorV);
            ourColorH -= 0f;
            ourColorS -= .05f;
            ourColorV += 0f;
            if (ourColorH > 1) { ourColorH = 1; }
            if (ourColorS > 1) { ourColorS = 1; }
            if (ourColorV > 1) { ourColorV = 1; }
            if (ourColorH < 0) { ourColorH = 0; }
            if (ourColorS < 0) { ourColorS = 0; }
            if (ourColorV < 0) { ourColorV = 0; }
            //Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
            lightColor = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);

            if (lightColor.r > heavyColor.r)
            {
                middleColor.r = (lightColor.r - heavyColor.r) / 2 + heavyColor.r;
            }
            else
            {
                middleColor.r = (heavyColor.r - lightColor.r) / 2 + lightColor.r;
            }
            if (lightColor.g > heavyColor.g)
            {
                middleColor.g = (lightColor.g - heavyColor.g) / 2 + heavyColor.g;
            }
            else
            {
                middleColor.g = (heavyColor.g - lightColor.g) / 2 + lightColor.g;
            }
            if (lightColor.b > heavyColor.b)
            {
                middleColor.b = (lightColor.b - heavyColor.b) / 2 + heavyColor.b;
            }
            else
            {
                middleColor.b = (heavyColor.b - lightColor.b) / 2 + lightColor.b;
            }

            if (colorCycle <= 0)
            {
                colorCycle = Random.Range(Time.deltaTime * 15, Time.deltaTime * 30);
                float colorToGoTo = Random.Range(0f, 3f);
                if (colorToGoTo < 1)
                {
                    currentColor = new Color(lightColor.r, lightColor.g, lightColor.b);
                }
                else if (colorToGoTo > 2)
                {
                    currentColor = new Color(heavyColor.r, heavyColor.g, heavyColor.b);
                }
                else
                {
                    currentColor = new Color(middleColor.r, middleColor.g, middleColor.b);
                }
            }
            else
            {
                colorCycle -= Time.deltaTime;
            }

            mySpriteRenderer.color = currentColor;
        }

	}

    //If we run into something
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (doorScript.pauseMenu)
        {

        }
        else
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

                    /*
                    //Change our color, because we have been played!
                    mySpriteRenderer.color = pressedColor;
                    */

                }
            }
        }
    }
}

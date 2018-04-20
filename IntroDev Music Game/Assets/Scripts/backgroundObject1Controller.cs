using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundObject1Controller : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public Vector3 rotation;

    public bool backgroundMode;

    public Vector3 scaleVector;
    public float currentScale;
    public float startingScale;

    //public int[] myLevels;
    public bool dontDestroyUs;

    doorController doorScript;

    SpriteRenderer mySpriteRenderer;
    public GameObject myPattern;
    SpriteRenderer patternSpriteRenderer;

	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        patternSpriteRenderer = myPattern.GetComponent<SpriteRenderer>();

        dontDestroyUs = true;

        reset();
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 pos = transform.position;

        pos.x += direction.x * speed * Time.deltaTime;
        pos.y += direction.y * speed * Time.deltaTime;

        if (backgroundMode) {

            pos.z = 30;

        } else {
            pos.z = 10;
        }

        transform.position = pos;


        Quaternion rot = transform.rotation;

        rotation = rot.eulerAngles;

        rotation.y += 10f * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rotation);

        if (pos.x > 8 || pos.x < -7 || pos.y > 7 || pos.y < -5) {
            reset();
        }

        if (doorScript.readyForCameraSwitch == false) {
            dontDestroyUs = false;
        }

        if (doorScript.readyForCameraSwitch && dontDestroyUs == false) {
            Destroy(gameObject);
        }

        /*
        scaleVector = transform.localScale;
        currentScale = scaleVector.x;

        for (int x = 0; x < doorScript.squarePlayed.Length; x++)
        {
            if (doorScript.squarePlayed[x])
            {
                currentScale += 1;
            }
        }

        float scaleDifference = currentScale - startingScale;
        if (Mathf.Abs(scaleDifference) <= 0.01f) {
            currentScale = startingScale;
        } else if (scaleDifference > 0) {
            currentScale -= scaleDifference * Time.deltaTime;
        } else if (currentScale < startingScale) {
            currentScale -= -scaleDifference * Time.deltaTime;
        }

        Debug.Log(currentScale + " " + startingScale);
        scaleVector = new Vector3(currentScale, currentScale, 1f);
        transform.localScale = scaleVector;
        */
	}

    void reset() {
        direction = new Vector3(0, 0, 5);

        if (backgroundMode)
        {
            patternSpriteRenderer.color = new Color(1, 1, 1, 0);

            int colorChoice = Mathf.RoundToInt(Random.Range(0, 3));

            if (colorChoice == 0f) {
                mySpriteRenderer.color = doorScript.CurrentColorPalette[0];
            } else if (colorChoice == 1f) {
                mySpriteRenderer.color = doorScript.CurrentColorPalette[1];
            } else {
                Color backgroundColor = doorScript.CurrentColorPalette[1];
                float ourColorH, ourColorS, ourColorV;
                Color.RGBToHSV(backgroundColor, out ourColorH, out ourColorS, out ourColorV);
                //Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
                ourColorV += .031f;
                ourColorS -= .004f;
                //Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
                Color ourColor = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);
                mySpriteRenderer.color = ourColor;
            }
        }
        else
        {
            Color myColor = doorScript.CurrentColorPalette[1];
            Color myPatternColor = doorScript.CurrentColorPalette[0];

            myColor.a = 1;
            myPatternColor.a = 1;

            mySpriteRenderer.color = myColor;
            patternSpriteRenderer.color = myPatternColor;
        }

        /*
         * Old color computation (slightly lighter than background color)
        Color backgroundColor = doorScript.CurrentColorPalette[4];
        float ourColorH, ourColorS, ourColorV;
        Color.RGBToHSV(backgroundColor, out ourColorH, out ourColorS, out ourColorV);
        Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
        ourColorV += .031f;
        ourColorS -= .004f;
        Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
        Color ourColor = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);
        patternSpriteRenderer.color = ourColor;
        Debug.Log(ourColor);
        */

        //Make new position
        int sideToSpawnOn = Mathf.RoundToInt(Random.Range(0, 4));
        float x;
        float y;
        //spawn on right
        if (sideToSpawnOn == 0) {
            x = 8;
            y = Random.Range(-4, 6);
            direction = new Vector3(-1, Random.Range(-2, 2), 5);
        //spawn on top
        } else if (sideToSpawnOn == 1) {
            x = Random.Range(-6, 7);
            y = 7;
            direction = new Vector3(Random.Range(-2, 2), -1, 5);
        //spawn on left
        } else if (sideToSpawnOn == 2) {
            x = -7;
            y = Random.Range(-4, 6);
            direction = new Vector3(1, Random.Range(-2, 2), 5);
        //spawn on bottom
        } else {
            x = Random.Range(-6, 7);
            y = -5;
            direction = new Vector3(Random.Range(-2, 2), 1, 5);
        }

        if (backgroundMode) {
            x = Random.Range(-6f, 7f);
            y = Random.Range(-4f, 6f);
        }

        //Go towards the center
        //Randomize that so it's center ish

        //Randomize our shape
        if (backgroundMode)
        {
            startingScale = Random.Range(1f, 1.5f);
            currentScale = startingScale;

            speed = 1;
        }
        else
        {
            startingScale = Random.Range(0.5f, 1f);
            currentScale = startingScale;

            speed = 4;
        }

        Vector3 scale = transform.localScale;
        scale = new Vector3(currentScale, currentScale, 1f);
        transform.localScale = scale;

        //Randomize our speed

        direction = direction.normalized;

        Vector3 resetPos = transform.position;

        resetPos.x = x;
        resetPos.y = y;

        transform.position = resetPos;

        //Randomize our rotation
        Quaternion rot = transform.rotation;

        float rotY = Random.Range(-180f, 180f);
        //Debug.Log("rotY " + rotY);
        Vector3 newRot = new Vector3(0, rotY, 0);
        //rot = Quaternion.Euler(newRot);

        transform.rotation = Quaternion.Euler(newRot);
    }
}

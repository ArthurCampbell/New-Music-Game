using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youController : MonoBehaviour {

    public float speed;
    public float rotZ;

    public bool canSwitchCharacters;

    public doorController doorScript;

    SpriteRenderer mySpriteRenderer;
    Color heavyColor;
    Color middleColor;
    Color lightColor;
    Color currentColor;
    float colorCycle;
    //float colorCycleMax;

    GameObject drumstick;
    GameObject me;

    //public int currentSquarePos;

	// Use this for initialization
	void Start () {
        heavyColor = new Color(1, 1, 1, 1);
        middleColor = new Color(1, 1, 1, 1);
        lightColor = new Color(1, 1, 1, 1);
        currentColor = new Color(1, 1, 1, 1);

        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
        drumstick = GameObject.Find("drumstick");
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        me = gameObject;

        canSwitchCharacters = false;

        Vector3 pos = transform.position;
        //We start one to the left of the instruction square
        pos.x = -6f;
        pos.y = 1f;
        pos.z = -0.15f;
        //currentSquarePos = 0;
        transform.position = pos;

        drumstick.transform.localPosition = pos; 
        drumstick.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (doorScript.pauseMenu)
        {

        }
        else
        {
            //Reference numbers from original palette
            //Light: 42 67 98 / 42 51 99
            //HEavy: 35 94 100

            lightColor = doorScript.CurrentColorPalette[0];
            float ourColorH, ourColorS, ourColorV;
            Color.RGBToHSV(lightColor, out ourColorH, out ourColorS, out ourColorV);
            //Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
            ourColorH -= .07f;
            ourColorS += .27f;
            ourColorV -= .02f;
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
            ourColorS -= .16f;
            ourColorV += .01f;
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
            } else {
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

            if (colorCycle <= 0) {
                colorCycle = Random.Range(Time.deltaTime * 3, Time.deltaTime * 10);
                float colorToGoTo = Random.Range(0f, 3f);
                if (colorToGoTo < 1) {
                    currentColor = new Color(lightColor.r, lightColor.g, lightColor.b);
                } else if (colorToGoTo > 2) {
                    currentColor = new Color(heavyColor.r, heavyColor.g, heavyColor.b);
                } else {
                    currentColor = new Color(middleColor.r, middleColor.g, middleColor.b);
                }
            } else {
                colorCycle -= Time.deltaTime;
            }

            mySpriteRenderer.color = currentColor;
            //

            Vector3 pos = transform.position;

            if (doorScript.readyForCameraSwitch == false)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    pos.x -= 1f;
                    rotZ = 90;

                    switchCharacters();
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    pos.x += 1f;
                    rotZ = -90;

                    switchCharacters();
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    pos.y += 1f;
                    rotZ = 0;

                    switchCharacters();
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    pos.y -= 1f;
                    rotZ = 180;

                    switchCharacters();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                drumstick.SetActive(!drumstick.activeSelf);
                me.SetActive(!me.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                canSwitchCharacters = !canSwitchCharacters;
            }

            if (doorScript.currentLevel > 12 && doorScript.currentLevel < 16)
            {
                canSwitchCharacters = true;
            }
            else
            {
                canSwitchCharacters = false;
            }

            transform.position = pos;
            drumstick.transform.position = pos;


            Quaternion rot = transform.rotation;

            rot = Quaternion.Euler(0f, 0f, rotZ);

            transform.rotation = rot;
        }
	}

    void switchCharacters() {
        if (canSwitchCharacters) {
            drumstick.SetActive(!drumstick.activeSelf);
            me.SetActive(!me.activeSelf);
        }
    }
}

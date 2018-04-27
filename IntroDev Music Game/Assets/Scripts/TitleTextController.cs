using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextController : MonoBehaviour {

    doorController doorScript;

    TextMesh myTextMesh;

    public Color myColor;

	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindGameObjectWithTag("Door").GetComponent<doorController>();

        myTextMesh = GetComponent<TextMesh>();

        myColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {

        //Change Colors
        float ourColorH, ourColorS, ourColorV;
        Color.RGBToHSV(myColor, out ourColorH, out ourColorS, out ourColorV);
        ourColorH += .2f * Time.deltaTime;
        ourColorS = 0.8f;
        ourColorV = 0.8f;
        myColor = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);

        if (doorScript.currentLevel != 15)
        {
            myColor.a = 0;
        }

        if (doorScript.readyForCameraSwitch)
        {
            if (doorScript.currentLevel == 15)
            {
                myColor.a = 1;
            }
            else
            {
                myColor.a = 0;
            }
        }

        myTextMesh.color = myColor;
	}
}

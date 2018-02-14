using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectMachineController : MonoBehaviour {

    public GameObject levelSelectSquare;
    levelSelectController levelSelectScript;
    doorController doorScript;

    public float levelSelectSquareX;
    public float levelSelectSquareY;
    public float levelSelectSquareXStart;

    public bool levelSelectSquaresMade;

	// Use this for initialization
	void Start () {
        levelSelectScript = levelSelectSquare.GetComponent<levelSelectController>();
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        levelSelectSquareXStart = -6f;
        levelSelectSquareX = levelSelectSquareXStart;
        levelSelectSquareY = 5;

        levelSelectSquaresMade = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L) && levelSelectSquaresMade == false){
            //For Each Level
            for (int x = 1; x <= 15; x++) {
                //assign it to a level select square
                levelSelectScript.myLevel = x;
                //Spawn a level select square
                Instantiate(levelSelectSquare, new Vector3(levelSelectSquareX, levelSelectSquareY, 0f), Quaternion.identity);

                levelSelectSquareX++;
                if (levelSelectSquareX > 6) {
                    levelSelectSquareY--;
                    levelSelectSquareX = -6;
                }
            }

            levelSelectSquaresMade = true;

        }
	}
}

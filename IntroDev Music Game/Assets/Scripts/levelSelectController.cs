using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectController : MonoBehaviour {

    public int myLevel;

    public TextMesh myLevelNumber;

    levelSelectMachineController levelSelectMachineScript;

    doorController doorScript;

	// Use this for initialization
	void Start () {
        myLevelNumber = GetComponentInChildren < TextMesh > ();

        levelSelectMachineScript = GameObject.Find("LevelSelectMachine").GetComponent<levelSelectMachineController>();
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
	}
	
	// Update is called once per frame
	void Update () {
        myLevelNumber.text = myLevel + "";

        if (Input.GetKeyDown(KeyCode.L)) {
            levelSelectMachineScript.levelSelectSquaresMade = false;
            levelSelectMachineScript.levelSelectSquareX = levelSelectMachineScript.levelSelectSquareXStart;
            levelSelectMachineScript.levelSelectSquareY = 5;
            Destroy(gameObject);
        }

	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player"){

            if (myLevel != doorScript.currentLevel) {
                doorScript.currentLevel = myLevel - 1;
                doorScript.timeForLevelChange = true;
            }
        }
    }
}

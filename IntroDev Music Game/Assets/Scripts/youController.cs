using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youController : MonoBehaviour {

    public float speed;
    //bool facingLeft;

    public bool canSwitchCharacters;

    public doorController doorScript;

    GameObject drumstick;
    GameObject me;

    //public int currentSquarePos;

	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
        drumstick = GameObject.Find("drumstick");
        me = gameObject;

        canSwitchCharacters = false;

        Vector3 pos = transform.position;
        //We start one to the left of the instruction square
        pos.x = -6f;
        pos.y = 1f;
        //currentSquarePos = 0;
        transform.position = pos;

        drumstick.transform.localPosition = pos; 
        drumstick.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            pos.x -= 1f;
            //facingLeft = true;

            switchCharacters();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos.x += 1f;
            //facingLeft = false;

            switchCharacters();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos.y += 1f;

            switchCharacters();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos.y -= 1f;

            switchCharacters();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            drumstick.SetActive(!drumstick.activeSelf);
            me.SetActive(!me.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            canSwitchCharacters = !canSwitchCharacters;
        }

        if (doorScript.currentLevel > 12 && doorScript.currentLevel < 16) {
            canSwitchCharacters = true;
        } else {
            canSwitchCharacters = false;
        }

        transform.position = pos;
        drumstick.transform.localPosition = pos; 

	}

    void switchCharacters() {
        if (canSwitchCharacters) {
            drumstick.SetActive(!drumstick.activeSelf);
            me.SetActive(!me.activeSelf);
        }
    }
}

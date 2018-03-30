using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youController : MonoBehaviour {

    public float speed;
    public float rotZ;

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
        pos.z = -0.15f;
        //currentSquarePos = 0;
        transform.position = pos;

        drumstick.transform.localPosition = pos; 
        drumstick.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

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

    void switchCharacters() {
        if (canSwitchCharacters) {
            drumstick.SetActive(!drumstick.activeSelf);
            me.SetActive(!me.activeSelf);
        }
    }
}

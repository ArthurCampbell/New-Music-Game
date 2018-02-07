using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youController : MonoBehaviour {

    public float speed;
    bool facingLeft;

    public doorController doorScript;

    //public int currentSquarePos;

	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        Vector3 pos = transform.position;
        //We start one to the left of the instruction square
        pos.x = -6f;
        pos.y = 1f;
        //currentSquarePos = 0;
        transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            pos.x -= 1f;
            facingLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos.x += 1f;
            facingLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos.y += 1f;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos.y -= 1f;
        }

        transform.position = pos;

        SpriteRenderer meSprite = GetComponent<SpriteRenderer>();

        if (facingLeft){
            //meSprite.flipX = true;
    
        } else {
            //meSprite.flipX = false;
        }
	}
}

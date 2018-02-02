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
        pos.x = doorScript.squareX[0] - 4f;
        pos.y = doorScript.squareY[0];
        //currentSquarePos = 0;
        transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.A)) {
            //transform.position += new Vector3(-1f, 0f, 0f) * Time.deltaTime * speed;
            pos.x -= 1f;
            facingLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * speed;
            pos.x += 1f;
            facingLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //transform.position += new Vector3(0f, 1f, 0f) * Time.deltaTime * speed;
            pos.y += 1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //transform.position += new Vector3(0f, -1f, 0f) * Time.deltaTime * speed;
            pos.y -= 1f;
        }

        transform.position = pos;

        SpriteRenderer meSprite = GetComponent<SpriteRenderer>();

        if (facingLeft){
            meSprite.flipX = true;
    
        } else {
            meSprite.flipX = false;
        }
	}
}

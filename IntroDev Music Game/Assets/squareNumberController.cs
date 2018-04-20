using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareNumberController : MonoBehaviour {

    public SpriteRenderer mySpriteRenderer;

    public doorController doorScript;

    public int lastBeat;

	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (doorScript.currentBeat != lastBeat) {
            if (mySpriteRenderer.color.r == 1) {
                mySpriteRenderer.color = new Color(0f, 0f, 0f, 1f);
            } else {
                mySpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        lastBeat = doorScript.currentBeat;
	}
}

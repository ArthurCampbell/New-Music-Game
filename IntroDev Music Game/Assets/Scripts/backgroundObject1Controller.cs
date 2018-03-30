using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundObject1Controller : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public float rotation;

    //public int[] myLevels;
    public bool dontDestroyUs;

    doorController doorScript;

    SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        dontDestroyUs = true;

        reset();
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        pos.x += direction.x * speed * Time.deltaTime;
        pos.y += direction.y * speed * Time.deltaTime;

        if (rotation < 360f) {
            rotation += 10f * Time.deltaTime;
        } else {
            rotation = 0f;
        }

        rot = Quaternion.Euler(0f, rotation, 0f);

        transform.position = pos;
        transform.rotation = rot;


        if (pos.x > 8 || pos.x < -7 || pos.y > 7 || pos.y < -5) {
            reset();
        }

        if (doorScript.readyForCameraSwitch == false) {
            dontDestroyUs = false;
        }

        if (doorScript.readyForCameraSwitch && dontDestroyUs == false) {
            Destroy(gameObject);
        }
	}

    void reset() {
        direction = new Vector3(0, 0, 5);

        Color backgroundColor = doorScript.CurrentColorPalette[4];
        float ourColorH, ourColorS, ourColorV;
        Color.RGBToHSV(backgroundColor, out ourColorH, out ourColorS, out ourColorV);
        Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
        ourColorV += .031f;
        ourColorS -= .004f;
        Debug.Log(ourColorH + ", " + ourColorS + ", " + ourColorV);
        Color ourColor = Color.HSVToRGB(ourColorH, ourColorS, ourColorV);
        mySpriteRenderer.color = ourColor;
        Debug.Log(ourColor);

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

        //Go towards the center
        //Randomize that so it's center ish
        //Randomize our shape
        //Randomize our speed

        direction = direction.normalized;

        Vector3 resetPos = transform.position;

        resetPos.x = x;
        resetPos.y = y;

        transform.position = resetPos;

        //Randomize our rotation
        Quaternion rot = transform.rotation;

        float rotY = Random.Range(-180f, 180f);
        Debug.Log("rotY " + rotY);

        rot = Quaternion.Euler(0, rotY, 0);

        transform.rotation = rot;
    }
}

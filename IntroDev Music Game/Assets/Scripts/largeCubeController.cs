using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class largeCubeController : MonoBehaviour {

    public float rotX;
    public float rotY;
    public float rotZ;

    public float speed;

    public int levelISpawnedOn;

    public doorController doorScript;
	// Use this for initialization
	void Start () {
        Vector3 pos = transform.position;

        pos.x = 0.5f;
        pos.y = 1f;
        pos.z = 20f;

        transform.position = pos;

        Quaternion rot = transform.rotation;

        rotX = -45f;
        rotY = 45f;
        rotZ = 0f;

        rot = Quaternion.Euler(rotX, rotY, rotZ);

        transform.rotation = rot;

        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        levelISpawnedOn = doorScript.currentLevel;
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion rot = transform.rotation;

        rotY += speed * Time.deltaTime;

        rot = Quaternion.Euler(rotX, rotY, rotZ);

        transform.rotation = rot;

        if (doorScript.currentLevel != levelISpawnedOn) {
            Destroy(gameObject);
        }
	}
}

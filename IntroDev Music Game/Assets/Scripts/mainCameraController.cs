using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraController : MonoBehaviour {

    public doorController doorScript;

    public Camera me;

	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        me = GetComponent<Camera>();
        //me.orthographic = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.G))
        {
            pos.x -= 3f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.J))
        {
            pos.x += 3f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            pos.y += 3f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.H))
        {
            pos.y -= 3f * Time.deltaTime;

        }

        if (doorScript.readyForCameraSwitch) {
            Vector2 center = new Vector3(0.5f, 1f);
            Vector2 directionToCenter = new Vector2((center.x - transform.position.x), 
                                                    (center.y - transform.position.y)).normalized;
            Vector2 distanceToCenter = new Vector2((center.x - transform.position.x),
                                                    (center.y - transform.position.y));

            if (Mathf.Abs(distanceToCenter.x) < 0.5f && Mathf.Abs(distanceToCenter.y) < 0.5f)
            {
                doorScript.fakeRoomCamera.targetTexture = doorScript.fakeRoomTexture;
                doorScript.readyForCameraSwitch = false;
                pos = new Vector3(0.5f, 1f, -10f);
            } else {
                pos.x += directionToCenter.x * 10 * Time.deltaTime;
                pos.y += directionToCenter.y * 10 * Time.deltaTime;
            }
        }

        transform.position = pos;
	}
}

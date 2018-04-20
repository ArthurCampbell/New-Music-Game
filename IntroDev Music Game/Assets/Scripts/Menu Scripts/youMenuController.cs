using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youMenuController : MonoBehaviour
{

    public float speed;
    public float rotZ;

    GameObject me;

    // Use this for initialization
    void Start()
    {
        me = gameObject;

        Vector3 pos = transform.position;
        //We start one to the left of the instruction square
        pos.x = -6f;
        pos.y = 1f;
        pos.z = -0.15f;
        //currentSquarePos = 0;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;


        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos.x -= 1f;
            rotZ = 90;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos.x += 1f;
            rotZ = -90;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos.y += 1f;
            rotZ = 0;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos.y -= 1f;
            rotZ = 180;
        }

        Vector3 rot = new Vector3(0f, 0f, rotZ);

        transform.rotation = Quaternion.Euler(rot);

        transform.position = pos;
    }

}


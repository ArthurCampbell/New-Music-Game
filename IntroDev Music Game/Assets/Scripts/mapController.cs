using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour {

    public doorController doorScript;

    public int[,] mapArray = new int[10, 10];

    /* I HAVE WRITTEN DOWN THE CURRENT MAP IN A SPREADSHEET
     * REMEMBER TO CHANGE IT WHENEVER YOU CHANGE THE CODE HERE PLEASE */


	// Use this for initialization
	void Start () {
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        for (int x = 0; x < Mathf.Sqrt(mapArray.Length); x++) {
            for (int n = 0; n < Mathf.Sqrt(mapArray.Length); n++) {
                mapArray[x, n] = 0;
            }
        }

        /*
        //Levels 1-13 in straight line from [0, 9] to [14, 9]
        for (int x = 0; x < 7; x++){
            mapArray[x + 1, 2] = x + 7;
        }
        */

        /*
        mapArray[6, 1] = 16;
        mapArray[6, 0] = 17;

        mapArray[6, 3] = 15;
        mapArray[7, 3] = 14;

        mapArray[0, 3] = 21;
        mapArray[1, 3] = 1;
        mapArray[2, 3] = 2;
        mapArray[3, 3] = 3;

        mapArray[1, 4] = 6;
        mapArray[2, 4] = 5;
        mapArray[3, 4] = 4;
        */

        mapArray[1, 1] = 15;
        mapArray[1, 2] = 14;
        mapArray[1, 3] = 10;
        mapArray[1, 4] = 11;
        mapArray[1, 5] = 12;

        mapArray[2, 2] = 1;
        mapArray[2, 3] = 9;
        mapArray[2, 5] = 13;

        mapArray[3, 2] = 2;
        mapArray[3, 3] = 8;

        mapArray[4, 2] = 3;
        mapArray[4, 3] = 4;
        mapArray[4, 4] = 5;

        mapArray[5, 3] = 7;
        mapArray[5, 4] = 6;

	}
	
    public Vector2Int findNextRoom(string directionToGoTo) {
        if (directionToGoTo == "LEFT") {
            int spotToMoveTo = doorScript.mapPosition[0] - 1;
            if (spotToMoveTo >= 0 && spotToMoveTo < 20) {
                return new Vector2Int(spotToMoveTo, doorScript.mapPosition[1]);
            } else {
                return new Vector2Int(0, 0);
            }
        } else if (directionToGoTo == "RIGHT")
        {
            int spotToMoveTo = doorScript.mapPosition[0] + 1;

            if (spotToMoveTo >= 0 && spotToMoveTo < 20)
            {
                return new Vector2Int(spotToMoveTo, doorScript.mapPosition[1]);
            }
            else
            {
                return new Vector2Int(0, 0);
            }
        } else if (directionToGoTo == "UP")
        {
            int spotToMoveTo = doorScript.mapPosition[1] - 1;
            if (spotToMoveTo >= 0 && spotToMoveTo < 20)
            {
                return new Vector2Int(doorScript.mapPosition[0], spotToMoveTo);
            }
            else
            {
                return new Vector2Int(0, 0);
            }
        } else if (directionToGoTo == "DOWN")
        {
            int spotToMoveTo = doorScript.mapPosition[1] + 1;
            if (spotToMoveTo >= 0 && spotToMoveTo < 20)
            {
                return new Vector2Int(doorScript.mapPosition[0], spotToMoveTo);
            }
            else
            {
                return new Vector2Int(0, 0);
            }
        }
        else
        {
            return new Vector2Int(0, 0);
        }
    }

	// Update is called once per frame
	void Update () {
        
	}
}

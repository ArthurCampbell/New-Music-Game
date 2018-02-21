using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour {

    public doorController doorScript;

    public int[,] mapArray = new int[20, 20];

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

        //Levels 1-15 in straight line from [0, 9] to [14, 9]
        for (int x = 0; x < 15; x++){
            mapArray[x, 9] = x + 1;
        }

        mapArray[11, 8] = 16;
        mapArray[11, 7] = 17;

        mapArray[11, 10] = 15;
        mapArray[12, 10] = 14;

        //Debug.Log(mapArray);
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

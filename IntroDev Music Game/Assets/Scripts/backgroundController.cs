using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundController : MonoBehaviour {

    AudioSource myAudioSource;
    public AudioClip backgroundMusic;

    doorController doorScript;

    public bool backgroundObject1Spawned;
    public bool backgroundSquaresSpawned;
    public bool largeCubeSpawned;

    public int objectsToSpawn;

    public GameObject backgroundObject1;
    public GameObject backgroundSquare;
    public GameObject largeCube;

    public int[] flatCapsuleLevels;
    public int[] backgroundSquareLevels;

    public backgroundObject1Controller backgroundObjectScript;
    public backgroundObject1Controller backgroundObjectSquareScript;

    // Use this for initialization
    void Start () {
        //play background music at half volume, looped
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = backgroundMusic;
        myAudioSource.loop = true;
        myAudioSource.volume = 0.5f;

        //Don't play just yet
        //myAudioSource.Play();

        //Get door script
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        backgroundObjectScript = backgroundObject1.GetComponent<backgroundObject1Controller>();
        backgroundObjectSquareScript = backgroundSquare.GetComponent<backgroundObject1Controller>();

        //spawn a thingy
        for (int x = 0; x < 4; x++)
        {
            Debug.Log("woo spawning");
            Instantiate(backgroundObject1, new Vector3(-20f, 20f, 20f), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        //Code for specific levels
        if (doorScript.readyForCameraSwitch)
        {
            if (doorScript.currentLevel == 1)
            {
                if (objectsToSpawn > 0) {
                    Instantiate(backgroundObject1, new Vector3(-20f, 20f, 20f), Quaternion.identity);
                    objectsToSpawn--;
                }
                /*
                if (backgroundObject1Spawned == false)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        Debug.Log("woo spawning");
                        Instantiate(backgroundObject1, new Vector3(-20f, 20f, 20f), Quaternion.identity);
                    }
                    backgroundObject1Spawned = true;
                }*/
            }
            if (doorScript.currentLevel == 4) {
                if (backgroundSquaresSpawned == false)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Instantiate(backgroundSquare, new Vector3(-20f, 20f, 15f), Quaternion.identity);
                    }
                    backgroundSquaresSpawned = true;
                }
            }
            if (doorScript.currentLevel == 6)
            {
                if (largeCubeSpawned == false) {
                    Instantiate(largeCube, transform.position, Quaternion.identity);
                    largeCubeSpawned = true;
                }
            } else {
                largeCubeSpawned = false;

            }
        } else {
            objectsToSpawn = 4;
        }
	}
}

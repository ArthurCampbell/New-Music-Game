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
    public int capsulesToSpawn;
    public int squaresToSpawn;
    public int circlesToSpawn;

    public GameObject backgroundObject1;
    public GameObject backgroundSquare;
    public GameObject backgroundCircle;
    public GameObject largeCube;

    public int[] flatCapsuleLevels;
    public int[] backgroundSquareLevels;

    public backgroundObject1Controller backgroundObjectScript;
    public backgroundObject1Controller backgroundObjectSquareScript;
    public backgroundObject1Controller backgroundCircleScript;

    // Use this for initialization
    void Start () {
        //play background music at half volume, looped
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = backgroundMusic;
        myAudioSource.loop = true;
        myAudioSource.volume = 0.15f;

        //Don't play just yet
        myAudioSource.Play();

        //Get door script
        doorScript = GameObject.FindWithTag("Door").GetComponent<doorController>();

        backgroundObjectScript = backgroundObject1.GetComponent<backgroundObject1Controller>();
        backgroundObjectSquareScript = backgroundSquare.GetComponent<backgroundObject1Controller>();
        backgroundCircleScript = backgroundCircle.GetComponent<backgroundObject1Controller>();


        spawn(6, 8, 8, true, true, false);
	}
	
	// Update is called once per frame
	void Update () {
        
        //Code for specific levels
        if (doorScript.readyForCameraSwitch)
        {
            if (doorScript.currentLevel == 1)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 2)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }

            if (doorScript.currentLevel == 3)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 4)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 5)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 6)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 7)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 8)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
        } else {
            objectsToSpawn = 4;
            backgroundObject1Spawned = false;
        }
	}

    public void spawn(int capsulesToSpawn, int squaresToSpawn, int circlesToSpawn, bool capsuleMode, bool squareMode, bool circleMode) {
        for (int x = 0; x < capsulesToSpawn; x++) {
            backgroundObjectScript.backgroundMode = capsuleMode;
            Instantiate(backgroundObject1, new Vector3(-20f, 20f, 20f), Quaternion.identity);
        }

        for (int x = 0; x < squaresToSpawn; x++)
        {
            backgroundObjectSquareScript.backgroundMode = squareMode;
            Instantiate(backgroundSquare, new Vector3(-20f, 20f, 20f), Quaternion.identity);
        }

        for (int x = 0; x < circlesToSpawn; x++)
        {
            backgroundCircleScript.backgroundMode = circleMode;
            Instantiate(backgroundCircle, new Vector3(-20f, 20f, 20f), Quaternion.identity);
        }
    }
}

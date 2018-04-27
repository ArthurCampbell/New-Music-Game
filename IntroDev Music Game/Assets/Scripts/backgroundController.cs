using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundController : MonoBehaviour {

    //AudioSource myAudioSource;
    public AudioClip backgroundMusic;
    public AudioClip backgroundBass;
    public AudioClip backgroundBassDrum;
    public AudioClip backgroundSnare;
    public AudioClip backgroundClick;
    public AudioClip backgroundMelody;

    AudioSource backgroundBassSource;
    AudioSource backgroundBassDrumSource;
    AudioSource backgroundSnareSource;
    AudioSource backgroundClickSource;
    AudioSource backgroundMelodySource;

    public doorController doorScript;

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

    public float bassVolume;
    public float bassDrumVolume;
    public float snareVolume;
    public float clickVolume;
    public float melodyVolume;

    public int[] flatCapsuleLevels;
    public int[] backgroundSquareLevels;

    public backgroundObject1Controller backgroundObjectScript;
    public backgroundObject1Controller backgroundObjectSquareScript;
    public backgroundObject1Controller backgroundCircleScript;

    // Use this for initialization
    void Start () {
        /*
        //play background music at half volume, looped
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = backgroundMusic;
        myAudioSource.loop = true;
        myAudioSource.volume = 0.15f;

        //Don't play just yet
        myAudioSource.Play();
        */

        AudioSource[] audioSources = GetComponents<AudioSource>();

        //play background music at half volume, looped
        backgroundBassSource = audioSources[0];
        backgroundBassSource.clip = backgroundBass;
        backgroundBassSource.loop = true;
        backgroundBassSource.volume = 0f;

        backgroundBassDrumSource = audioSources[1];
        backgroundBassDrumSource.clip = backgroundBassDrum;
        backgroundBassDrumSource.loop = true;
        backgroundBassDrumSource.volume = 00f;

        backgroundClickSource = audioSources[2];
        backgroundClickSource.clip = backgroundClick;
        backgroundClickSource.loop = true;
        backgroundClickSource.volume = 0f;

        backgroundSnareSource = audioSources[3];
        backgroundSnareSource.clip = backgroundSnare;
        backgroundSnareSource.loop = true;
        backgroundSnareSource.volume = 0f;

        backgroundMelodySource = audioSources[4];
        backgroundMelodySource.clip = backgroundMelody;
        backgroundMelodySource.loop = true;
        backgroundMelodySource.volume = 0f;

        backgroundBassSource.Play();
        backgroundSnareSource.Play();
        backgroundBassDrumSource.Play();
        backgroundClickSource.Play();
        backgroundMelodySource.Play();


        backgroundBassSource.volume = bassVolume;
        /*
        backgroundBassSource.volume = bassVolume;
        backgroundSnareSource.volume = snareVolume;
        backgroundClickSource.volume = clickVolume;
        backgroundBassDrumSource.volume = bassDrumVolume;
        backgroundMelodySource.volume = melodyVolume;
        */

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
                    spawn(3, 6, 6, true, true, false);
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
                backgroundSnareSource.volume = snareVolume;

                if (backgroundObject1Spawned == false)
                {
                    spawn(9, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 4)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(12, 10, 10, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 5)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(10, 8, 8, false, true, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 6)
            {
                

                if (backgroundObject1Spawned == false)
                {
                    spawn(8, 10, 8, true, false, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 7)
            {

                backgroundBassDrumSource.volume = bassDrumVolume;
                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 12, 6, true, false, true);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 8)
            {

                if (backgroundObject1Spawned == false)
                {
                    spawn(12, 12, 8, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 9)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(15, 15, 4, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 10)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(20, 20, 2, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 11)
            {
                backgroundClickSource.volume = clickVolume;

                if (backgroundObject1Spawned == false)
                {
                    spawn(2, 2, 10, false, false, true);
                    spawn(20, 20, 2, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 12)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(4, 4, 15, false, false, true);
                    spawn(20, 20, 4, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 13)
            {

                if (backgroundObject1Spawned == false)
                {
                    spawn(6, 6, 20, false, false, true);
                    spawn(20, 20, 6, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 14)
            {
                backgroundMelodySource.volume = melodyVolume;

                if (backgroundObject1Spawned == false)
                {
                    spawn(8, 8, 8, false, false, true);
                    spawn(25, 25, 25, true, true, false);
                    backgroundObject1Spawned = true;
                }
            }
            if (doorScript.currentLevel == 15)
            {
                if (backgroundObject1Spawned == false)
                {
                    spawn(30, 30, 30, true, true, true);
                    spawn(10, 10, 10, false, false, false); 
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

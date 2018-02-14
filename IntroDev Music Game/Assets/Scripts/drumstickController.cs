using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drumstickController : MonoBehaviour {

    GameObject player;
    youController playerScript;
    GameObject me;

    public AudioClip mySound;
    AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<youController>();
        me = gameObject;

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = mySound;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos.x -= 1f;

            switchCharacters();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos.x += 1f;

            switchCharacters();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos.y += 1f;

            switchCharacters();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos.y -= 1f;

            switchCharacters();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            player.SetActive(!player.activeSelf);
            me.SetActive(!me.activeSelf);
        }


        transform.position = pos;
        player.transform.localPosition = pos;
	}

    void switchCharacters() {
        if (playerScript.canSwitchCharacters) {
            player.SetActive(!player.activeSelf);
            me.SetActive(!me.activeSelf);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Square") {
            myAudioSource.Play();
        }

        if (coll.gameObject.tag == "Level Selector") {
            player.SetActive(!player.activeSelf);
            me.SetActive(!me.activeSelf);
        }

        if (coll.gameObject.tag == "Door") {
            player.SetActive(!player.activeSelf);
            me.SetActive(!me.activeSelf);
        }
    }
}

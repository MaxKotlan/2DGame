﻿using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    Chunk chunk;
    bool x, y, z;
    // Use this for initialization
    void Start () {
        chunk = GetComponent<Chunk>();
        y = false;
        x = false;
        z = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            y = true;
            x = false;
            z = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            y = false;
            x = true;
            z = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            y = false;
            x = false;
            z = true;
        }

        if (x == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                chunk.scrollx++;
                chunk.Regenerate();
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                chunk.scrollx--;
                chunk.Regenerate();
            }
        }
        if (y == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                chunk.scrolly++;
                chunk.Regenerate();
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                chunk.scrolly--;
                chunk.Regenerate();
            }
        }
        if (z == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                chunk.scrollz++;
                chunk.Regenerate();
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                chunk.scrollz--;
                chunk.Regenerate();
            }
        }
    }
}

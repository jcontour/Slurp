﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class make_grid : MonoBehaviour
{
    public GameObject gridTile;
    public GameObject player;

    bool rotating;
    int dir;
    float timer;
    Quaternion currR;
    float speed = 5;

    public int numRows = 20;
    public int numColumns = 20;

    public bool gridCreated = false;

    GameObject rotateMe;

    // Start is called before the first frame update
    void Start()
    {
        if (!gridCreated)
        {
            Color t = Random.ColorHSV(0f, 1f, 1f, 1f, 0.7f, 1f);
            Color m = Random.ColorHSV(0f, 1f, 1f, 1f, 0.7f, 1f);
            Color b = Random.ColorHSV(0f, 1f, 1f, 1f, 0.7f, 1f);

            float seed = Random.Range(0, 100f); // make a random level every time 
            for (float i = 0; i < numRows; i++)
            {
                for (float j = 0; j < numColumns; j++)
                {
                    float freq = 3f; // how dense is map
                    float levels = 5; // how many vertical levels in map
                    float yPos = Mathf.RoundToInt((Mathf.PerlinNoise(((i + seed) / numRows) * freq, ((j + seed) / numColumns) * freq) - 0.5f) * levels);
                    GameObject g = Instantiate(gridTile, new Vector3(i, yPos + 2, j), Quaternion.identity);
                    g.transform.parent = gameObject.transform;
                    g.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_ColorTop", t);
                    g.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_ColorMid", m);
                    g.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_ColorBot", b);
                }
            }

            gridCreated = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            timer += Time.deltaTime * speed;
            if (timer < 2f)
            {
                RotateCamera(currR, dir, timer);
            }
            else
            {
                this.transform.parent = null;
                Destroy(rotateMe);
                rotating = false;
                timer = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                
                GetReadyToRotate();
                currR = rotateMe.transform.rotation;
                dir = -1;
                rotating = true;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                GetReadyToRotate();
                currR = rotateMe.transform.rotation;
                dir = 1;
                rotating = true;
            }
        }
    }

    void RotateCamera(Quaternion oldR, int dir, float timer)
    {
        Vector3 rot = oldR.eulerAngles;
        Quaternion newR = Quaternion.Euler(rot.x, rot.y + 90 * dir, rot.z);
        rotateMe.transform.rotation = Quaternion.Slerp(oldR, newR, timer);
    }

    void GetReadyToRotate()
    {
        rotateMe = new GameObject();
        rotateMe.name = "rotateme";
        rotateMe.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        this.transform.parent = rotateMe.transform;

    }
}

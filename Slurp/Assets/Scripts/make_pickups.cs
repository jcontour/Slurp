﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class make_pickups : MonoBehaviour
{

    public GameObject e;
    Vector3 dir;
    Vector3[] dirs = new Vector3[] { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
    int whichDir;

    int maxX;
    int maxZ;
    public int numEnemies;

    Vector3 startPos;
    Vector3[] startPosOptions;

    public Mesh[] meshOptions;

    // Start is called before the first frame update
    void Start()
    {
        maxX = GetComponent<make_grid>().numRows;
        maxZ = GetComponent<make_grid>().numColumns;

        for (int i = 0; i < numEnemies; i++)
        {
            CreateEnemy(Random.Range(0,12));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy(int whichMesh)
    {
        startPosOptions = new Vector3[]
        {
            new Vector3(Random.Range(0,maxX),5,0),
            new Vector3(Random.Range(0,maxX),5,maxZ-1),
            new Vector3(0,5,Random.Range(0,maxZ)),
            new Vector3(maxX-1,5,Random.Range(0,maxZ))
        };

        whichDir = Random.Range(0,4);

        startPos = startPosOptions[whichDir];
        dir = dirs[whichDir];

        GameObject doot = Instantiate(e, startPos, Quaternion.identity);
        doot.GetComponent<straight_move>().dir = dir;
        doot.GetComponentInChildren<MeshFilter>().mesh = meshOptions[whichMesh];
        doot.GetComponent<get_collected>().meshNum = whichMesh;
    }
}

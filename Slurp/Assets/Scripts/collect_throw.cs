using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect_throw : MonoBehaviour
{
    public Mesh[] meshOptions;
    public int whichMesh;
    public GameObject projectile;

    public bool hasCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Throw(int power, Vector3 startPos, Vector3 dir)
    {
        Debug.Log(power + " " + startPos + " " + dir);
        GameObject g = Instantiate(projectile, startPos + dir + Vector3.up, Quaternion.identity);
        g.GetComponent<MeshFilter>().mesh = meshOptions[whichMesh];
        hasCollected = false;
    }

    public void Collect(GameObject g, int meshNum)
    {
        Debug.Log(g.name);
        whichMesh = meshNum;
        hasCollected = true;
    }

    void Move_Throw(GameObject g, Mesh m, Vector3 start, Vector3 end)
    {

    }
}

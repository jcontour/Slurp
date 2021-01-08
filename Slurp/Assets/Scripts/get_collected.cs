using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_collected : MonoBehaviour
{

    GameObject player;
    public int meshNum;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1)
        {
            player.GetComponent<collect_throw>().Collect(this.gameObject, meshNum);
            Destroy(this.gameObject);
        }
    }
}

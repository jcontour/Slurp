using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_death : MonoBehaviour
{

    float timer;
    public float death_time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= death_time)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bbeg_movement : MonoBehaviour {

	public float Timer = 2f;
	public GameObject target;
	float attraction = 0;
	int minX, maxX, minZ, maxZ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime;
		if (Timer <= 0) 
		{
			transform.position = transform.position + moveTo(target.transform.position, transform.position); 
			Timer = 2f;
		}

	}

	Vector3Int moveTo(Vector3 end, Vector3 start){
		Vector3 dist = end - start;
		int amount = Mathf.RoundToInt(dist.magnitude);
		if (amount > 5) { attraction = .5f; } else { attraction = 1; }
		if (dist.x < 0 || dist.z < 0) {
			minX = Mathf.RoundToInt(dist.x * attraction);
			maxX = 0;
			minZ = Mathf.RoundToInt(dist.z * attraction);
			maxZ = 0;
		} else {
			minX = Mathf.RoundToInt(dist.x * attraction);
			maxX = 0;
			minZ = Mathf.RoundToInt(dist.z * attraction);
			maxZ = 0;
		}

		return new Vector3Int (Random.Range (minX, maxX), 0, Random.Range (minZ, maxZ));

	}
		
}

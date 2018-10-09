using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bbeg_movement : MonoBehaviour {

	public float Timer = 0f;
	public GameObject target;
	float attraction = 0;
	int minX, maxX, minZ, maxZ;
	Vector3 bbegStart, bbegEnd;

	void Start () {
		bbegStart = transform.position;
		bbegEnd = transform.position;
	}
	
	void Update () {

		Timer += Time.deltaTime;

		if (Timer >= 2) {
			bbegStart = transform.position;
			bbegEnd = moveTo (transform.position, target.transform.position);
			Timer = 0f;
		} 

		jumpBtwnPoints (bbegStart, bbegEnd, Timer);

	}

	Vector3Int moveTo(Vector3 start, Vector3 end){
		Vector3 dist = end - start;
		int amount = Mathf.RoundToInt(dist.magnitude);
		if (amount > 5) { attraction = .5f; } else { attraction = 1; }

		if (Mathf.RoundToInt(dist.x) < 0) {
			minX = Mathf.RoundToInt(dist.x * attraction);
			maxX = 0;
		} else {
			minX = 0;
			maxX = Mathf.RoundToInt(dist.x * attraction);
		}

		if (Mathf.RoundToInt(dist.z) < 0) {
			minZ = Mathf.RoundToInt(dist.z * attraction);
			maxZ = 0;
		} else {
			minZ = 0;
			maxZ = Mathf.RoundToInt(dist.z * attraction);
		}

		Vector3Int movingTo = new Vector3Int (Mathf.RoundToInt(start.x + Random.Range (minX, maxX)), 1, Mathf.RoundToInt(start.z + Random.Range (minZ, maxZ)));
		return movingTo;
	}

	void jumpBtwnPoints (Vector3 start, Vector3 end, float timer){

		Vector3 peakPoint = new Vector3(start.x+(end.x - start.x) /2, 5, start.z+(end.z - start.z) /2);
		Vector3 m1 = Vector3.Lerp (start, peakPoint, timer);
		Vector3 m2 = Vector3.Lerp (peakPoint, end, timer);
		transform.position = Vector3.Lerp (m1, m2, timer);

	}
		
}

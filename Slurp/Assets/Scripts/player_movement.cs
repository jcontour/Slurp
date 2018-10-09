using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour {

	Vector3 startPos, endPos;
	float Timer = 0;
	bool doMove = false;
	public float speed = 10;

	void Start () {
		startPos = transform.position;
		endPos = transform.position;
	}
	
	void Update () {
		if (doMove) {
			Timer += Time.deltaTime * speed;
			if (Timer < 1.5f) {
				movePlayer (startPos, endPos, Timer);
			} else {
				doMove = false;
				Timer = 0;
			}

		} else {
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				startPos = transform.position;
				endPos = new Vector3 ((transform.position.x - 1f), transform.position.y, transform.position.z);
				doMove = true;
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				startPos = transform.position;
				endPos = new Vector3 ((transform.position.x + 1f), transform.position.y, transform.position.z);
				doMove = true;
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				startPos = transform.position;
				endPos = new Vector3 (transform.position.x, transform.position.y, (transform.position.z + 1f));
				doMove = true;
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				startPos = transform.position;
				endPos = new Vector3 (transform.position.x, transform.position.y, (transform.position.z - 1f));
				doMove = true;
			}
		}
	}

	void movePlayer (Vector3 start, Vector3 end, float timer){
			Vector3 peakPoint = new Vector3(start.x+(end.x - start.x) /2, 1.5f, start.z+(end.z - start.z) /2);
			Vector3 m1 = Vector3.Lerp (start, peakPoint, timer);
			Vector3 m2 = Vector3.Lerp (peakPoint, end, timer);
			transform.position = Vector3.Lerp (m1, m2, timer);
	}

}


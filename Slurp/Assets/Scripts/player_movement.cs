using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour {

	Vector3 startPos, endPos;
	float Timer = 0;
	float timerval = 1.5f;
	bool doMove = false;
	Vector3 dir;
	public float speed = 10;
	float peak;

	public LayerMask gridLayer;
	GameObject playerMesh;
	Animator anim;

	bool playerPlaced = false;

	void Start () {
		playerMesh = transform.GetChild(0).gameObject;
		anim = playerMesh.GetComponent<Animator>();
		peak = startPos.y + 0.5f;
	}
	void Update () {
		if (!playerPlaced)
		{
			startPos = GetStartPos();
			transform.position = startPos;
			endPos = startPos;
			playerPlaced = true;
		}
		else
		{
			if (doMove)
			{
				Timer += Time.deltaTime * speed;
				if (Timer < timerval)
				{
					movePlayer(startPos, endPos, peak, Timer);
				}
				else
				{
					transform.position = endPos;
					doMove = false;
					Timer = 0;
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
				{
					startPos = transform.position;
					dir = Vector3.left;
					checkCanMove(startPos, dir);
				}
				if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
				{
					startPos = transform.position;
					dir = Vector3.right;
					checkCanMove(startPos, dir);

				}
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
				{
					startPos = transform.position;
					dir = Vector3.forward;
					checkCanMove(startPos, dir);

				}
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
				{
					startPos = transform.position;
					dir = Vector3.back;
					checkCanMove(startPos, dir);

				}
			}
		}
	}

    void checkCanMove(Vector3 start, Vector3 dir)
    {
		endPos = start + dir;
		Debug.Log("start " + start);
		
		RaycastHit hit;
		if (Physics.Raycast(new Vector3(endPos.x, 10, endPos.z), Vector3.down, out hit, 10f, gridLayer) == true)
		{
			endPos.y = hit.point.y;
			Debug.Log("end " + endPos);
			
			if (start.y > endPos.y)
			{
				peak = start.y + 1.5f;
			} else if (start.y < endPos.y)
            {
				peak = start.y + 2.5f;
            } else
            {
				peak = start.y + 0.5f;
            }
			Debug.Log("peak " + peak);
			doMove = true;

		} else
        {
			peak = start.y;
			endPos = start;
			anim.SetTrigger("dontMoveX");
			doMove = false;
		}
	}

	void movePlayer (Vector3 start, Vector3 end, float peak, float timer){
        Vector3 peakPoint = new Vector3(start.x + (end.x - start.x) / 2, peak, start.z + (end.z - start.z) / 2);
        Vector3 m1 = Vector3.Lerp(start, peakPoint, timer/timerval);
        Vector3 m2 = Vector3.Lerp(peakPoint, end, timer/timerval);
        transform.position = Vector3.Lerp(m1, m2, timer/timerval);
    }

	Vector3 GetStartPos()
    {
		RaycastHit hit;
		float yPos;
		if (Physics.Raycast(new Vector3(0,10,19), Vector3.down, out hit, 20f, gridLayer))
        {
			yPos = hit.point.y;
			return new Vector3(0, yPos, 19);
		} else
        {
			return new Vector3(0, 4, 19);
		}
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour {

	Vector3 startPos, endPos;
	float Timer = 0;
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
				if (Timer < 2f)
				{
					movePlayer(startPos, endPos, peak, Timer);
				}
				else
				{
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
        RaycastHit hit;
		// MOVING
		// check if something is in front of you
		if (Physics.Raycast(transform.position, dir, out hit, 1f, gridLayer) == true)
		{
			//
			// if there's something in front of you and NOT something on top of that, jump on it.
			if (Physics.Raycast(transform.position + Vector3.up, dir, out hit, 1f, gridLayer) == false)
			{
				endPos = startPos + dir + Vector3.up;
				peak = startPos.y + 2.5f;
				doMove = true;
			}
			else // if there's more than one thing stacked in front of you, feebly jump like a ween
			{
				endPos = startPos;
				peak = startPos.y + 2f;
				doMove = true;
			}
		}
		else
		{
			// if there is flat ground in front of you, move to it
            if (Physics.Raycast(transform.position + dir, Vector3.down, out hit, 1f, gridLayer))
            {
                endPos = startPos + dir;
				peak = startPos.y + 0.5f;
				doMove = true;
			}
            else
            { // if there isn't ground but there is a lower layer, jump down
				if (Physics.Raycast(transform.position + dir + Vector3.down, Vector3.down, out hit, 1f, gridLayer))
                {
					endPos = startPos + dir + Vector3.down;
					peak = endPos.y + 1.5f;
					doMove = true;
				} else // if there's nothing directly in front or directly below, don't move
                {
					anim.SetTrigger("dontMoveX");
					doMove = false;
				}
			}
		}
       
    }

	void movePlayer (Vector3 start, Vector3 end, float peak, float timer){
        Vector3 peakPoint = new Vector3(start.x + (end.x - start.x) / 2, peak, start.z + (end.z - start.z) / 2);
        Vector3 m1 = Vector3.Lerp(start, peakPoint, timer);
        Vector3 m2 = Vector3.Lerp(peakPoint, end, timer);
        transform.position = Vector3.Lerp(m1, m2, timer);
    }

	Vector3 GetStartPos()
    {
		RaycastHit hit;
		float yPos;
		if (Physics.Raycast(new Vector3(0,10,19), Vector3.down, out hit, 20f, gridLayer))
        {
			yPos = hit.point.y + 0.5f;
			//Debug.Log(yPos);
			return new Vector3(0, yPos, 19);
		} else
        {
			return new Vector3(0, 4, 19);
		}
    }
}


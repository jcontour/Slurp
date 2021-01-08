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
	public float rotSpeed;
	float peak;
	public LayerMask gridLayer;
	public GameObject playerMesh;
	public GameObject playerMesh_pad;
	Animator anim;
	bool playerPlaced = false;
	public ParticleSystem p;
	public make_grid grid;

	float size = 1.2f;

	void Start () {
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
					movePlayer(startPos, endPos, peak, Timer, dir);
				}
				else
				{
					transform.position = endPos;
					//Instantiate(p, endPos, Quaternion.identity);
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
				if (Input.GetKeyDown(KeyCode.Space))
                {
					Slam();
                }
			}
		}
	}

	void Slam()
    {
		RaycastHit hit;
		if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down, out hit, 5f, gridLayer) == true)
        {
			GameObject g = hit.collider.gameObject;
			grid.SlamParticles(transform.position);
			Destroy(g);
			transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

			//size -= 0.1f;
			//playerMesh_pad.transform.localPosition = new Vector3(0, (size / 2) - 0.6f, 0);
			//playerMesh.transform.localScale = new Vector3(size, size, size);
			//Instantiate(p_slam, transform.position, Quaternion.identity);
				
			
		}
    }

    void checkCanMove(Vector3 start, Vector3 dir)
    {
		endPos = start + dir;
		
		RaycastHit hit;
		if (Physics.Raycast(new Vector3(endPos.x, 20, endPos.z), Vector3.down, out hit, 100f, gridLayer) == true)
		{
			endPos.y = hit.point.y;
			
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
			doMove = true;

		} else
        {
			peak = start.y;
			endPos = start;
			anim.SetTrigger("dontMoveX");
			doMove = false;
		}
	}

	void movePlayer (Vector3 start, Vector3 end, float peak, float timer, Vector3 dir){
        Vector3 peakPoint = new Vector3(start.x + (end.x - start.x) / 2, peak, start.z + (end.z - start.z) / 2);
        Vector3 m1 = Vector3.Lerp(start, peakPoint, timer/timerval);
        Vector3 m2 = Vector3.Lerp(peakPoint, end, timer/timerval);
        transform.position = Vector3.Lerp(m1, m2, timer/timerval);

		playerMesh.transform.RotateAround(transform.position, dir, rotSpeed * Time.deltaTime);
    }

	Vector3 GetStartPos()
    {
		RaycastHit hit;
		float yPos;
		if (Physics.Raycast(new Vector3(0,10,0), Vector3.down, out hit, 20f, gridLayer))
        {
			yPos = hit.point.y;
			return new Vector3(0, yPos, 0);
		} else
        {
			return new Vector3(0, 4, 0);
		}
    }
}


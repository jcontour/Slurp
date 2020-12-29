using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    public GameObject player;
    Vector3 startpos, endpos, playerpos;
    float peak;

    bool calculateNextMove = true;
    Vector3 playerDirection;
    float Timer;
    float timerval = 3;
    int speed = 3;
    float maxX;
    float maxZ;

    bool placed = false;
    public GameObject gridManager;
    public LayerMask gridLayer;

    // Start is called before the first frame update
    void Start()
    {
        int z = Random.Range(0, 20);
        int x = Random.Range(0, 20);
        
        startpos = new Vector3(x, 10, z);
        transform.position = startpos;
        endpos = startpos;
        peak = startpos.y + 2f;

        maxX = gridManager.GetComponent<make_grid>().numRows - 1;
        maxZ = gridManager.GetComponent<make_grid>().numColumns - 1;
    }

    // Update is called once per frame
    void Update()
    {
        // wait to place enemy until grid is created
        if (gridManager.GetComponent<make_grid>().gridCreated)
        {
            if (!placed) // if we aren't placed yet, get the start position and put us there.
            {
                startpos = GetStartPos();
                transform.position = startpos;
                endpos = startpos;
                placed = true;
            }
            else
            {   // once we are placed, find the direction to the player and calculate a endpos towards them.
                if (calculateNextMove) {
                    playerpos = player.transform.position;
                    playerDirection = playerpos - transform.position;
                    endpos = moveTowards(playerDirection);
                    calculateNextMove = false;
                } else 
                {   // after calculating move, move us.
                    Timer += Time.deltaTime * speed;
                    if (Timer < timerval)
                    {
                        moveEnemy(startpos, endpos, peak, Timer);
                    }
                    else
                    {
                        startpos = endpos;
                        Timer = 0;
                        calculateNextMove = true;
                    }
                }
            }
        }
    }

    Vector3 moveTowards(Vector3 dir)
    {
        //Debug.Log("dir " + dir);
        // make sure new x and z are within the set speed and not jumping off the map.
        float movex = Mathf.Round(Random.Range(0, dir.x));
        if (movex > speed) { movex = speed; }
        else if (movex < speed * -1) { movex = speed * -1; }
        float newXpos = Mathf.Round(transform.position.x + movex);
        if (newXpos > maxX) { newXpos = maxX; }
        else if (newXpos < 0) { newXpos = 0; }
        
        float movez = Mathf.Round(Random.Range(0, dir.z));
        if (movez > speed) { movez = speed; }
        else if (movez < speed * -1) { movez = speed * -1; }
        float newZpos = Mathf.Round(transform.position.z + movez);
        if (newZpos > maxZ) { newZpos = maxZ; }
        else if (newZpos < 0) { newZpos = 0; }

        // calculate the height of the jump, according to the total distance.
        float lengthOfJump = Vector2.Distance(new Vector2(0, 0), dir);
        peak = transform.position.y + 3f;
        //if (peak > transform.position.y + 3) { peak = transform.position.y + 3; }
        //peak = new Vector3(newXpos - (movex / 2), peaky, newZpos - (movez / 2));

        RaycastHit hit;
        float newYpos;

        if (Physics.Raycast(new Vector3(newXpos, 10, newZpos), Vector3.down, out hit, 20f, gridLayer))
        {
            newYpos = hit.point.y + 0.25f;
            //Debug.Log("moving to " + new Vector3(newXpos, newYpos, newZpos));
            return new Vector3(newXpos, newYpos, newZpos);
        }
        else
        {
            //Debug.Log("error placing enemy at X:"+ newXpos + " Z:" + newZpos);
            return new Vector3(newXpos, 10, newZpos);
        }

    }
    void moveEnemy(Vector3 start, Vector3 end, float peak, float timer)
    {
        //Debug.Log("moving...");
        Vector3 peakPoint = new Vector3(start.x + (end.x - start.x) / 2, peak, start.z + (end.z - start.z) / 2);
        Vector3 m1 = Vector3.Lerp(start, peakPoint, timer/timerval);
        Vector3 m2 = Vector3.Lerp(peakPoint, end, timer/timerval);
        transform.position = Vector3.Lerp(m1, m2, timer/timerval);
    }

    Vector3 GetStartPos()
    {
        RaycastHit hit;
        float yPos;

        if (Physics.Raycast(new Vector3(transform.position.x, 10, transform.position.z), Vector3.down, out hit, 20f, gridLayer))
        {
            yPos = Mathf.Round(hit.point.y) + 0.25f;
            return new Vector3(transform.position.x, yPos, transform.position.z);
        }
        else
        {
            Debug.Log("error placing enemy");
            return transform.position;
        }
    }
}

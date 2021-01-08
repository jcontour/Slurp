using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    public Vector3 dir;
    bool enemyPlaced = false;
    Vector3 startPos;
    Vector3 endPos;
    public LayerMask gridLayer;
    float Timer;
    float timerval;
    float rotSpeed;
    float speed;
    public GameObject enemyMesh;
    float peak;

    bool doMove;

    // Start is called before the first frame update
    void Start()
    {
        doMove = false;
        Timer = 0;
        timerval = 3;
        speed = 5;

        if (!enemyPlaced)
        {
            startPos = GetStartPos();
            transform.position = startPos;
            endPos = startPos;
            enemyPlaced = true;
        }

        //Debug.Log("dir " + dir);

    }

    // Update is called once per frame
    void Update()
    {
        if (doMove)
        {
            //Debug.Log("moving");
            Timer += Time.deltaTime * speed;
            if (Timer < timerval)
            {
                moveEnemy(startPos, endPos, peak, Timer);
               //Debug.Log(dir);
            }
            else
            {
                transform.position = endPos;
                startPos = endPos;
                doMove = false;
                Timer = 0;
            }
        }
        else
        {
            Timer += Time.deltaTime * speed;
            if (Timer > timerval)
            {
                checkCanMove(startPos);
            }
        }
    }

    void checkCanMove(Vector3 start)
    {
        endPos = start + dir;

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(endPos.x, 20, endPos.z), Vector3.down, out hit, 100f, gridLayer) == true)
        {
            endPos.y = hit.point.y;

            if (start.y > endPos.y)
            {
                peak = start.y + 1.5f;
            }
            else if (start.y < endPos.y)
            {
                peak = start.y + 2.5f;
            }
            else
            {
                peak = start.y + 0.5f;
            }
            Timer = 0;
            doMove = true;

        }
        else
        {
            peak = start.y;
            endPos = start;
            doMove = false;
        }
    }

    void moveEnemy(Vector3 start, Vector3 end, float peak, float timer)
    {
        //Debug.Log(start + " " + end);

        Vector3 peakPoint = new Vector3(start.x + (end.x - start.x) / 2, peak, start.z + (end.z - start.z) / 2);
        Vector3 m1 = Vector3.Lerp(start, peakPoint, timer / timerval);
        Vector3 m2 = Vector3.Lerp(peakPoint, end, timer / timerval);
        transform.position = Vector3.Lerp(m1, m2, timer / timerval);

        enemyMesh.transform.RotateAround(transform.position, dir, rotSpeed * Time.deltaTime);
    }

    Vector3 GetStartPos()
    {
        RaycastHit hit;
        float yPos;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20f, gridLayer))
        {
            yPos = hit.point.y;
            return new Vector3(transform.position.x, yPos, transform.position.z);
        }
        else
        {
            return transform.position;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingObject : MonoBehaviour
{
    public float speed;
    public int walkCount;
    protected int currentWalkCount;

    protected bool npcCanMove = true;

    protected Vector3 vector;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public Animator animator;

    protected void Move(String _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }
    protected bool CheckCollsion()
    {
        RaycastHit2D hit;
        // A지점, B지점
        // 레이저
        // hit = null;
        // hit = 방해물;

        Vector2 start = transform.position; //A
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //B

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        if (hit.transform != null)
            return true;
        return false;
    }
    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {

        switch (_frequency)
        {
            case 1:
                yield return new WaitForSeconds(4f);
                break;
            case 2:
                yield return new WaitForSeconds(3f);
                break;
            case 3:
                yield return new WaitForSeconds(2f);
                break;
            case 4:
                yield return new WaitForSeconds(1f);
                break;
            case 5:
                break;
        }
        npcCanMove = false;
        vector.Set(0, 0, vector.z);
        switch(_dir)
        {
            case "UP":
                vector.y = 1f;
                break;
            case "DOWN":
                vector.y = -1f;
                break;
            case "RIGHT":
                vector.x = 1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;

        }
        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);

        while(true)
        {
            bool checkCollsionFlag = CheckCollsion();
            if (checkCollsionFlag) { 
                animator.SetBool("Walking", false);
                yield return new WaitForSeconds(1f);
            }
            else
            {

                break;
            }
        }
        

        animator.SetBool("Walking", true);
        boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);
        while (currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, vector.y * speed, 0);
            currentWalkCount++;
            if (currentWalkCount == 12)
                boxCollider.offset = Vector2.zero;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;
        if(_frequency != 5)
            animator.SetBool("Walking", false);
        npcCanMove = true;
    }
}

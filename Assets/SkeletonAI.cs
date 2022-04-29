using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    int direction = 0;
    Animator anim;
    Rigidbody2D rb;


    float walkTimeAvg = 1.0f;
    float standTimeAvg = 1.0f;
    float turnProbability = 1.0f;
    [SerializeField] float moveSpeed = 5.5f;

    float moveTimer = 2.0f;
    bool walking = false;
    Vector2 moveDir = Vector2.up;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (walking)
        {
            rb.velocity = moveDir * moveSpeed;
            if (moveTimer <= 0) StopWalk();
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (moveTimer <= 0) Walk();
        }
    }

    void StopWalk()
    {
        walking = false;
        moveTimer = standTimeAvg * (Random.value + .5f);
        anim.SetFloat("speed", 0);
    }

    void Walk()
    {
        float rnd = Random.value;
        if (rnd <= turnProbability)
        {
            Turn();
        }
        moveTimer = walkTimeAvg * (Random.value + .5f);
        walking = true;
        anim.SetFloat("speed", moveSpeed*.5f);
    }

    void Turn()
    {
        bool success = false;
        while (!success)
        {
            int r = (int)Random.Range(0, 4);
            if (r != direction)
            {
                success = true;
                direction = r;
            }
        }
        anim.SetInteger("direction", direction);

        switch (direction)
        {
            case 0:
                moveDir = Vector2.up;
                break;
            case 1:
                moveDir = Vector2.right;
                break;
            case 2:
                moveDir = Vector2.down;
                break;
            case 3:
                moveDir = Vector2.left;
                break;
        }
    }
}

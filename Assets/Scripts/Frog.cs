using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float LeftCap;
    [SerializeField] private float RightCap;

    [SerializeField] private float JumpLength;
    [SerializeField] private float JumpHeight;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    private Rigidbody2D RB;

    private bool FacingLeft = true;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        RB = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
       
    }

    private void Move()
    {
        if (FacingLeft)
        {
            //test to see if beyond the left cap
            if (transform.position.x > LeftCap)
            {
                //test to see if frog is on the ground, then jump
                if (coll.IsTouchingLayers(ground))
                {
                    RB.velocity = new Vector2(-JumpLength, JumpHeight);

                    transform.localScale = new Vector3(1, 1);
                }
            }
            else
            {
                FacingLeft = false;
            }
        }

        else
        {
            //test to see if beyond the left cap
            if (transform.position.x < RightCap)
            {
                //test to see if frog is on the ground, then jump
                if (coll.IsTouchingLayers(ground))
                {
                    RB.velocity = new Vector2(JumpLength, JumpHeight);

                    transform.localScale = new Vector3(-1, 1);
                }
            }
            else
            {
                FacingLeft = true;
            }
        }
    }
}
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
    private Animator Animation;

    private bool FacingLeft = true;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        RB = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
    }


    private void Update()
    {
       if(Animation.GetBool("Jumping"))
        {
            if(RB.velocity.y < .1)
            {
                Animation.SetBool("Falling", true);
                Animation.SetBool("Jumping", false);
            }
        }
       if(coll.IsTouchingLayers(ground) && Animation.GetBool("Falling"))
        {
            Animation.SetBool("Falling", false);
        }
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
                    Animation.SetBool("Jumping", true);

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
                    Animation.SetBool("Jumping", true);

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
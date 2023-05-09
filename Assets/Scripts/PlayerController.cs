using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Start variablerna
    private Rigidbody2D RB;
    private Animator Animation;
    private Collider2D Coll;

    //FSM
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;

    //Inspector variabler
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 20f;
    [SerializeField] private int Cherries = 0;
    [SerializeField] private Text CherriesText;
    [SerializeField] private float Damage = 10f;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }

        VelocityState();
        Animation.SetInteger("state", (int)state); //Animation "state" av enum state
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            Cherries += 1;
            CherriesText.text = Cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")

            if(state == State.falling)
            {
                Destroy(other.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right and therefore i should be damaged and moved left
                    RB.velocity = new Vector2(-Damage, RB.velocity.y);
                }
                else
                {
                    //Enemy is to my left and therefore i should be damaged and moved right
                    RB.velocity = new Vector2(Damage, RB.velocity.y);
                }
            }                
        
    }


    private void Movement()
    {
        float DirectionH = Input.GetAxis("Horizontal");

        //Go right
        if (DirectionH < 0)
        {
            RB.velocity = new Vector2(-speed, RB.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        //Go left
        else if (DirectionH > 0)
        {
            RB.velocity = new Vector2(speed, RB.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && Coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    private void Jump()
    {
        RB.velocity = new Vector2(RB.velocity.x, jumpforce);
        state = State.jumping;
    }
       

    private void VelocityState()
    {
        if (state == State.jumping)
        {
            if (RB.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if(Coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if(Mathf.Abs(RB.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(RB.velocity.x) > 2f)
        {
           //Running
            state = State.running;
        }

        else
        {
            state = State.idle;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Animation;
    protected Rigidbody2D RB;
    protected AudioSource death;

    protected virtual void Start()
    {
        Animation = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        death = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        Animation.SetTrigger("Death");
        RB.velocity = new Vector2(0, 0);
        death.Play();
        RB.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}

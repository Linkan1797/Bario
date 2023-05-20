using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Animation;
    protected Rigidbody2D RB;

    protected virtual void Start()
    {
        Animation = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    public void JumpedOn()
    {
        Animation.SetTrigger("Death");
        RB.velocity = new Vector2(0, 0);
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}

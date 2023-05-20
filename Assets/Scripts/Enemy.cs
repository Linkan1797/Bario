using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Animation;

    protected virtual void Start()
    {
        Animation = GetComponent<Animator>();
    }

    public void JumpedOn()
    {
        Animation.SetTrigger("Death");
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}

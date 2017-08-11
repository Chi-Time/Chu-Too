using UnityEngine;
using System.Collections;
using System;

public class Mouse : Avatar
{
    protected override void AssignReferences ()
    {
        base.AssignReferences ();

        this.tag = "Mouse";
    }

    protected override void HitPad (Collider2D other)
    {
        SetDirection (other.GetComponent<DirectionPad> ().CurrentDirection);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Cat"))
            HitCat ();
    }

    private void HitCat ()
    {
        Time.timeScale = 0.0f;
        print ("Game Over");
    }
}

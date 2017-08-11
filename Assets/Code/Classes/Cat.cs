using UnityEngine;
using System.Collections;

public class Cat : Avatar
{
    protected override void AssignReferences ()
    {
        base.AssignReferences ();

        this.tag = "Cat";
    }

    protected override void HitPad (Collider2D other)
    {
        var p = other.GetComponent<DirectionPad> ();

        SetDirection (p.CurrentDirection);
        p.Eat ();
    }
}

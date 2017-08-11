﻿using UnityEngine;
using System.Collections;

//TODO: Store current position of pad when placed, then when scene restarts, replace pad there if eaten.
public class DirectionPad : MonoBehaviour
{
    public Vector2 CurrentDirection { get; private set; }

    [SerializeField] private int _Uses = 3;
    [SerializeField] private Directions _Direction = Directions.Up;

    private void Awake ()
    {
        SetDirection ();
    }

    private void SetDirection ()
    {
        switch(_Direction)
        {
            case Directions.Down:
                CurrentDirection = Vector2.down;
                break;
            case Directions.Up:
                CurrentDirection = Vector2.up;
                break;
            case Directions.Left:
                CurrentDirection = Vector2.left;
                break;
            case Directions.Right:
                CurrentDirection = Vector2.right;
                break;
        }
    } 

    public void Eat ()
    {
        _Uses--;

        if (_Uses <= 0)
            this.gameObject.SetActive (false);
        else
            ShrinkPad ();
    }

    private void ShrinkPad ()
    {
        //TODO: Shrink pad without shrinking collider.
    }
}

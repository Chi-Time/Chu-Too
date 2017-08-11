using UnityEngine;
using System.Collections;

public class DirectionPad : MonoBehaviour
{
    public Vector2 CurrentDirection { get; private set; }

    [SerializeField] private int _Uses = 3;
    [SerializeField] private Directions _Direction = Directions.Up;

    private BoxCollider2D _BoxCollider = null;
    private Vector2 _Size = Vector2.zero;
    private float _ShrinkAmount = 0.0f;

    private void Awake ()
    {
        _BoxCollider = GetComponent<BoxCollider2D> ();
        
        SetDefaults ();
        SetDirection ();
    }

    private void SetDefaults ()
    {
        _Size = _BoxCollider.size;
        _ShrinkAmount = (1f / _Uses);
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
        transform.localScale = new Vector3 (
            transform.localScale.x - _ShrinkAmount, 
            transform.localScale.y - _ShrinkAmount,
            0.0f
        );

        _BoxCollider.size = _Size;
    }
}

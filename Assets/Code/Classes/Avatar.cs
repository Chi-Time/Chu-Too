using UnityEngine;
using System.Collections;

//TODO: Consider using gridmove script for mouse and cat movement.
//TODO: Fix janky turn at move pads.
//TODO: Refactor avatar class as it's unwieldy, can be split into subsections for movement etc.

[RequireComponent(typeof (Collider2D), typeof (Rigidbody2D))]
public abstract class Avatar : MonoBehaviour
{
    [HideInInspector] public float MoveSpeed = 0.5f;

    [SerializeField] public Directions InitialDirection = Directions.Up;
    [SerializeField] protected LayerMask _Walls;

    protected bool _HasStarted = false;
    protected Vector2 _CurrentDir = Vector2.zero;
    protected Transform _Transform = null;
    protected Rigidbody2D _Rigidbody2D = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    protected virtual void AssignReferences ()
    {
        _Transform = GetComponent<Transform> ();
        _Rigidbody2D = GetComponent<Rigidbody2D> ();

        EventManager.OnStageStarted += StageStarted;
    }

    private void Start ()
    {
        SetDefaults ();
    }

    protected virtual void SetDefaults ()
    {
        _Rigidbody2D.gravityScale = 0.0f;
        _Rigidbody2D.isKinematic = false;
        _Rigidbody2D.freezeRotation = true;
    }

    protected void SetInitialDirection ()
    {
        switch(InitialDirection)
        {
            case Directions.Up:
                SetDirection (Vector2.up);
                break;
            case Directions.Down:
                SetDirection (Vector2.down);
                break;
            case Directions.Left:
                SetDirection (Vector2.left);
                break;
            case Directions.Right:
                SetDirection (Vector2.right);
                break;
        }
    }

    protected void SetDirection (Vector2 dir)
    {
        _CurrentDir = dir;

        SetRotation ();
        StartCoroutine (Move (dir));
    }

    protected void SetRotation ()
    {
        if (_CurrentDir == Vector2.up)
            _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
        else if (_CurrentDir == Vector2.down)
            _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
        else if (_CurrentDir == Vector2.left)
            _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
        else if (_CurrentDir == Vector2.right)
            _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, -90));
    }

    protected void Update ()
    {
        CheckAhead ();
    }

    protected void StageStarted (bool hasStarted)
    {
        _HasStarted = hasStarted;

        if (hasStarted)
            SetInitialDirection ();
        else
            ResetAvatar ();
    }

    protected void ResetAvatar ()
    {
        this.gameObject.SetActive (true);
        SetDirection (Vector2.zero);
        StopAllCoroutines ();
    }

    protected IEnumerator Move (Vector2 position)
    {
        var elapsedTime = 0.0f;
        var startPos = _Rigidbody2D.position;

        while (elapsedTime < MoveSpeed)
        {
            _Rigidbody2D.MovePosition (Vector2.Lerp (startPos, startPos + position, (elapsedTime / MoveSpeed)));
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame ();
        }

        _Rigidbody2D.position = new Vector3 (Mathf.Round (_Rigidbody2D.position.x), Mathf.Round (_Rigidbody2D.position.y), 0.0f);

        StopAllCoroutines ();
        StartCoroutine (Move (_CurrentDir));
    }

    /// <summary>Check ahead of the mouse ensuring that no wall is in the way.</summary>
    protected void CheckAhead ()
    {
        if (_CurrentDir == Vector2.up)
        {
            if (IsWall (Vector2.up))
                GetNextDirection (Vector2.up);
        }
        else if (_CurrentDir == Vector2.right)
        {
            if (IsWall (Vector2.right))
                GetNextDirection (Vector2.right);
        }
        else if (_CurrentDir == Vector2.left)
        {
            if (IsWall (Vector2.left))
                GetNextDirection (Vector2.left);
        }
        else if (_CurrentDir == Vector2.down)
        {
            if (IsWall (Vector2.down))
                GetNextDirection (Vector2.down);
        }
    }

    /// <summary>Check the possible directions for the player, ensuring no wall is in the way.</summary>
    protected void GetNextDirection (Vector2 hitDir)
    {
        if (hitDir == Vector2.up)
        {
            if (!IsWall (Vector2.right))
                SetDirection (Vector2.right);
            else if (!IsWall (Vector2.left))
                SetDirection (Vector2.left);
            else if (!IsWall (Vector2.down))
                SetDirection (Vector2.down);
        }
        else if (hitDir == Vector2.right)
        {
            if (!IsWall (Vector2.down))
                SetDirection (Vector2.down);
            else if (!IsWall (Vector2.up))
                SetDirection (Vector2.up);
            else if (!IsWall (Vector2.left))
                SetDirection (Vector2.left);
        }
        else if (hitDir == Vector2.left)
        {
            if (!IsWall (Vector2.up))
                SetDirection (Vector2.up);
            else if (!IsWall (Vector2.down))
                SetDirection (Vector2.down);
            else if (!IsWall (Vector2.right))
                SetDirection (Vector2.right);
        }
        else if (hitDir == Vector2.down)
        {
            if (!IsWall (Vector2.left))
                SetDirection (Vector2.left);
            else if (!IsWall (Vector2.right))
                SetDirection (Vector2.right);
            else if (!IsWall (Vector2.up))
                SetDirection (Vector2.up);
        }
    }

    /// <summary>Check if the current direction contains a wall.</summary>
    protected bool IsWall (Vector2 dir)
    {
        var end = new Vector2 (_Rigidbody2D.position.x + dir.x * .5f, _Rigidbody2D.position.y + dir.y * .5f);

        Debug.DrawLine (_Rigidbody2D.position, end, Color.red);

        if (Physics2D.Linecast (_Rigidbody2D.position, end, _Walls))
            return true;

        return false;
    }

    //private void OnTriggerStay2D (Collider2D other)
    //{
    //    if (other.CompareTag ("Pad")) && VectorsAreEqual (_Transform.position, other.transform.position))
    //        HitPad (other);
    //}

    //private void OnTriggerEnter2D (Collider2D other)
    //{
    //    if (other.CompareTag ("Pad") && _HasStarted)
    //        HitPad (other);
    //}

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag ("Pad") && _HasStarted)
            HitPad (other);
    }

    protected bool VectorsAreEqual (Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude (a - b) < 0.0001;
    }

    protected abstract void HitPad (Collider2D other);

    private void OnDestroy ()
    {
        EventManager.OnStageStarted -= StageStarted;
    }
}

using UnityEngine;
using System.Collections;

public class MouseOld : MonoBehaviour
{
    //[HideInInspector]
    //public float MoveSpeed = 0.5f;

    //[SerializeField]
    //private LayerMask _Walls;
    //[SerializeField]
    //private int _CurrentX = 0;
    //[SerializeField]
    //private int _CurrentY = 0;

    //private Vector2 _CurrentDir = Vector2.zero;
    //private Transform _Transform = null;
    //private Rigidbody2D _Rigidbody2D = null;

    //private void Awake ()
    //{
    //    AssignReferences ();
    //}

    //private void AssignReferences ()
    //{
    //    _Transform = GetComponent<Transform> ();
    //    _Rigidbody2D = GetComponent<Rigidbody2D> ();
    //}

    //private void Start ()
    //{
    //    SetDefaults ();
    //}

    //private void SetDefaults ()
    //{
    //    _Rigidbody2D.gravityScale = 0.0f;
    //    _Rigidbody2D.isKinematic = false;
    //    _Rigidbody2D.freezeRotation = true;

    //    SetDirection (Vector2.up);
    //}

    //private void SetDirection (Vector2 dir)
    //{
    //    _CurrentDir = dir;

    //    SetRotation ();

    //    StartCoroutine (Move (dir, MoveSpeed));
    //}

    //private void SetRotation ()
    //{
    //    if (_CurrentDir == Vector2.up)
    //        _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
    //    else if (_CurrentDir == Vector2.down)
    //        _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
    //    else if (_CurrentDir == Vector2.left)
    //        _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
    //    else if (_CurrentDir == Vector2.right)
    //        _Transform.rotation = Quaternion.Euler (new Vector3 (0, 0, -90));
    //}

    //private void Update ()
    //{
    //    //Move ();
    //    CheckAhead ();
    //}

    ////private void Move ()
    ////{
    ////    _Rigidbody2D.velocity = _CurrentDir * (MoveSpeed * Time.fixedDeltaTime);

    ////    //if (_CurrentDir.x == 0)
    ////    //    _Rigidbody2D.position = new Vector2 (Mathf.Round (_Rigidbody2D.position.x), _Rigidbody2D.position.y);
    ////    //else if (_CurrentDir.y == 0)
    ////    //    _Rigidbody2D.position = new Vector2 (_Rigidbody2D.position.x, Mathf.Round (_Rigidbody2D.position.y));
    ////}

    //private IEnumerator Move (Vector2 position, float time)
    //{
    //    //if (_CurrentDir.x == 0)
    //    //    _Rigidbody2D.position = new Vector2 (Mathf.Round (_Rigidbody2D.position.x), _Rigidbody2D.position.y);
    //    //else if (_CurrentDir.y == 0)
    //    //    _Rigidbody2D.position = new Vector2 (_Rigidbody2D.position.x, Mathf.Round (_Rigidbody2D.position.y));

    //    var elapsedTime = 0.0f;
    //    var startPos = _Rigidbody2D.position;

    //    while (elapsedTime < time)
    //    {
    //        _Rigidbody2D.MovePosition (Vector2.Lerp (startPos, startPos + position, (elapsedTime / time)));
    //        elapsedTime += Time.fixedDeltaTime;
    //        yield return new WaitForEndOfFrame ();
    //    }

    //    _Rigidbody2D.position = new Vector3 (Mathf.Round (_Rigidbody2D.position.x), Mathf.Round (_Rigidbody2D.position.y), 0.0f);

    //    StopAllCoroutines ();
    //    StartCoroutine (Move (_CurrentDir, MoveSpeed));
    //}

    ///// <summary>Check ahead of the mouse ensuring that no wall is in the way.</summary>
    //private void CheckAhead ()
    //{
    //    if (_CurrentDir == Vector2.up)
    //    {
    //        if (IsWall (Vector2.up))
    //            GetNextDirection (Vector2.up);
    //    }
    //    else if (_CurrentDir == Vector2.right)
    //    {
    //        if (IsWall (Vector2.right))
    //            GetNextDirection (Vector2.right);
    //    }
    //    else if (_CurrentDir == Vector2.left)
    //    {
    //        if (IsWall (Vector2.left))
    //            GetNextDirection (Vector2.left);
    //    }
    //    else if (_CurrentDir == Vector2.down)
    //    {
    //        if (IsWall (Vector2.down))
    //            GetNextDirection (Vector2.down);
    //    }
    //}

    ///// <summary>Check the possible directions for the player, ensuring no wall is in the way.</summary>
    //private void GetNextDirection (Vector2 hitDir)
    //{
    //    if (hitDir == Vector2.up)
    //    {
    //        if (!IsWall (Vector2.right))
    //            SetDirection (Vector2.right);
    //        else if (!IsWall (Vector2.left))
    //            SetDirection (Vector2.left);
    //        else if (!IsWall (Vector2.down))
    //            SetDirection (Vector2.down);
    //    }
    //    else if (hitDir == Vector2.right)
    //    {
    //        if (!IsWall (Vector2.down))
    //            SetDirection (Vector2.down);
    //        else if (!IsWall (Vector2.up))
    //            SetDirection (Vector2.up);
    //        else if (!IsWall (Vector2.left))
    //            SetDirection (Vector2.left);
    //    }
    //    else if (hitDir == Vector2.left)
    //    {
    //        if (!IsWall (Vector2.up))
    //            SetDirection (Vector2.up);
    //        else if (!IsWall (Vector2.down))
    //            SetDirection (Vector2.down);
    //        else if (!IsWall (Vector2.right))
    //            SetDirection (Vector2.right);
    //    }
    //    else if (hitDir == Vector2.down)
    //    {
    //        if (!IsWall (Vector2.left))
    //            SetDirection (Vector2.left);
    //        else if (!IsWall (Vector2.right))
    //            SetDirection (Vector2.right);
    //        else if (!IsWall (Vector2.up))
    //            SetDirection (Vector2.up);
    //    }
    //}

    ///// <summary>Check if the current direction contains a wall.</summary>
    //private bool IsWall (Vector2 dir)
    //{
    //    var end = new Vector2 (_Rigidbody2D.position.x + dir.x * .5f, _Rigidbody2D.position.y + dir.y * .5f);

    //    Debug.DrawLine (_Rigidbody2D.position, end, Color.red);

    //    if (Physics2D.Linecast (_Rigidbody2D.position, end, _Walls))
    //        return true;

    //    return false;
    //}

    //private void OnTriggerStay2D (Collider2D other)
    //{
    //    if (other.CompareTag ("Respawn") && VectorsAreEqual (_Transform.position, other.transform.position))
    //        SetDirection (other.GetComponent<DirectionPad> ().CurrentDirection);
    //}

    //private bool VectorsAreEqual (Vector3 a, Vector3 b)
    //{
    //    return Vector3.SqrMagnitude (a - b) < 0.0001;
    //}
}

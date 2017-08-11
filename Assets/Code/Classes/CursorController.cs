using UnityEngine;
using System.Collections;

[System.Serializable]
public class CursorController
{
    public bool _IsMoving = false;

    private enum Orientation { Horizontal, Vertical };

    [SerializeField] private float _GridSize = 1f;
    [SerializeField] private float _MoveSpeed = 5f;
    [SerializeField] private int _MinX = 0, _MaxX = 0;
    [SerializeField] private int _MinY = 0, _MaxY = 0;
    [SerializeField] private bool _AllowDiagonals = true;
    [SerializeField] private bool _CorrectDiagonalSpeed = true;
    [SerializeField] private Orientation _CurrentGridOrientation = Orientation.Vertical;

    private Vector2 _Input = Vector2.zero;
    private Transform _Transform = null;
    private MonoBehaviour _Behaviour = null;

    public void Init (MonoBehaviour behaviour)
    {
        _Behaviour = behaviour;
        _Transform = _Behaviour.GetComponent<Transform> ();
    }
    
    public void GetInput ()
    {
        if (!_IsMoving)
        {
            _Input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

            if (!_AllowDiagonals)
            {
                if (Mathf.Abs (_Input.x) > Mathf.Abs (_Input.y))
                    _Input.y = 0;
                else
                    _Input.x = 0;
            }

            if (_Input != Vector2.zero)
                _Behaviour.StartCoroutine (Move ());
        }
    }

    public IEnumerator Move ()
    {
        _IsMoving = true;

        var startPosition = _Transform.position;
        var endPos = GetEndPosition (startPosition);
        var factor = GetFactor ();
        var time = 0.0f;

        if (IsValidMove (endPos))
        {
            while (time < 1f)
            {
                time += Time.deltaTime * (_MoveSpeed / _GridSize) * factor;
                _Transform.position = Vector3.Lerp (startPosition, endPos, time);

                yield return new WaitForEndOfFrame ();
            }
        }

        _IsMoving = false;

        yield return 0;
    }

    private Vector3 GetEndPosition (Vector3 startPos)
    {
        if (_CurrentGridOrientation == Orientation.Horizontal)
        {
            return new Vector3 (
                startPos.x + System.Math.Sign (_Input.x) * _GridSize,
                startPos.y,
                startPos.z + System.Math.Sign (_Input.y) * _GridSize
            );
        }
        else
        {
            return new Vector3 (
                startPos.x + System.Math.Sign (_Input.x) * _GridSize,
                startPos.y + System.Math.Sign (_Input.y) * _GridSize,
                startPos.z
            );
        }
    }

    private float GetFactor ()
    {
        if (_AllowDiagonals && _CorrectDiagonalSpeed && _Input.x != 0 && _Input.y != 0)
            return 0.7071f;
        else
            return 1f;
    }

    private bool IsValidMove (Vector3 end)
    {
        if(end.x < _MaxX && end.x > _MinX)
            if(end.y < _MaxY && end.y > _MinY)
                return true;

        return false;
    }
}

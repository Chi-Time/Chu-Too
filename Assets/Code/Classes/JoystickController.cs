using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Fix so that tiles cannot be placed on teleporters, pits or rockets.
//TODO: Fix so that eaten tiles are returned to their respective pool. Do this with global pool manager.
public class JoystickController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CursorController _Cursor = new CursorController ();
    //[Header("Attributes")]
    //[SerializeField] private int _UpPadCount = 0;
    //[SerializeField] private int _DownPadCount = 0;
    //[SerializeField] private int _LeftPadCount = 0;
    //[SerializeField] private int _RightPadCount = 0;
    [Header("Prefabs")]
    [SerializeField] private DirectionPad _UpPad = null;
    [SerializeField] private DirectionPad _DownPad = null;
    [SerializeField] private DirectionPad _LeftPad = null;
    [SerializeField] private DirectionPad _RightPad = null;

    private ScenePool _ScenePool = null;

    //private List<DirectionPad> _UpPads = new List<DirectionPad> ();
    //private List<DirectionPad> _DownPads = new List<DirectionPad> ();
    //private List<DirectionPad> _LeftPads = new List<DirectionPad> ();
    //private List<DirectionPad> _RightPads = new List<DirectionPad> ();
    //private List<DirectionPad> _ActivePads = new List<DirectionPad> ();

    private void Awake ()
    {
        _Cursor.Init (this);
        //SetPads ();

        _ScenePool = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScenePool> ();

        EventManager.OnStageStarted += StageStarted;
    }

    //private void SetPads ()
    //{
    //    GeneratePads (_UpPad.gameObject, _UpPads, _UpPadCount);
    //    GeneratePads (_DownPad.gameObject, _DownPads, _DownPadCount);
    //    GeneratePads (_LeftPad.gameObject, _LeftPads, _LeftPadCount);
    //    GeneratePads (_RightPad.gameObject, _RightPads, _RightPadCount);
    //}

    //private void GeneratePads (GameObject pad, List<DirectionPad> pads, int amount)
    //{
    //    for (int i = 0; i < amount; i++)
    //    {
    //        var p = (GameObject)Instantiate (pad, Vector2.zero, Quaternion.identity);
    //        pads.Add (p.GetComponent<DirectionPad> ());
    //        p.SetActive (false);
    //    }
    //}

    private void Update ()
    {
        _Cursor.GetInput ();
        GetInput ();
    }

    private void GetInput ()
    {
        if (Input.GetButtonDown ("Down Pad"))
            PlacePad (_ScenePool._DownPads);
        else if (Input.GetButtonDown ("Right Pad"))
            PlacePad (_ScenePool._RightPads);
        else if (Input.GetButtonDown ("Left Pad"))
            PlacePad (_ScenePool._LeftPads);
        else if (Input.GetButtonDown ("Up Pad"))
            PlacePad (_ScenePool._UpPads);
    }

    /// <summary>Places a pad in the current cursor position, if no other pad is found in the current place.</summary>
    /// <param name="pads">The pad list to place a pad from.</param>
    private void PlacePad (List<DirectionPad> pads)
    {
        // Ensure there is no pad in the current position.
        // If there isn't. Place one down and add it to the active objects pool.
        if (!PadAlreadyInPosition () && pads.Count > 0)// && !SceneObjectInPosition ())
        {
            //for(int i = 0; i < pads.Count; i++)
            //{
            //    if(!pads[i].gameObject.activeSelf)
            //    {
            //        var pad = pads[i];
            //        pads.Remove (pad);
            //        _ScenePool._ActivePads.Add (pad);
            //        pad.gameObject.SetActive (true);

            //        pad.transform.position = GetRoundedPosition ();

            //        return;
            //    }
            //}

            var pad = _ScenePool.GetInactivePad (pads, Vector3.zero);

            if(pad != null)
                pad.transform.position = GetRoundedPosition ();
        }
    }

    /// <summary>Checks if there are any active pads in the current cursor position. Returns true if pad is found.</summary>
    private bool PadAlreadyInPosition ()
    {
        // Loop through every active pad and check if any are under our cursor.
        for (int i = 0; i < _ScenePool._ActivePads.Count; i++)
        {
            if (_ScenePool._ActivePads[i].gameObject.activeSelf)
            {
                if (_ScenePool._ActivePads[i].transform.position == GetRoundedPosition ())
                {
                    _ScenePool.ReturnToPool (_ScenePool._ActivePads[i]);
                    return true;
                }
            }
        }

        return false;
    }

    private bool SceneObjectInPosition ()
    {
        //for(int i = 0; i < _ScenePool._ObjectsInScene.Count; i++)
        //{

        //}

        return false;
    }

    ///// <summary>Sorts the current pad back into the correct pad pool.</summary>
    ///// <param name="pad">The pad to return.</param>
    //private void ReturnToPool (DirectionPad pad)
    //{
    //    if (pad.CurrentDirection == Vector2.up)
    //        _ScenePool._UpPads.Add (pad);
    //    else if (pad.CurrentDirection == Vector2.down)
    //        _ScenePool._DownPads.Add (pad);
    //    else if (pad.CurrentDirection == Vector2.right)
    //        _ScenePool._RightPads.Add (pad);
    //    else if (pad.CurrentDirection == Vector2.left)
    //        _ScenePool._LeftPads.Add (pad);
    //}

    /// <summary>Get's a rounded vector of the cursor's current position.</summary>
    private Vector3 GetRoundedPosition ()
    {
        return new Vector3 (
            Mathf.Round (transform.position.x),
            Mathf.Round (transform.position.y),
            0.0f
        );
    }

    private void StageStarted (bool hasStarted)
    {
        if (hasStarted)
            this.gameObject.SetActive (false);
        else
            this.gameObject.SetActive (true);
    }

    private void OnDestroy ()
    {
        EventManager.OnStageStarted -= StageStarted;
    }
}

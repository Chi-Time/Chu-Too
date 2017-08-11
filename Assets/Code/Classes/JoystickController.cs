using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Fix so that eaten tiles are returned to their respective pool. Do this with global pool manager.
public class JoystickController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CursorController _Cursor = new CursorController ();

    private ScenePool _ScenePool = null;

    private void Awake ()
    {
        _Cursor.Init (this);
        _ScenePool = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScenePool> ();

        EventManager.OnStageStarted += StageStarted;
    }

    private void Update ()
    {
        _Cursor.GetInput ();
        GetInput ();
    }

    private void GetInput ()
    {
        if (Input.GetButtonDown ("Down Pad"))
            PlacePad (_ScenePool.DownPads);
        else if (Input.GetButtonDown ("Right Pad"))
            PlacePad (_ScenePool.RightPads);
        else if (Input.GetButtonDown ("Left Pad"))
            PlacePad (_ScenePool.LeftPads);
        else if (Input.GetButtonDown ("Up Pad"))
            PlacePad (_ScenePool.UpPads);
    }

    /// <summary>Places a pad in the current cursor position, if no other pad is found in the current place.</summary>
    /// <param name="pads">The pad list to place a pad from.</param>
    private void PlacePad (List<DirectionPad> pads)
    {
        // Ensure there is no pad in the current position.
        // If there isn't. Place one down and add it to the active objects pool.
        if (!PadAlreadyInPosition () && pads.Count > 0 && !SceneObjectInPosition ())
        {
            var pad = _ScenePool.GetInactivePad (pads, Vector3.zero);

            if(pad != null)
                pad.transform.position = GetRoundedPosition ();
        }
    }

    /// <summary>Checks if there are any active pads in the current cursor position. Returns true if pad is found.</summary>
    private bool PadAlreadyInPosition ()
    {
        // Loop through every active pad and check if any are under our cursor.
        for (int i = 0; i < _ScenePool.ActivePads.Count; i++)
        {
            if (_ScenePool.ActivePads[i].gameObject.activeSelf)
            {
                if (_ScenePool.ActivePads[i].transform.position == GetRoundedPosition ())
                {
                    _ScenePool.ReturnToPool (_ScenePool.ActivePads[i]);
                    return true;
                }
            }
        }

        return false;
    }

    private bool SceneObjectInPosition ()
    {
        for(int i = 0; i < _ScenePool.ObjectsInScene.Count; i++)
        {
            if (_ScenePool.ObjectsInScene[i].transform.position == GetRoundedPosition ())
                return true;
        }

        return false;
    }

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

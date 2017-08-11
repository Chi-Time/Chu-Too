using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ScenePool : MonoBehaviour
{
    #region Variables
    public List<DirectionPad> _UpPads = new List<DirectionPad> ();
    public List<DirectionPad> _DownPads = new List<DirectionPad> ();
    public List<DirectionPad> _LeftPads = new List<DirectionPad> ();
    public List<DirectionPad> _RightPads = new List<DirectionPad> ();
    public List<DirectionPad> _ActivePads = new List<DirectionPad> ();
    public List<GameObject> _ObjectsInScene = new List<GameObject> ();

    [Header ("Attributes")]
    [Tooltip("The number of up pads the player can have this stage.")]
    [SerializeField] private int _UpPadCount = 0;
    [Tooltip ("The number of down pads the player can have this stage.")]
    [SerializeField] private int _DownPadCount = 0;
    [Tooltip ("The number of left pads the player can have this stage.")]
    [SerializeField] private int _LeftPadCount = 0;
    [Tooltip ("The number of right pads the player can have this stage.")]
    [SerializeField] private int _RightPadCount = 0;
    [Header("Prefabs")]
    [SerializeField] private DirectionPad _UpPad = null;
    [SerializeField] private DirectionPad _DownPad = null;
    [SerializeField] private DirectionPad _LeftPad = null;
    [SerializeField] private DirectionPad _RightPad = null;
    #endregion

    #region Methods
    public void Awake ()
    {
        GeneratePads ();
    }

    private void GeneratePads ()
    {
        GeneratePadPool (_UpPad.gameObject, _UpPads, _UpPadCount);
        GeneratePadPool (_DownPad.gameObject, _DownPads, _DownPadCount);
        GeneratePadPool (_LeftPad.gameObject, _LeftPads, _LeftPadCount);
        GeneratePadPool (_RightPad.gameObject, _RightPads, _RightPadCount);
    }

    private void GeneratePadPool (GameObject padPrefab, List<DirectionPad> padPool, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            var pad = Instantiate<GameObject> (padPrefab, Vector2.zero, Quaternion.identity);
            padPool.Add (pad.GetComponent<DirectionPad> ());
            pad.SetActive (false);
        }
    }

    /// <summary>Sorts the current pad back into the correct pad pool.</summary>
    /// <param name="pad">The pad to return.</param>
    public void ReturnToPool (DirectionPad pad)
    {
        _ActivePads.Remove (pad);
        pad.gameObject.SetActive (false);
        pad.transform.position = Vector2.zero;

        if (pad.CurrentDirection == Vector2.up)
            _UpPads.Add (pad);
        else if (pad.CurrentDirection == Vector2.down)
            _DownPads.Add (pad);
        else if (pad.CurrentDirection == Vector2.right)
            _RightPads.Add (pad);
        else if (pad.CurrentDirection == Vector2.left)
            _LeftPads.Add (pad);
    }

    public DirectionPad GetInactivePad (List<DirectionPad> pads, Vector3 position)
    {
        for(int i = 0; i < pads.Count; i++)
        {
            // If the current pad is inactive.
            if(pads[i].gameObject.activeSelf == false)
            {
                var pad = pads[i];
                pads.Remove (pad);
                _ActivePads.Add (pad);
                pad.gameObject.SetActive (true);

                return pad;
            }
        }

        return null;
    }


    #endregion
}

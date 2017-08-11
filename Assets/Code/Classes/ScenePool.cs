using UnityEngine;
using System.Collections.Generic;

//TODO: Spend time documenting codebase.

[System.Serializable]
public class ScenePool : MonoBehaviour
{
    #region Variables
    public List<DirectionPad> UpPads = new List<DirectionPad> ();
    public List<DirectionPad> DownPads = new List<DirectionPad> ();
    public List<DirectionPad> LeftPads = new List<DirectionPad> ();
    public List<DirectionPad> RightPads = new List<DirectionPad> ();
    public List<DirectionPad> ActivePads = new List<DirectionPad> ();
    public List<GameObject> ObjectsInScene = new List<GameObject> ();

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
        GenerateSceneObjects ();

        EventManager.OnStageStarted += StageStarted;
    }

    private void GeneratePads ()
    {
        GeneratePadPool (_UpPad.gameObject, UpPads, _UpPadCount);
        GeneratePadPool (_DownPad.gameObject, DownPads, _DownPadCount);
        GeneratePadPool (_LeftPad.gameObject, LeftPads, _LeftPadCount);
        GeneratePadPool (_RightPad.gameObject, RightPads, _RightPadCount);
    }

    private void GeneratePadPool (GameObject padPrefab, List<DirectionPad> padPool, int amount)
    {
        // Create a pad, default it, get the component and then add it to it's pool.
        for(int i = 0; i < amount; i++)
        {
            var padGO = Instantiate<GameObject> (padPrefab, Vector2.zero, Quaternion.identity);
            var pad = padGO.GetComponent<DirectionPad> ();
            padGO.SetActive (false);
            padPool.Add (pad);
        }
    }

    private void GenerateSceneObjects ()
    {
        AddToScenePool (GameObject.FindGameObjectsWithTag ("Teleport"));
        AddToScenePool (GameObject.FindGameObjectsWithTag ("Rocket"));
    }

    private void AddToScenePool (GameObject[] objects)
    {
        if(objects != null)
            for(int i = 0; i < objects.Length; i++)
                ObjectsInScene.Add (objects[i]);
    }

    /// <summary>Sorts the current pad back into the correct pad pool. Defaulting it's state.</summary>
    /// <param name="pad">The pad to return.</param>
    public void ReturnToPool (DirectionPad pad)
    {
        ActivePads.Remove (pad);
        pad.gameObject.SetActive (false);
        pad.transform.position = Vector2.zero;

        if (pad.CurrentDirection == Vector2.up)
            UpPads.Add (pad);
        else if (pad.CurrentDirection == Vector2.down)
            DownPads.Add (pad);
        else if (pad.CurrentDirection == Vector2.right)
            RightPads.Add (pad);
        else if (pad.CurrentDirection == Vector2.left)
            LeftPads.Add (pad);
    }

    /// <summary>Retrieves an inactive pad from the pool and returns it activated for use.</summary>
    /// <param name="pads"></param>
    /// <param name="position"></param>
    public DirectionPad GetInactivePad (List<DirectionPad> pads, Vector3 position)
    {
        for(int i = 0; i < pads.Count; i++)
        {
            // If the current pad is inactive:
            // then get it from the pool, make it active and add it to our active pads.
            if(pads[i].gameObject.activeSelf == false)
            {
                var pad = pads[i];
                pads.Remove (pad);
                ActivePads.Add (pad);
                pad.gameObject.SetActive (true);

                return pad;
            }
        }

        return null;
    }

    private void StageStarted (bool hasStarted)
    {
        // If the stage is reset, return each pad back to being active.
        if(!hasStarted)
            for (int i = 0; i < ActivePads.Count; i++)
                ActivePads[i].gameObject.SetActive (true);
    }

    private void OnDestroy ()
    {
        EventManager.OnStageStarted -= StageStarted;
    }
    #endregion
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct AvatarData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Directions InitialDirection;
}

public class AvatarController : MonoBehaviour
{
    [SerializeField] private float _DefaultMiceSpeed = 0.25f;
    [SerializeField] private float _DefaultCatSpeed = 0.4f;
    [SerializeField] private float _FastMiceSpeed = 0.15f;
    [SerializeField] private float _FastCatSpeed = 0.3f;
    [SerializeField] private Avatar[] _Mice = null;
    [SerializeField] private Avatar[] _Cats = null;
    [SerializeField] private AvatarData[] _MiceData = null;
    [SerializeField] private AvatarData[] _CatData = null;

    private void Start ()
    {
        AssignReferences ();
        SetSpeed (false);

        EventManager.OnStageStarted += StageStarted;
    }

    private void AssignReferences ()
    {
        var mice = GameObject.FindGameObjectsWithTag ("Mouse");
        var cats = GameObject.FindGameObjectsWithTag ("Cat");

        if(mice != null)
            GeneratePools (out _Mice, mice, out _MiceData);

        if (cats != null)
            GeneratePools (out _Cats, cats, out _CatData);
    }

    private void GeneratePools (out Avatar[] avatars, GameObject[] avatarGO, out AvatarData[] data)
    {
        avatars = new Avatar[avatarGO.Length];
        data = new AvatarData[avatarGO.Length];

        for (int i = 0; i < avatarGO.Length; i++)
        {
            avatars[i] = avatarGO[i].GetComponent<Avatar> ();

            data[i] = new AvatarData
            {
                Position = avatarGO[i].transform.position,
                Rotation = avatarGO[i].transform.rotation,
                InitialDirection = avatars[i].InitialDirection
            };
        }
    }

    public void SetSpeed (bool speedUp)
    {
        if(speedUp)
        {
            if (_Mice != null)
                for (int i = 0; i < _Mice.Length; i++)
                    _Mice[i].MoveSpeed = _FastMiceSpeed;

            if (_Cats != null)
                for (int i = 0; i < _Cats.Length; i++)
                    _Cats[i].MoveSpeed = _FastCatSpeed;
        }
        else
        {
            if (_Mice != null)
                for (int i = 0; i < _Mice.Length; i++)
                    _Mice[i].MoveSpeed = _DefaultMiceSpeed;

            if (_Cats != null)
                for (int i = 0; i < _Cats.Length; i++)
                    _Cats[i].MoveSpeed = _DefaultCatSpeed;
        }
    }

    public void ResetPositions ()
    {
        //Reset (_Mice, _MiceData);
        //Reset (_Cats, _CatData);

        for (int i = 0; i < _CatData.Length; i++)
        {
            _Cats[i].transform.position = _CatData[i].Position;
            _Cats[i].transform.rotation = _CatData[i].Rotation;
            _Cats[i].InitialDirection = _CatData[i].InitialDirection;
            _Cats[i].gameObject.SetActive (true);
        }

        for (int i = 0; i < _MiceData.Length; i++)
        {
            _Mice[i].transform.position = _MiceData[i].Position;
            _Mice[i].transform.rotation = _MiceData[i].Rotation;
            _Mice[i].InitialDirection = _MiceData[i].InitialDirection;
            _Mice[i].gameObject.SetActive (true);
        }
    }

    private void StageStarted (bool hasStarted)
    {
        if (!hasStarted)
            ResetPositions ();
    }

    private void OnDestroy ()
    {
        EventManager.OnStageStarted -= StageStarted;
    }

    //private void Reset (Avatar[] avatars, AvatarData[] data)
    //{
    //    for (int i = 0; i < data.Length; i++)
    //    {
    //        avatars[i].transform.position = data[i].Position;
    //        avatars[i].transform.rotation = data[i].Rotation;
    //        avatars[i].InitialDirection = data[i].InitialDirection;
    //        avatars[i].gameObject.SetActive (true);

    //        print (data[i].Position + " " + avatars[i].name + " " + avatars[i].transform.position);
    //    }
    //}
}

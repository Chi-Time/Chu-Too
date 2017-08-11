using UnityEngine;
using System.Collections;

//TODO: Make teleporters 2-way.

[RequireComponent (typeof (Collider2D))]
public class Teleport : MonoBehaviour
{
    public int ID = 0;

    [SerializeField] private Transform _Partner = null;
    [SerializeField] private bool _IsEnd = false;
    

    private void Start ()
    {
        GetComponent<Collider2D> ().isTrigger = true;

        FindPartner ();
    }

    private void FindPartner ()
    {
        var teleporters = GameObject.FindGameObjectsWithTag ("Teleport");

        for (int i = 0; i < teleporters.Length; i++)
        {
            var teleport = teleporters[i].GetComponent<Teleport> ();

            if (teleport != this && teleport.ID == ID)
                _Partner = teleport.transform;
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(!_IsEnd)
            other.transform.position = _Partner.transform.position;
    }
}

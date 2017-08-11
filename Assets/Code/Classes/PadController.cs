using UnityEngine;
using System.Collections;

public class PadController : MonoBehaviour
{
    public GameObject CurrentPad = null;

    private void Update ()
    {
        GetInput ();
    }

    private void GetInput ()
    {
        if (Input.GetMouseButtonDown (0) && CurrentPad != null)
        {
            var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            var pos = new Vector3 (Mathf.Round (mousePos.x), Mathf.Round (mousePos.y), 0.0f);
            CurrentPad.transform.position = pos;
        }
    }
}

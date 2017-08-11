using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
    [SerializeField] private bool _WasClicked = false;

    private void OnMouseDown ()
    {
        if (!_WasClicked)
            GameObject.Find ("GameController").GetComponent<PadController> ().CurrentPad = this.gameObject;
        else
            GameObject.Find ("GameController").GetComponent<PadController> ().CurrentPad = null;

        _WasClicked = !_WasClicked;
    }
}

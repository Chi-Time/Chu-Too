using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
public class Rocket : MonoBehaviour
{
    private void Start ()
    {
        GetComponent<Collider2D> ().isTrigger = true;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag ("Mouse"))
            MouseTrigger (other.gameObject);
        else if (other.CompareTag ("Cat"))
            CatTrigger ();
    }

    private void MouseTrigger (GameObject other)
    {
        GameObject.FindGameObjectWithTag ("GameController").GetComponent<LevelController> ().MouseCollected ();
        other.SetActive (false);
    }

    private void CatTrigger ()
    {
        print ("Game Over");
        Time.timeScale = 0.0f;
    }
}

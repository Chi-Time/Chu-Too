using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public static bool HasStarted = false;

    [SerializeField] private int _MiceToCollect = 0;

    private bool _LevelComplete = false;

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        _MiceToCollect = GameObject.FindGameObjectsWithTag ("Mouse").Length;
    }

    private void Update ()
    {
        //TODO: Add speed up button.
        if (Input.GetButtonDown ("Submit") && !HasStarted && !_LevelComplete)
        {
            HasStarted = true;
            EventManager.StartStage (true);

            return;
        }
        else if (Input.GetButtonDown ("Submit") && HasStarted && !_LevelComplete)
        {
            HasStarted = false;
            EventManager.StartStage (false);

            Time.timeScale = 1.0f;

            return;
        }

        if (Input.GetButtonDown ("Speed Up"))
            GetComponent<AvatarController> ().SetSpeed (true);
        else if (Input.GetButtonUp ("Speed Up"))
            GetComponent<AvatarController> ().SetSpeed (false);
    }

    public void MouseCollected ()
    {
        _MiceToCollect--;

        if (_MiceToCollect <= 0)
            LevelOver ();
    }

    private void LevelOver ()
    {
        print ("Level Complete");
        Time.timeScale = 0.0f;
        _LevelComplete = true;
    }
}

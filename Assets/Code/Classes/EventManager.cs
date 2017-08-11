public delegate void StateChanged (GameStates state);
public delegate void StageStarted (bool hasStarted);

public static class EventManager
{
    /// Event listener for when game states are changed.
    public static event StateChanged OnStateChanged;
    /// Event listener for when the global score is updated and changed.
    public static event StageStarted OnStageStarted;

    /// <summary>
    /// Changes the current game state.
    /// </summary>
    /// <param name="state">The new state for the game to switch to.</param>
    public static void ChangeState (GameStates state)
    {
        OnStateChanged (state);
    }

    public static void StartStage (bool hasStarted)
    {
        OnStageStarted (hasStarted);
    }
}

public enum GameStates
{

}

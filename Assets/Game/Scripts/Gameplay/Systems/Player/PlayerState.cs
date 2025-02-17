public sealed class PlayerState
{
    private PlayerMovementState CurrentPlayerMovementState { get; set; } = PlayerMovementState.Idling;

    public void SetMovementState(PlayerMovementState state)
    {
        CurrentPlayerMovementState = state;
    }
}

public enum PlayerMovementState
{
    Idling = 0,
    Running = 1
}
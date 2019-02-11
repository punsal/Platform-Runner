public static class GameEventManager
{
    public delegate void GameEvent();
    public static event GameEvent GameStart, GameOver, Jump;

    public static void TriggerGameStart()
    {
        GameStart?.Invoke();
    }

    public static void TriggerGameOver()
    {
        GameOver?.Invoke();
    }

    public static void TriggerJump()
    {
        Jump?.Invoke();
    }
}
namespace LessmoreCase.Events
{
    using System;
    using LessmoreCase.Utilities;

    
    public static class EventSystem
    {
        public static Action OnStartGame;
        public static void CallStartGame() => OnStartGame?.Invoke();

        public static Action<GameResult> OnGameOver;
        public static void CallGameOver(GameResult gameResult) => OnGameOver?.Invoke(gameResult);

        public static Action OnNewLevelLoad;
        public static void CallNewLevelLoad() => OnNewLevelLoad?.Invoke();

        public static Action<int> OnElementsDespawned;
        public static void CallElementsDespawned(int count) => OnElementsDespawned?.Invoke(count);

        public static Action<int> OnSelectionChanged;
        public static void CallSelectionChanged(int count) => OnSelectionChanged?.Invoke(count);

        public static Action<int, int> OnScoreChanged;
        public static void CallScoreChanged(int oldScore, int newScore) => OnScoreChanged?.Invoke(oldScore, newScore);
    }
}

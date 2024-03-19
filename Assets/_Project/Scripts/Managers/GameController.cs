namespace LessmoreCase.Game
{
    using LessmoreCase.Events;
    using LessmoreCase.Utilities;
    using System.Collections;
    using UnityEngine;

    public class GameController : Singleton<GameController>
    {
        public static int Score { get; private set; }

        [SerializeField] private GameGrid _grid = null;

        [SerializeField] private int _movesAvailable = 20;

        public int MovesAvailable
        {
            get => this._movesAvailable;
            set => this._movesAvailable = value;
        }


        public void Init()
        {
            SetStatus(Status.ready);
        }


        private void OnEnable()
        {
            EventSystem.OnNewLevelLoad += OnNewLevelLoad;

            EventSystem.OnElementsDespawned += OnElementsDespawned;
        }

        private void OnDisable()
        {
            EventSystem.OnNewLevelLoad -= OnNewLevelLoad;

            EventSystem.OnElementsDespawned += OnElementsDespawned;
        }

        private void OnNewLevelLoad()
        {
            InitializeGame();

            StartCoroutine(RunGame());
        }

        private void OnElementsDespawned(int count)
        {
            int oldScore = Score;
            Score = oldScore + count * (count - 1);

            EventSystem.OnScoreChanged.Invoke(oldScore, Score);
        }

        public void InitializeGame()
        {
            Score = 0;

            this._grid.SetUpGrid();
        }

        public IEnumerator RunGame()
        {
            while (this.MovesAvailable > 0)
            {
                yield return this._grid.WaitForSelection();

                yield return this._grid.DespawnSelection();

                yield return this._grid.WaitForMovement();

                yield return this._grid.RespawnElements();
            }
        }
    }
}
namespace LessmoreCase.Game
{
    using LessmoreCase.Events;
    using LessmoreCase.Utilities;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameController : Singleton<GameController>
    {
        public static int Score { get; private set; }

        [SerializeField] private GameGrid _grid = null;

        [SerializeField] private int _movesAvailable = int.MaxValue;

        public List<GameGridElement> selectedElements;

        private int _moveSelectionEndValue;
        private int _moveSelectionValue;

        public int MovesAvailable
        {
            get => this._movesAvailable;
            set => this._movesAvailable = value;
        }

        public int MoveSelectionEndValue
        {
            get { return this._moveSelectionEndValue; }
            set { this._moveSelectionEndValue = value; }
        }

        public int MoveSelectionValue
        {
            get { return this._moveSelectionValue; }
            set { this._moveSelectionValue = value; }
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
            this._grid = FindAnyObjectByType<GameGrid>();

            InitializeGame();

            StartCoroutine(RunGame());
        }

        private void OnElementsDespawned(int count)
        {
            int oldScore = Score;
            Score = oldScore + count * (count - 1);

            EventSystem.CallScoreChanged(oldScore, Score);
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

        public int FindClosestValue(int value)
        {
            var closest = int.MaxValue;
            var minDifference = int.MaxValue;
            for (int i = 0; i < _grid.gridElementValues.Count; i++)
            {
                var difference = Mathf.Abs((long)_grid.gridElementValues[i].GridValue - value);
                if (minDifference > difference)
                {
                    minDifference = (int)difference;
                    closest = _grid.gridElementValues[i].GridValue;
                }
            }

            return closest;
        }
    }
}
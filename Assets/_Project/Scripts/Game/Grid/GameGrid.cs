namespace LessmoreCase.Game
{
    using LessmoreCase.Utilities.Extensions;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameGrid : MonoBehaviour
    {
        [System.Serializable]
        public class GridElementValue
        {
            public Color GridColor;
            public int GridValue;
        }

        [SerializeField] private SelectionLine _selectionLine = null;
        [SerializeField] private GameGridElement _gridElementPrefab = null;

        [SerializeField] private int _rowCount = 8;
        [SerializeField] private int _columnCount = 8;
        [SerializeField] private float _cellSize = 1.0f;

        [SerializeField] private List<Color> _colors = new List<Color>();

        public List<GridElementValue> gridElementValues = new List<GridElementValue>();
        public List<GridElementValue> gridElementsToSpawn;
        
        private GameGridInput _gridInput;
        private GameGridMovement _gridMovement;
        private GameGridSpawning _gridSpawning;

        public List<GameGridElement> Elements { get; } = new List<GameGridElement>();

        //public List<Color> Colors => this._colors;

        public Vector2 StartCellPosition => this.GridCenter - this.CellSize * new Vector2(this.ColumnCount - 1, this.RowCount - 1) / 2.0f;

        public float CellSize => this._cellSize;
        public int RowCount => this._rowCount;
        public int ColumnCount => this._columnCount;
        public Vector2 GridCenter => this.transform.position;
        public Transform GridContainer => this.transform;

        private void OnEnable()
        {
            this._gridInput = new GameGridInput(this, this._selectionLine);
            this._gridMovement = new GameGridMovement(this);
            this._gridSpawning = new GameGridSpawning(this);

            gridElementsToSpawn.AddRange(new List<GridElementValue> { 
                gridElementValues[0], 
                gridElementValues[1], 
                gridElementValues[2] 
            });
        }

        public void SetUpGrid()
        {
            for (int y = 0; y < this.ColumnCount; y++)
            {
                for (int x = 0; x < this.RowCount; x++)
                {
                    GameGridElement element = Instantiate(this._gridElementPrefab);

                    element.transform.localScale = Vector2.one * this._cellSize;

                    element.transform.position = this.GridToWorldPosition(x, y);
                    element.Position = this.GridToWorldPosition(x, y);
                    element.spawnPosition = element.Position;

                    element.transform.SetParent(this.GridContainer.transform, true);

                    int index = this.gridElementsToSpawn.GetRandomIndex();

                    element.Color = this.gridElementsToSpawn[index].GridColor;
                    element.ElementText = this.gridElementsToSpawn[index].GridValue.ToString();
                    element.Value = this.gridElementsToSpawn[index].GridValue;

                    this.Elements.Add(element);
                }
            }
        }

        public Coroutine WaitForMovement()
        {
            return StartCoroutine(this._gridMovement.WaitForMovement());
        }

        public Coroutine WaitForSelection()
        {
            return StartCoroutine(this._gridInput.WaitForSelection());
        }

        public Coroutine RespawnElements()
        {
            return StartCoroutine(this._gridSpawning.RespawnElements());
        }

        public Coroutine DespawnSelection()
        {
            return StartCoroutine(this._gridSpawning.Despawn(this._gridInput.SelectedElements));
        }
    }
}
namespace LessmoreCase.Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameGridMovement
    {
        private GameGrid _grid;

        public GameGridMovement(GameGrid grid)
        {
            this._grid = grid;
        }

        public IEnumerator WaitForMovement()
        {
            _moveElements();

            while (_isMovementDone() == false)
            {
                yield return new WaitForSeconds(0.05f);
            }

            yield break;
        }

        private bool _isMovementDone()
        {
            foreach (GameGridElement element in this._grid.Elements)
            {
                if (element.IsSpawned && element.IsMoving)
                {
                    return false;
                }
            }

            return true;
        }

        private void _moveElements()
        {
            for (int y = 0; y < this._grid.RowCount; y++)
            {
                for (int x = 0; x < this._grid.ColumnCount; x++)
                {
                    _processCell(x, y);
                }
            }
        }

        private void _processCell(int column, int row)
        {
            GameGridElement element = this._grid.GetElement(column, row);

            if (element.IsSpawned == false)
            {
                for (int i = row + 1; i < this._grid.RowCount; i++)
                {
                    GameGridElement next = this._grid.GetElement(column, i);

                    if (next != null && next.IsSpawned && !next.IsMoving)
                    {
                        _moveElement(new Vector2Int(column, i), new Vector2Int(column, row));

                        return;
                    }
                }
            }
        }

        private void _moveElement(Vector2Int oldPos, Vector2Int newPos)
        {
            GameGridElement element1 = this._grid.GetElement(oldPos.x, oldPos.y);
            GameGridElement element2 = this._grid.GetElement(newPos.x, newPos.y);

            this._grid.Elements[this._grid.GridPositionToIndex(oldPos)] = element2;
            this._grid.Elements[this._grid.GridPositionToIndex(newPos)] = element1;

            element2.transform.position = this._grid.GridToWorldPosition(oldPos);
            element1.Move(this._grid.GridToWorldPosition(newPos), 0.4f);
        }
    }
}
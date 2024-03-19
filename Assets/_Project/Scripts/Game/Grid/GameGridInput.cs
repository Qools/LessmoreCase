namespace LessmoreCase.Game
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using LessmoreCase.Events;

    public class GameGridInput
    {
        private GameGrid _grid;
        private SelectionLine _selectionLine;

        public List<GameGridElement> SelectedElements { get; private set; }

        public GameGridInput(GameGrid grid, SelectionLine selectionLine)
        {
            this._grid = grid;
            this._selectionLine = selectionLine;
            this.SelectedElements = new List<GameGridElement>(); ;
        }

        private void _clear()
        {
            this._selectionLine.Clear();
            this.SelectedElements.Clear();

            EventSystem.CallSelectionChanged(this.SelectedElements.Count);
        }
        public IEnumerator WaitForSelection()
        {
            _clear();

            while (true)
            {
                yield return null;

                if (Input.GetMouseButton(0))
                {
                    if (_inputRaycast(out GameGridElement element))
                    {
                        _processGridElement(element);
                    }
                }
                else if (this.SelectedElements.Count > 0)
                {
                    if (_isValidInput())
                    {
                        this._selectionLine.Clear();

                        yield break;
                    }
                    else
                    {
                        _clear();
                    }
                }
            }
        }
        private bool _inputRaycast(out GameGridElement element)
        {
            element = null;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hitInfo)
            {
                element = hitInfo.transform.GetComponent<GameGridElement>();

                if (element != null)
                {
                    return true;
                }
            }

            return false;
        }

        private void _processGridElement(GameGridElement element)
        {
            if (this.SelectedElements.Count == 0)
            {
                this._selectionLine.Color = element.Color;

                _addElementToSelection(element);
            }
            else
            {
                if (this.SelectedElements.Contains(element))
                {
                    if (_isSecondLast(element))
                    {
                        _deselectLast();
                    }
                }
                else if (_isSelectable(element))
                {
                    _addElementToSelection(element);
                }
            }
        }

        private bool _isSecondLast(GameGridElement element)
        {
            return this.SelectedElements.Count >= 2 && element.Equals(this.SelectedElements[this.SelectedElements.Count - 2]) == true;
        }

        private bool _isSelectable(GameGridElement element)
        {
            return _hasValidColor(element) && _isInDistance(element);
        }

        private bool _hasValidColor(GameGridElement element)
        {
            return element.Color == this._selectionLine.Color;
        }

        private bool _isInDistance(GameGridElement element)
        {
            Vector2 lastElementPos = this.SelectedElements.Last().transform.position;

            return Vector2.Distance(element.transform.position, lastElementPos) < 1.5f * this._grid.CellSize;
        }

        private void _addElementToSelection(GameGridElement element)
        {
            if (this.SelectedElements.Count > 0)
            {
                float pitch = 1.0f + this.SelectedElements.Count * 0.1f;
                GameSFX.Instance.Play(GameSFX.Instance.SelectionClip, pitch);
            }

            this.SelectedElements.Add(element);

            this._selectionLine.Color = element.Color;

            this._selectionLine.SetPositions(this.SelectedElements);

            EventSystem.CallSelectionChanged(this.SelectedElements.Count);
        }

        private void _deselectLast()
        {
            this.SelectedElements.Remove(this.SelectedElements.Last());

            this._selectionLine.SetPositions(this.SelectedElements);

            EventSystem.CallSelectionChanged(this.SelectedElements.Count);
        }

        private bool _isValidInput()
        {
            return this.SelectedElements.Count > 1 ? true : false;
        }
    }
}
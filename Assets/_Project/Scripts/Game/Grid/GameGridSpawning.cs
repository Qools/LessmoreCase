namespace LessmoreCase.Game
{
    using LessmoreCase.Utilities.Extensions;
    using LessmoreCase.Events;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameGridSpawning
    {
        private GameGrid _grid;

        private GameGridElement _lastElement;

        public GameGridSpawning(GameGrid grid)
        {
            this._grid = grid;
        }

        public IEnumerator RespawnElements()
        {
            GameSFX.Instance.Play(GameSFX.Instance.SpawnClip);

            float closestValue = GameController.Instance.FindClosestValue(GameController.Instance.MoveSelectionValue);

            _lastElement.Color = this._grid.gridElementValues.Find(e => e.GridValue == closestValue).GridColor;
            _lastElement.ElementText = this._grid.gridElementValues.Find(e => e.GridValue == closestValue).GridValue.ToString();
            _lastElement.Value = this._grid.gridElementValues.Find(e => e.GridValue == closestValue).GridValue;

            yield return new WaitForSeconds(0.4f);

            for (int i = 0; i < _grid.Elements.Count; i++)
            {
                if (_grid.Elements[i].IsSpawned == false)
                {
                    int index = this._grid.gridElementsToSpawn.GetRandomIndex();

                    _grid.Elements[i].Color = this._grid.gridElementsToSpawn[index].GridColor;
                    _grid.Elements[i].ElementText = this._grid.gridElementsToSpawn[index].GridValue.ToString();
                    _grid.Elements[i].Value = this._grid.gridElementsToSpawn[index].GridValue;

                    _grid.Elements[i].Spawn();
                }
            }
        }

        public IEnumerator Despawn(List<GameGridElement> elements)
        {
            GameSFX.Instance.Play(GameSFX.Instance.DespawnClip);

            GameController.Instance.MoveSelectionValue = 0;

            foreach (GameGridElement element in elements)
            {
                GameController.Instance.MoveSelectionValue += element.Value;
            }

            for (int i = 0; i < elements.Count - 1; i++)
            {
                elements[i].Despawn();
            }

            _lastElement = elements[elements.Count - 1];

            yield return new WaitForSeconds(0.4f);

            GameController.Instance.MovesAvailable--;

            EventSystem.CallElementsDespawned(elements.Count);
        }
    }
}
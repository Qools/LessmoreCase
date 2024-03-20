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

        public GameGridSpawning(GameGrid grid)
        {
            this._grid = grid;
        }

        public IEnumerator RespawnElements()
        {
            GameSFX.Instance.Play(GameSFX.Instance.SpawnClip);

            foreach (GameGridElement element in this._grid.Elements)
            {
                if (element.IsSpawned == false)
                {
                    int index = this._grid.gridElementValues.GetRandomIndex();

                    element.Color = this._grid.gridElementValues[index].GridColor;
                    element.ElementText = this._grid.gridElementValues[index].GridValue.ToString();

                    element.Spawn();
                }
            }

            yield return new WaitForSeconds(0.4f);
        }

        public IEnumerator Despawn(List<GameGridElement> elements)
        {
            GameSFX.Instance.Play(GameSFX.Instance.DespawnClip);

            for (int i = 0; i < elements.Count - 1; i++)
            {
                elements[i].Despawn();
            }

            yield return new WaitForSeconds(0.4f);

            GameController.Instance.MovesAvailable--;

            EventSystem.CallElementsDespawned(elements.Count);
        }
    }
}
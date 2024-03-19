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
                    element.Color = this._grid.Colors.GetRandom();

                    element.Spawn();
                }
            }

            yield return new WaitForSeconds(0.4f);
        }

        public IEnumerator Despawn(List<GameGridElement> elements)
        {
            GameSFX.Instance.Play(GameSFX.Instance.DespawnClip);

            foreach (GameGridElement gridElement in elements)
            {
                gridElement.Despawn();
            }

            yield return new WaitForSeconds(0.4f);

            GameController.Instance.MovesAvailable--;

            EventSystem.OnElementsDespawned.Invoke(elements.Count);
        }
    }
}
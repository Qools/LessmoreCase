namespace LessmoreCase.Game
{
    using LessmoreCase.Utilities.Extensions;
    using UnityEngine;

    public class GameGridElement : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        public bool IsMoving { get; private set; }
        public bool IsSpawned => this.gameObject.activeSelf;
        public Color Color
        {
            get { return this._spriteRenderer.color; }
            set { this._spriteRenderer.color = value; }
        }

        public void Move(Vector3 targetPos, float time)
        {
            this.IsMoving = true;

            this.transform.MoveCoroutine(targetPos, time, () =>
            {
                this.IsMoving = false;
            });
        }

        public void Spawn()
        {
            this.gameObject.SetActive(true);

            Vector2 startScale = Vector2.one * 0.1f;
            Vector2 endScale = Vector2.one * 1.0f;

            StartCoroutine(this.transform.ScaleCoroutine(startScale, endScale, 0.25f));
        }

        public void Despawn()
        {
            Vector2 startScale = Vector2.one * 1.0f;
            Vector2 endScale = Vector2.one * 0.1f;

            StartCoroutine(this.transform.ScaleCoroutine(startScale, endScale, 0.25f, () =>
            {
                this.gameObject.SetActive(false);
            }));
        }
    }
}
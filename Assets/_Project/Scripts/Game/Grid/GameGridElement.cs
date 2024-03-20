namespace LessmoreCase.Game
{
    using LessmoreCase.Utilities.Extensions;
    using UnityEngine;
    using TMPro;

    public class GameGridElement : MonoBehaviour
    {

        [SerializeField] private TextMeshPro _gridText;
        [SerializeField] private SpriteRenderer _spriteRenderer = null;

        public Vector3 spawnPosition;

        public bool IsMoving { get; private set; }
        public bool IsSpawned => this.gameObject.activeSelf;
        public Color Color
        {
            get { return this._spriteRenderer.color; }
            set { this._spriteRenderer.color = value; }
        }

        public string ElementText
        {
            get { return this._gridText.text; }
            set { this._gridText.text = value; }
        }

        public Vector3 Position
        {
            get { return this.transform.position; }
            set { this.transform.position = value; }
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

        public void Despawn(Transform target)
        {
            this.transform.MoveCoroutine(target.position, 0.25f, () =>
            {
                this.gameObject.SetActive(false);
            });
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
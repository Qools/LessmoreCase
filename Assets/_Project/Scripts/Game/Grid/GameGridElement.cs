namespace LessmoreCase.Game
{
    using LessmoreCase.Utilities.Extensions;
    using UnityEngine;
    using TMPro;

    public class GameGridElement : MonoBehaviour
    {
        private int _value;
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

        public int Value
        {
            get { return this._value; }
            set { this._value = value; }
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

            this.transform.ScaleCoroutine(startScale, endScale, 0.25f);
        }

        public void Despawn(Transform target)
        {
            Vector3 oldPos = this.transform.position;
            Vector2 startScale = Vector2.one * 1.0f;
            Vector2 endScale = Vector2.one * 0.1f;

            this.transform.PunchScaleEffect(new Vector3(0.25f, 0.25f, 0.25f), 5, 1, 0.25f);

            this.transform.MoveCoroutine(target.position, 0.25f, () =>
            {
                this.gameObject.SetActive(false);
                this.transform.position = oldPos;
            });
        }

        public void Despawn()
        {
            Vector2 startScale = Vector2.one * 1.0f;
            Vector2 endScale = Vector2.one * 0.1f;

            this.transform.ScaleCoroutine(startScale, endScale, 0.25f, () =>
            {
                this.gameObject.SetActive(false);
            });
        }
    }
}
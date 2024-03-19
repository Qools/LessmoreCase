namespace LessmoreCase.Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(LineRenderer))]
    public class SelectionLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Color _color;

        public Color Color
        {
            get { return this._color; }
            set
            {
                this._color = value;
                this._lineRenderer.startColor = value;
                this._lineRenderer.endColor = value;
            }
        }

        private void Awake()
        {
            this._lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetPositions(List<GameGridElement> selectedElements)
        {
            this._lineRenderer.positionCount = selectedElements.Count;

            for (int i = 0; i < selectedElements.Count; i++)
            {
                this._lineRenderer.SetPosition(i, selectedElements[i].transform.position);
            }
        }

        public void Clear()
        {
            this._lineRenderer.positionCount = 0;
        }
    }
}
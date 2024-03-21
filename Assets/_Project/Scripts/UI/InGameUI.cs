namespace LessmoreCase.UI
{
    using LessmoreCase.Game;
    using UnityEngine;
    using LessmoreCase.Events;
    using TMPro;
    using LessmoreCase.Utilities.Extensions;

    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject _selectionPrediction;
        [SerializeField] private SpriteRenderer _selectionPreditionSprite;
        [SerializeField] private TextMeshPro _selectionPreditionText;

        [SerializeField] private GameGrid _grid;

        // Start is called before the first frame update
        void Start()
        {
            _selectionPrediction.SetActive(false);
        }

        private void OnEnable()
        {
            EventSystem.OnSelectionStarted += _enableSelectionPrediction;
            EventSystem.OnSelectionEnd += _disableSelectionPrediction;

            EventSystem.OnSelectionChanged += _updateSelectionPrediction;
        }

        private void OnDisable()
        {
            EventSystem.OnSelectionStarted -= _enableSelectionPrediction;
            EventSystem.OnSelectionEnd -= _disableSelectionPrediction;

            EventSystem.OnSelectionChanged -= _updateSelectionPrediction;
        }

        private void _enableSelectionPrediction()
        {
            _selectionPrediction.SetActive(true);
        }

        private void _disableSelectionPrediction()
        {
            _selectionPrediction.SetActive(false);
        }

        private void _updateSelectionPrediction(int value)
        {
            _selectionPrediction.transform.PunchScaleEffect(new Vector3(0.1f, 0.1f, 0.1f), 5, 1, 0.25f);

            float closestValue = GameController.Instance.FindClosestValue(GameController.Instance.MoveSelectionValue);

            _selectionPreditionSprite.color = this._grid.gridElementValues.Find(e => e.GridValue == closestValue).GridColor;
            _selectionPreditionText.text = this._grid.gridElementValues.Find(e => e.GridValue == closestValue).GridValue.ToString();
        }
    }
}
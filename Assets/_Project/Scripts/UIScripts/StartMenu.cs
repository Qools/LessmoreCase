namespace LessmoreCase.Game.UI
{
    using LessmoreCase.Events;
    using UnityEngine;
    using UnityEngine.UI;

    public class StartMenu : UIPanel
    {
        [SerializeField] private Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(() => StartGame());
        }

        public void StartGame()
        {
            EventSystem.CallStartGame();

            MenuManager.Instance.SwitchPanel<InGamePanel>();
        }
    }
}
namespace LessmoreCase.Game.UI
{
    using TMPro;

    public class InGamePanel : UIPanel
    {
        public TextMeshProUGUI inGameText;

        public override void Open()
        {
            base.Open();

            inGameText.text = "Level " + DataManager.Instance.GetLevel().ToString("00");
        }
    }
}
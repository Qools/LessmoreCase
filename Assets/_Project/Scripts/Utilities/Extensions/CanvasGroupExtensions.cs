namespace LessmoreCase.Utilities.Extensions
{
    using DG.Tweening;
    using System.Collections;
    using UnityEngine;

    public static class CanvasGroupExtensions
    {
        public static void FadeInCoroutine(this CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            canvasGroup.DOFade(0f, duration).OnComplete(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });

            
        }

        public static void FadeOutCoroutine(this CanvasGroup canvasGroup, float duration)
        {

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            canvasGroup.DOFade(1f, duration);
        }
    }
}
namespace LessmoreCase.Utilities.Extensions
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.Events;

    public static class TransformExtensions
    {
        public static void ScaleCoroutine(this Transform transform, Vector2 from, Vector2 to, float time, UnityAction onComplete = null)
        {
            transform.DOScale(to, time).OnComplete(() =>
            {
                transform.localScale = to;

                onComplete?.Invoke();
            });

        }

        public static void MoveCoroutine(this Transform transform, Vector2 targetPos, float time, UnityAction onComplete = null)
        {
            Vector2 startPos = transform.position;

            transform.DOMove(targetPos, time).OnComplete(() => 
            {
                transform.position = targetPos;

                onComplete?.Invoke();
            });
        }
    }
}
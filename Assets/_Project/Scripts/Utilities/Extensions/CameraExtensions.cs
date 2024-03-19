namespace LessmoreCase.Utilities.Extensions
{
    using UnityEngine;

    public static class CameraExtensions
    {
        public static float GetHeight(this Camera camera)
        {
            return camera.orthographicSize * 2;
        }

        public static float GetWidth(this Camera camera)
        {
            return camera.GetHeight() * camera.aspect;
        }
    }
}
using UnityEngine;

namespace YooE
{
    public static class MotionUseCases
    {
        public static void Rotate(Transform objectTransform, Vector3 direction, float rotationSpeed)
        {
            if (direction == Vector3.zero) return;

            objectTransform.rotation =
                Quaternion.Lerp(objectTransform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
        }
    }
}
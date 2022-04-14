using UnityEngine;

namespace MarwanZaky
{
    namespace MathfX
    {
        public static class MathfX
        {
            public static Vector3 AngleToVector2D(float angleDeg)
            {
                var angleRad = angleDeg * Mathf.Deg2Rad;
                return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f);
            }

            public static Vector3 AngleToVector3D(float angleDeg)
            {
                var angleRad = angleDeg * Mathf.Deg2Rad;
                return new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad));
            }

            public static float VectorToAngle2D(Vector3 vector)
            {
                vector.Normalize();

                var angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

                if (angle < 0)
                    angle += 360;

                return angle;
            }

            public static float VectorToAngle3D(Vector3 vector)
            {
                vector.Normalize();

                var angle = Mathf.Atan2(vector.z, vector.x) * Mathf.Rad2Deg;

                // if (angle < 0)
                //     angle += 360;

                return angle;
            }
        }
    }
}
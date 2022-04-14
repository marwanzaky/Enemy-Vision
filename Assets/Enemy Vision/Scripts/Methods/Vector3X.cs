using UnityEngine;

namespace MarwanZaky {
    namespace Methods {
        public static class Vector3X {
            public static Vector3 IgnoreZ(Vector3 value, float z = 0) {
                return new Vector3(value.x, value.y, z);
            }

            public static Vector3 IgnoreY(Vector3 value, float y = 0) {
                return new Vector3(value.x, y, value.z);
            }

            public static Vector3 IgnoreX(Vector3 value, float x = 0) {
                return new Vector3(x, value.y, value.z);
            }
        }
    }
}
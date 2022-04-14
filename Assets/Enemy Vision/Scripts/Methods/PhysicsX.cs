using UnityEngine;

namespace MarwanZaky {
    namespace Methods {
        public static class PhysicsX {
            public static (RaycastHit hit, Ray ray) MouseHit(LayerMask layerMask, float maxDistance = 1000f, bool debug = false) {
                return MouseHit(Camera.main, layerMask, maxDistance, debug);
            }

            public static (RaycastHit hit, Ray ray) MouseHit(Camera cam, LayerMask layerMask, float maxDistance = 1000f, bool debug = false) {
                var mousePos = Input.mousePosition;
                var ray = cam.ScreenPointToRay(mousePos);
                var hit = new RaycastHit();

                Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, layerMask);

                if (debug) {
                    Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
                }

                return (hit, ray);
            }

            public static RaycastHit Raycast(Vector3 origin, Vector3 direction, LayerMask layerMask, float maxDistance = 10f, bool debug = false) {
                var ray = new Ray(origin, direction);
                var hit = new RaycastHit();

                Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, layerMask);

                if (debug) {
                    Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
                }

                return hit;
            }
        }
    }
}
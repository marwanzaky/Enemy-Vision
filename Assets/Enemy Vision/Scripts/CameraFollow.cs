using UnityEngine;

namespace MarwanZaky {
    public class CameraFollow : MonoBehaviour {
        Vector3 offset;

        [SerializeField] Transform target;

        private void Start() {
            offset = transform.position - target.position;
        }

        private void LateUpdate() {
            var targetPos = target.position + offset;
            transform.position = targetPos;
        }
    }
}
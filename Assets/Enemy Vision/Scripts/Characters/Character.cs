using UnityEngine;
using MarwanZaky.Methods;

namespace MarwanZaky {
    public class Character : MarwanZaky.Shared.Character {
        const float GRAVITY = -9.81f;

        Vector3 velocity = Vector3.zero;

        protected bool isGrounded = false;
        protected bool wasGrounded = false;

        protected float smoothAnimValVelX;    // smooth animation value velocity x-axis
        protected float smoothAnimValVelY;    // smooth animation value velcoity y-axis

        [Header("Character Settings"), SerializeField] protected Animator animator;
        [SerializeField] protected Collider groundCollider;
        [SerializeField] protected LayerMask groundMask;
        [SerializeField] protected float gravityScale = 1f;
        [SerializeField] protected float jumpHeight = 5f;
        [SerializeField] protected float walkSpeed = 3f;
        [SerializeField] protected float runSpeed = 10f;
        [SerializeField] protected float smoothMoveTime = .3f;
        [SerializeField] protected float smoothRotateTime = 3f;

        protected virtual bool IsRunning { get; set; }

        protected float Speed => IsRunning ? runSpeed : walkSpeed;

        protected bool _IsGrounded => isGrounded;

        protected bool IsFloating {
            get => animator.GetBool("Float");
            set => animator.SetBool("Float", value);
        }

        protected float Animator_MoveX {
            get => animator.GetFloat("MoveX");
            set => animator.SetFloat("MoveX", value);
        }

        protected float Animator_MoveY {
            get => animator.GetFloat("MoveY");
            set => animator.SetFloat("MoveY", value);
        }

        protected Vector2 _Move { get; set; }
        protected Vector3 Velocity => velocity;

        protected virtual float Radius => .25f;

        protected virtual void Update() {
            IsGrounded();
            Gravity();
        }

        private void Gravity() {
            if (isGrounded && velocity.y < 0)
                velocity.y = -2;

            velocity.y += GRAVITY * gravityScale * Time.deltaTime;
            Move(velocity * Time.deltaTime);
        }

        protected virtual void Move(Vector3 velocity) {
            Animator_MoveX = GetAnimMoveVal(_Move.x, Animator_MoveX, ref smoothAnimValVelX);
            Animator_MoveY = GetAnimMoveVal(_Move.y, Animator_MoveY, ref smoothAnimValVelY);
        }

        protected virtual void Jump() {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * GRAVITY * gravityScale);
        }

        protected virtual void IsGrounded() {
            isGrounded = IsGroundedSphere(groundCollider, Radius, groundMask, debug: true);

            if (isGrounded && !wasGrounded)
                OnGrounded();

            wasGrounded = isGrounded;
            IsFloating = !isGrounded;
        }

        protected virtual void OnGrounded() {

        }

        protected void LookAtMovementDirection(ref Vector3 lastPos) {
            LookAtMovementDirection(ref lastPos, transform);
        }

        protected void LookAtMovementDirection(ref Vector3 lastPos, Transform target, float smoothTime = 10f) {
            var dir = Vector3X.IgnoreY(transform.position - lastPos);
            var rot = dir.magnitude > Mathf.Epsilon ? Quaternion.LookRotation(dir) : Quaternion.LookRotation(Vector3.forward);

            target.rotation = Quaternion.Slerp(target.rotation, rot, smoothTime * Time.deltaTime);
            lastPos = transform.position;
        }

        protected void LimitPositionX(float min, float max) {
            var position = transform.position;
            position.x = Mathf.Clamp(position.x, min, max);
            transform.position = position;
        }

        protected void Rotate(float x) {
            var angleY = transform.eulerAngles.y + x;
            var rotation = Quaternion.AngleAxis(angleY, Vector3.up);
            transform.rotation = rotation;
        }

        protected void LookAt(float angle) {
            LookAt(angle, smoothRotateTime * Time.deltaTime);
        }

        protected void LookAt(float angle, float t) {
            var toward = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toward, t);
        }

        private void OnDrawGizmos() {
            var pos = Vector3X.IgnoreY(transform.position, groundCollider.bounds.min.y);
            Gizmos.DrawWireSphere(pos, Radius);
        }


        protected float GetAnimMoveVal(float move, float currentVal, ref float smoothVal) {
            const float WALK_VAL = 1f;
            const float RUN_VAL = 2f;

            var targetVal = move * (IsRunning ? RUN_VAL : WALK_VAL);
            return Mathf.SmoothDamp(currentVal, targetVal, ref smoothVal, smoothMoveTime);
        }
    }
}
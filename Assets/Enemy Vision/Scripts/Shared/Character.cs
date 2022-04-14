using UnityEngine;
using System.Collections.Generic;
using MarwanZaky.Methods;

namespace MarwanZaky
{
    namespace Shared
    {
        public class Character : MonoBehaviour
        {
            protected bool IsGroundedRaycast(Collider collider, float radius, float groundDistance, LayerMask groundMask, bool debug = false)
            {
                var origins = new Vector3[] {
                    collider.bounds.center,   // middle
                    collider.bounds.center + Vector3.right * -radius, // left
                    collider.bounds.center + Vector3.right * radius,   // right
                    collider.bounds.center + Vector3.forward * -radius,    // backward
                    collider.bounds.center + Vector3.forward * radius,   // foreward
                };

                var maxDistance = groundDistance + collider.bounds.center.y - collider.bounds.min.y;
                var hits = new List<RaycastHit>();

                foreach (var el in origins)
                {
                    var hit = PhysicsX.Raycast(el, Vector3.down, groundMask, maxDistance, debug);
                    hits.Add(hit);
                }

                foreach (var el in hits)
                {
                    if (el.collider != null)
                        return true;
                    continue;
                }

                return false;
            }

            protected bool IsGroundedSphere(Collider collider, float radius, LayerMask groundMask, bool debug = false)
            {
                return IsGroundedSphere(collider.bounds.min.y, radius, groundMask, debug);
            }

            protected bool IsGroundedSphere(float colliderMinY, float radius, LayerMask groundMask, bool debug = false)
            {
                var groundCheckPos = Vector3X.IgnoreY(transform.position, colliderMinY);
                return Physics.CheckSphere(groundCheckPos, radius, groundMask);
            }
        }
    }
}
using UnityEngine;
using MarwanZaky;
using MarwanZaky.Methods;
using System.Collections.Generic;

public class PlayerController : Character
{
    #region Singletone

    public static PlayerController Instance { get; private set; }

    private void Singletone()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    const float FALL_DISTANCE = -5;

    float rotationY;

    Vector3 lastPos;
    Vector2 lastTouchPos;

    [Header("Player Settings"), SerializeField] CharacterController controller;

    protected override float Radius => controller.radius;
    protected override bool IsRunning { get; set; } = true;

    private void Awake()
    {
        Singletone();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
    }

    protected override void Move(Vector3 velocity)
    {
        controller.Move(velocity);
        base.Move(velocity);
    }

    protected override void OnGrounded()
    {
        // Do on grounded
    }

    private void Movement()
    {
        var moveX = transform.right * Speed * Input.GetAxis("Horizontal");
        var moveZ = transform.forward * Speed * Input.GetAxis("Vertical");
        var movement = moveX + moveZ;

        Move(movement * Time.deltaTime);
    }
}
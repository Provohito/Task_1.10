using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float jumpforce;
    public override void Activate()
    {
        base.Activate();

        var _velocity = rBode2D.velocity;
        _velocity.y += Vector2.up.y * jumpforce;
        rBode2D.velocity = _velocity;
    }

    private void FixedUpdate()
    {
        float _horizontalValue = Input.GetAxis("Horizontal");

        playerAnimator.SetBool("jump", true);

        var _velocity = rBode2D.velocity;
        _velocity.x = Vector3.right.x * _horizontalValue * speed;

        if (_velocity.x > 0)
            playerR.flipX = false;
        else if (_velocity.x < 0)
            playerR.flipX = true;

        if (IsGrounded)
        {
            if (_velocity.x == 0)
                NextStateAction.Invoke(PlayerState.Idle);
            else
                NextStateAction.Invoke(PlayerState.Run);
        }
        rBode2D.velocity = _velocity;
    }

    public override PlayerState PlayerState => PlayerState.Jump;
}

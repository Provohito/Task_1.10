using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    [SerializeField]
    float speed = 1;
    private void FixedUpdate()
    {
        float _horizontalValue = Input.GetAxis("Horizontal");
        float _jumpValue = Input.GetAxis("Jump");

        playerAnimator.SetBool("run", true);

        var _velocity = rBode2D.velocity;
        _velocity.x = Vector3.right.x * _horizontalValue * speed;

        if (_velocity.x > 0)
            playerR.flipX = false;
        else if (_velocity.x < 0)
            playerR.flipX = true;

        rBode2D.velocity = _velocity;

        if (IsGrounded)
        {
            if (_jumpValue > 0)
            {
                NextStateAction.Invoke(PlayerState.Jump);
            }
            else if (_velocity.x == 0)
            {
                NextStateAction.Invoke(PlayerState.Idle);
            }
        }
        
    }
    public override PlayerState PlayerState => PlayerState.Run;
}

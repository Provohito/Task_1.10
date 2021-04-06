using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private void FixedUpdate()
    {
        float _horizontalValue = Input.GetAxis("Horizontal");
        float _jumpValue = Input.GetAxis("Jump");

        playerAnimator.SetBool("run", false);
        playerAnimator.SetBool("jump", false);

        if (_horizontalValue != 0)
        {
            NextStateAction.Invoke(PlayerState.Run);
        }
        else if (_jumpValue != 0)
        {
            NextStateAction.Invoke(PlayerState.Jump);
        }
        else
        {
            var _velocity = rBode2D.velocity;
            _velocity.x = 0;
            rBode2D.velocity = _velocity;
        }
    }

    public override PlayerState PlayerState => PlayerState.Idle;
}

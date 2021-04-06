using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    private void FixedUpdate()
    {
        rBode2D.velocity = Vector3.zero;
    }

    public override PlayerState PlayerState => PlayerState.Die;
}

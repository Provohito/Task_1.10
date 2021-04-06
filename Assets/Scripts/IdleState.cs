using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override PlayerState PlayerState => PlayerState.Idle;
}

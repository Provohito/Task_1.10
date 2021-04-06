using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    private void FixedUpdate()
    {
        rBode2D.velocity = Vector3.zero;
        playerAnimator.SetBool("die", true);
        StartCoroutine(GameEnd());
    }
    // вывод экрана об окончании игры
    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(4f);
        UiController.GetComponent<ControllerUI>().DieGame();
    }
    public override PlayerState PlayerState => PlayerState.Die;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    Rigidbody2D rBode2D;
    Animator playerAnimator;
    [SerializeField]
    GameObject UiController;


    public void Setup(Rigidbody2D _rBody2D, Animator _plaeyrAnimator)
    {
        rBode2D = _rBody2D;
        playerAnimator = _plaeyrAnimator;
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            playerAnimator.SetBool("die", true);
            StartCoroutine(GameEnd());
        }
        if (collision.transform.tag == "EndLevel")
        {
            UiController.GetComponent<ControllerUI>().EndGame();
        }
    }


    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(/*dieClip.length +*/ 2f);
        UiController.GetComponent<ControllerUI>().DieGame();
    }
    public abstract PlayerState PlayerState { get; }
}

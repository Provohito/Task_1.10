using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    

    protected Rigidbody2D rBode2D;
    protected Animator playerAnimator;
    protected SpriteRenderer playerR;
    [SerializeField]
    protected GameObject UiController;

    new List<Collider2D> collider2D;
    ContactFilter2D filter2D;

    // Получение параметров для State-a
    public void Setup(Rigidbody2D _rBody2D, Animator _plaeyrAnimator, SpriteRenderer _playerR)
    {
        rBode2D = _rBody2D;
        playerAnimator = _plaeyrAnimator;
        playerR = _playerR;

        collider2D = new List<Collider2D>();
        filter2D = new ContactFilter2D();
        filter2D.SetNormalAngle(89,91);
        filter2D.useNormalAngle = true;
        
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnCollision(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            NextStateAction.Invoke(PlayerState.Die);
            
        }
        if (collision.transform.tag == "EndLevel")
        {
            UiController.GetComponent<ControllerUI>().EndGame();
        }
    }

    protected bool IsGrounded
    {
        get
        {
            bool _value = false;

            int _count = rBode2D.GetContacts(filter2D, collider2D);
            _value = _count > 0;

            return _value;
        }
    }



    public abstract PlayerState PlayerState { get; }

    public Action<PlayerState> NextStateAction { get; set; }
}

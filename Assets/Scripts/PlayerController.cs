using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Die
}
public class PlayerController : MonoBehaviour
{
    [Header("Скорость прыжка на лифте")]
    [SerializeField]
    float jumpForce = 1;

    Rigidbody2D rBode2D;
    Animator playerAnimator;
    SpriteRenderer playerR;
    [Header("Ui controller")]
    [SerializeField]
    GameObject UiController;



    List<BaseState> states;
    BaseState currentState;

    private void Awake()
    {
        rBode2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerR = GetComponent<SpriteRenderer>();

        states = new List<BaseState>(transform.GetComponentsInChildren<BaseState>(true));
        states.ForEach(_state =>
        {
            _state.Setup(rBode2D, playerAnimator, playerR);
            _state.NextStateAction = OnNextStateRequest;
        });

        currentState = states.Find(_state => _state.PlayerState == PlayerState.Idle);
        currentState.Activate();
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Поднятие по лифту
        var _velocity = rBode2D.velocity;
        _velocity.y += Vector2.up.y * 3 * jumpForce;
        rBode2D.velocity = _velocity;
        if (collision.tag == "die")
        {
            UiController.GetComponent<ControllerUI>().DieGame();
        }
    }
    // Передаем объект колиизии в текущий State
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollision(collision);
    }

    // переход к нужному State-у
    void OnNextStateRequest(PlayerState _state)
    {
        currentState.Deactivate();
        currentState = states.Find(_s => _s.PlayerState == _state);
        currentState.Activate();
    }
}

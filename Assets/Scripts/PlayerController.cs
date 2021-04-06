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
    [Header("Скорость и сила прыжка")]
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float jumpForce = 1;

    Rigidbody2D rBode2D;
    Animator playerAnimator;
    [Header("Выбор клипа смерти и UI-контроллера")]
    [SerializeField]
    AnimationClip dieClip;
    [SerializeField]
    GameObject UiController;



    List<BaseState> states;
    BaseState currentState;

    private void Awake()
    {
        rBode2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        states = new List<BaseState>(transform.GetComponentsInChildren<BaseState>(true));
        states.ForEach(_state =>
        {
            _state.Setup(rBode2D, playerAnimator);
        });

        currentState = states.Find(_state => _state.PlayerState == PlayerState.Idle);
        currentState.Activate();
            
    }
    void Start()
    {
       
    }
    PlayerState state;

    void Update()
    {
        float _horizontalValue = Input.GetAxis("Horizontal");
        
        var _velocity = rBode2D.velocity;
        _velocity.x = Vector3.right.x * _horizontalValue * speed;
        if (_horizontalValue != 0 & _horizontalValue > 0)
        {
            playerAnimator.SetBool("run", true);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (_horizontalValue != 0 & _horizontalValue < 0)
        {
            playerAnimator.SetBool("run", true);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        else if(_horizontalValue == 0)
            playerAnimator.SetBool("run", false);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_velocity.y != 0)
            {
                return;
            }
            playerAnimator.SetBool("jump", true);
            _velocity.y += Vector2.up.y * 1 * jumpForce;
        }
        else
            playerAnimator.SetBool("jump", false);



        rBode2D.velocity = _velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var _velocity = rBode2D.velocity;
        _velocity.y += Vector2.up.y * 3 * jumpForce;
        rBode2D.velocity = _velocity;
        if (collision.tag == "die")
        {
            UiController.GetComponent<ControllerUI>().DieGame();
        }
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
        yield return new WaitForSeconds(dieClip.length + 2f);
        UiController.GetComponent<ControllerUI>().DieGame();
    }

    PlayerState State
    {
        get => State;

        set
        {
            State = value;
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : CustomBehavior
{
    public enum PlayerState
    {
        Disable,
        Tutorial,
        Playing,
        Falling,
        Dead
    }
    public PlayerState currentState;
    public float horizontalSpeed, gravitySpeed = 5, flapTime = 0.8f;
    public float increaseSpeedAmount, increaseSpeedStepDuration;
    private float _timeToIncrease = 0, _bonusSpeed = 0;
    private float _timeSinceTop = 0, _peakY;
    private CustomRigidbody customRigidbody;
    public UnityEvent afterDead;


    // Start is called before the first frame update
    void Start()
    {
        customRigidbody = GetComponent<CustomRigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            switch (currentState)
            {
                case PlayerState.Tutorial:
                    ToState((int)PlayerState.Playing);
                    customRigidbody.velocity = new Vector3(customRigidbody.velocity.x, flapTime * gravitySpeed);
                    break;
                case PlayerState.Playing:
                    customRigidbody.velocity = new Vector3(customRigidbody.velocity.x, flapTime * gravitySpeed);
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case PlayerState.Playing:
                _timeToIncrease += Time.fixedDeltaTime;
                CheckIncreaseSpeed();
                break;
        }
    }

    void CheckIncreaseSpeed()
    {
        if (_timeToIncrease > increaseSpeedStepDuration)
        {
            _timeToIncrease -= increaseSpeedStepDuration;
            _bonusSpeed += increaseSpeedAmount;
        }
    }

    void MoveCharacter(float timePassed)
    {
        //transform.rotation = Quaternion.Euler(0, 0, -30 * _timeSinceTop * Mathf.Abs(_timeSinceTop) - 40 * _timeSinceTop);
    }

    public override void OnCollide(CustomCollider collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            ToState((int)PlayerState.Falling);
        }
        if (collider.CompareTag("Floor"))
        {
            ToState((int)PlayerState.Dead);
        }
    }

    public void ToState(int newState)
    {
        currentState = (PlayerState)newState;
        switch (currentState)
        {
            case PlayerState.Playing:
                customRigidbody.accelerate = gravitySpeed * Vector2.down;
                customRigidbody.velocity = horizontalSpeed * Vector2.right;
                break;
            case PlayerState.Falling:
                customRigidbody.velocity = horizontalSpeed * Vector2.left;
                GetComponent<Animator>().SetBool("Falling", true);
                break;
            case PlayerState.Dead:
                customRigidbody.accelerate = Vector2.zero;
                customRigidbody.velocity = Vector2.zero;
                ScoreManager.Instance.SubmiteNewScore();
                GetComponent<Animator>().SetBool("Dead", true);
                if (afterDead != null)
                    afterDead.Invoke();
                break;
        }
    }
}

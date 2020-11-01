using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        _peakY = transform.position.y;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            switch (currentState)
            {
                case PlayerState.Tutorial:
                    ToState((int)PlayerState.Playing);
                    _timeSinceTop = -flapTime - Time.fixedDeltaTime + Time.fixedUnscaledDeltaTime;
                    _peakY = transform.position.y + gravitySpeed * _timeSinceTop * _timeSinceTop / 2;
                    break;
                case PlayerState.Playing:
                    _timeSinceTop = -flapTime - Time.fixedDeltaTime + Time.fixedUnscaledDeltaTime;
                    _peakY = transform.position.y + gravitySpeed * _timeSinceTop * _timeSinceTop / 2;
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
                _timeSinceTop += Time.fixedDeltaTime;
                _timeToIncrease += Time.fixedDeltaTime;
                CheckIncreaseSpeed();
                MoveCharacter(Time.fixedDeltaTime);
                break;
            case PlayerState.Falling:
                _timeSinceTop += Time.fixedDeltaTime;
                MoveCharacter(Time.fixedDeltaTime);
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
        Vector3 newPos = transform.position;
        switch (currentState)
        {
            case PlayerState.Playing:
                newPos += Vector3.right * timePassed * (horizontalSpeed + _bonusSpeed);
                newPos.y = _peakY - gravitySpeed * _timeSinceTop * _timeSinceTop / 2;
                break;
            case PlayerState.Falling:
                newPos += Vector3.left * timePassed * horizontalSpeed / 2;
                newPos.y = _peakY - gravitySpeed * _timeSinceTop * _timeSinceTop / 2;
                break;
        }
        customTransform.position = newPos;
        transform.rotation = Quaternion.Euler(0, 0, -30 * _timeSinceTop * Mathf.Abs(_timeSinceTop) - 40 * _timeSinceTop);
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
            this.enabled = false;
        }
    }

    public void ToState(int newState)
    {
        currentState = (PlayerState)newState;
    }
}

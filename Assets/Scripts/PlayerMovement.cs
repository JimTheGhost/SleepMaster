using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D _controller;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private PlayerHealth _healthController;
    private float _horizontalMove;
    private bool _jump = false;

    [SerializeField] private AudioClip jumpSfx;

    [SerializeField] private float movementSpeed;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthController = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        GetInput();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetAnimatorValues();
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    void GetInput()
    {
        if (!_healthController.isDead)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
                AudioManager.Instance.PlaySoundEffect(jumpSfx);
            }
        }
        else
        {
            _horizontalMove = 0;
            _jump = false;
        }
    }

    private void SetAnimatorValues()
    {
        if (!_healthController.isDead)
        {
            _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
            _animator.SetBool("IsJumping", _jump);
            _animator.SetBool("IsFalling", IsFalling());
        }
        
        _animator.SetBool("IsDead", _healthController.isDead);
    }

    private bool IsFalling()
    {
        return _rigidbody.velocity.y < -0.1;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D _controller;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _horizontalMove;
    private bool _jump = false;

    [SerializeField] private float movementSpeed;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetAnimatorValues();
        _controller.Move(_horizontalMove * Time.fixedDeltaTime,false, _jump);
        _jump = false;
    }

    void GetInput()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }
    }

    private void SetAnimatorValues()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        _animator.SetBool("IsJumping",_jump);
        _animator.SetBool("IsFalling", IsFalling());
    }

    private bool IsFalling()
    {
        return _rigidbody.velocity.y < -0.1;
    }
}

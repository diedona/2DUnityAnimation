using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveHorizontal;
    private bool facingRight;
    private bool attacked;

    public Rigidbody2D _rgbd2d;
    public SpriteRenderer _spriteRenderer;
    public Animator _animator;
    public float _speed = 5;

    // Use this for initialization
    void Start()
    {
        _rgbd2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        attacked = Input.GetButtonDown("Fire1");
        updateAnimation();
    }

    private void FixedUpdate()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Player_attack"))
        {
            Vector3 move = new Vector3(moveHorizontal, 0, 0);
            _rgbd2d.transform.Translate(move * Time.fixedDeltaTime * _speed);
        }
    }

    private void updateAnimation()
    {
        if (moveHorizontal > 0)
        {
            facingRight = true;
        }
        else if (moveHorizontal < 0)
        {
            facingRight = false;
        }

        _spriteRenderer.flipX = facingRight;
        _animator.SetFloat("speed", Math.Abs(moveHorizontal));

        if (attacked && (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Player_attack")))
        {
            _animator.SetTrigger("attack");
        }
    }
}

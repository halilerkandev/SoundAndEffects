using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityModifier;
    private bool _isOnGround = true;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        Physics.gravity *= _gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isOnGround && !isGameOver)
            Jump();
    }

    void Jump()
    {
        _playerRigidbody.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        _isOnGround = false;
        _playerAnimator.SetTrigger("Jump_trig");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            _playerAnimator.SetBool("Death_b", true);
            _playerAnimator.SetInteger("DeathType_int", 1);
        }
    }
}

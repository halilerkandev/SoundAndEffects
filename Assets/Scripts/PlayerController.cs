using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityModifier;
    private bool _isOnGround = true;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= _gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isOnGround)
            Jump();
    }

    void Jump()
    {
        _playerRigidbody.AddForce(10f * Vector3.up, ForceMode.Impulse);
        _isOnGround = false;
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
        }
    }
}

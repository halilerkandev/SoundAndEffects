using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    public float floatForce;
    private float _gravityModifier = 1.5f;
    private Rigidbody _playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource _playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;

    private float _topLimit = 17.0f;

    private bool _isOnGround = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= _gravityModifier;
        _playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        _playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) &&
            !gameOver &&
            transform.position.y < _topLimit &&
            !_isOnGround)
        {
            _playerRb.AddForce(Vector3.up * floatForce);
        }

        if(transform.position.y > _topLimit)
        {
            transform.position = new(transform.position.x,
                                        _topLimit,
                                        transform.position.z);
            _playerRb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            _playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            _playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
            _playerAudio.PlayOneShot(bounceSound, 1.0f);
            Debug.Log("Game Over!");
        }

    }

}

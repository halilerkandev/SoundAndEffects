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
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource _playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= _gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isOnGround && !isGameOver)
        {
            Jump();
            dirtParticle.Stop();
        }
    }

    void Jump()
    {
        _playerRigidbody.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        _isOnGround = false;
        _playerAnimator.SetTrigger("Jump_trig");
        _playerAudioSource.PlayOneShot(jumpSound, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _playerAudioSource.PlayOneShot(crashSound, 1.0f);
            isGameOver = true;
            Debug.Log("Game Over!");
            _playerAnimator.SetBool("Death_b", true);
            _playerAnimator.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();
            explosionParticle.Play();
        }
    }
}

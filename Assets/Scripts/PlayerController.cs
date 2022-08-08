using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityModifier;
    public bool isGameOver = false;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource _playerAudioSource;

    private int _jumpCount = 0;

    public bool isDashMode = false;

    public int score = 0;

    public bool atStartPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= _gravityModifier;
        Debug.Log("Score: " + score);

        _playerAnimator.SetFloat("Speed_f", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 3 && !atStartPoint)
        {
            _playerAnimator.SetFloat("Speed_f", 1.0f);
            atStartPoint = true;
            dirtParticle.Play();
        }

        if (!atStartPoint)
        {
            MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < 2 && !isGameOver)
        {
            Jump();
            dirtParticle.Stop();
        }

        if (Input.GetKey(KeyCode.X))
            Dash();
        else
            Dogtrot();
    }

    void Jump()
    {
        _playerRigidbody.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        _jumpCount++;
        _playerAnimator.SetTrigger("Jump_trig");
        _playerAudioSource.PlayOneShot(jumpSound, 1.0f);
    }

    void Dash()
    {
        if (!isDashMode)
        {
            isDashMode = true;
            var main = dirtParticle.main;
            main.simulationSpeed = 2;
            _playerAnimator.SetBool("Dash_Mode_b", true);
        }
    }

    void Dogtrot()
    {
        if (isDashMode)
        {
            isDashMode = false;
            var main = dirtParticle.main;
            main.simulationSpeed = 1;
            _playerAnimator.SetBool("Dash_Mode_b", false);
        }
    }

    void MoveForward()
    {
        var posX = transform.position.x;
        posX = Mathf.Lerp(posX, 3.5f, Time.deltaTime * 1.2f);
        transform.position = new(posX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && atStartPoint)
        {
            _jumpCount = 0;
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

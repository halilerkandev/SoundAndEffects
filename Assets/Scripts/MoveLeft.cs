using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float _speed;
    private PlayerController _playerControllerInstance;
    [SerializeField] private float _leftBound;

    private bool isPassed = false;

    private bool atStartPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerControllerInstance = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerControllerInstance.atStartPoint && !atStartPoint)
        {
            atStartPoint = true;
        }

        if(atStartPoint)
        {
            float currentSpeed = _speed;

            if (_playerControllerInstance.isDashMode)
                currentSpeed *= 2;

            if (!_playerControllerInstance.isGameOver)
                transform.Translate(currentSpeed * Time.deltaTime * Vector3.left);

            if (transform.position.x < _leftBound && gameObject.CompareTag("Obstacle"))
                Destroy(gameObject);

            if (transform.position.x < 3.0f &&
                gameObject.CompareTag("Obstacle") &&
                !_playerControllerInstance.isGameOver &&
                !isPassed)
            {
                isPassed = true;
                if (_playerControllerInstance.isDashMode)
                    _playerControllerInstance.score += 2;
                else
                    _playerControllerInstance.score++;
                Debug.Log("Score: " + _playerControllerInstance.score);
            }
        }

    }
}

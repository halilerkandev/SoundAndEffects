using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float _speed;
    private PlayerController _playerControllerInstance;
    [SerializeField] private float _leftBound;

    // Start is called before the first frame update
    void Start()
    {
        _playerControllerInstance = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_playerControllerInstance.isGameOver)
            transform.Translate(_speed * Time.deltaTime * Vector3.left);

        if(transform.position.x < _leftBound && gameObject.CompareTag("Obstacle"))
            Destroy(gameObject);
    }
}

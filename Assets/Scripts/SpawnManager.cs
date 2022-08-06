using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    private readonly Vector3 _startPos = 25 * Vector3.right;

    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnRepeatRate;
    private PlayerController _playerControllerInstance;

    // Start is called before the first frame update
    void Start()
    {
        _playerControllerInstance = GameObject.Find("Player").GetComponent<PlayerController>();
        //InvokeRepeating(nameof(SpawnObstacle), _spawnTime, _spawnRepeatRate);
        Invoke(nameof(SpawnObstacle), _spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnObstacle()
    {
        if(!_playerControllerInstance.isGameOver)
        {
            Instantiate(_obstaclePrefab, _startPos, _obstaclePrefab.transform.rotation);
            Invoke(nameof(SpawnObstacle), _spawnRepeatRate);
        }
    }
}

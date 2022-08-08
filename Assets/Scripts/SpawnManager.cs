using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePrefabs;
    private readonly Vector3 _startPos = 25 * Vector3.right;

    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnRepeatRate;
    private PlayerController _playerControllerInstance;

    private bool atStartPoint = false;
    private bool invoked = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerControllerInstance = GameObject.Find("Player").GetComponent<PlayerController>();
        //InvokeRepeating(nameof(SpawnObstacle), _spawnTime, _spawnRepeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (!atStartPoint && _playerControllerInstance.atStartPoint)
            atStartPoint = true;

        if (atStartPoint && !invoked)
        {
            invoked = true;
            Invoke(nameof(SpawnObstacle), _spawnTime);
        }
    }

    void SpawnObstacle()
    {
        if(!_playerControllerInstance.isGameOver && _playerControllerInstance.atStartPoint)
        {
            int index = Random.Range(0, _obstaclePrefabs.Length);
            Instantiate(_obstaclePrefabs[index], _startPos, _obstaclePrefabs[index].transform.rotation);
            Invoke(nameof(SpawnObstacle), _spawnRepeatRate);
        }
    }
}

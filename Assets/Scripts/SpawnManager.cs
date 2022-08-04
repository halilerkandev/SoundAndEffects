using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    private readonly Vector3 _startPos = 25 * Vector3.right;

    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnRepeatRate;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), _spawnTime, _spawnRepeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        Instantiate(_obstaclePrefab, _startPos, _obstaclePrefab.transform.rotation);
    }
}

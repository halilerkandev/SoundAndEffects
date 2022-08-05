using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 _startPos;
    private BoxCollider _boxCollider;
    private float _repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _boxCollider = GetComponent<BoxCollider>();
        _repeatWidth = _boxCollider.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= _startPos.x - _repeatWidth)
        {
            transform.position = _startPos;
        }
    }
}

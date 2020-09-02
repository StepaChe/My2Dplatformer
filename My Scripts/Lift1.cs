using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift1 : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private int _direction;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3 _curDirection;
    private float _prevMagnitude = 0;

    void Start()
    {
        _startPos = transform.position;
        _endPos = new Vector3(_direction == 1 ? _startPos.x : _startPos.x + _distance, _direction == 1 ? _startPos.y + _distance : _startPos.y);
        if (_direction == 1)
        {
            if (_startPos.y > _endPos.y)
                _curDirection = transform.up * -1f;
            else
                _curDirection = transform.up;
        }
        else if (_direction == 2)
        {
            if (_startPos.x > _endPos.x)
                _curDirection = transform.right * -1f;
            else
                _curDirection = transform.up;
        }
        _prevMagnitude = (_startPos - _endPos).sqrMagnitude;
    }

    private void Update()
    {
        if (Mathf.Abs((transform.position - _endPos).sqrMagnitude) > Mathf.Abs(_prevMagnitude))
        {
            var temp = _startPos;
            _startPos = _endPos;
            _endPos = temp;
            _curDirection *= -1;
        }
        _prevMagnitude = (transform.position - _endPos).sqrMagnitude;
        transform.position += _curDirection * _speed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour,IDragHandler
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Transform _player;
    [SerializeField] private float _sensivity=0.005f;
    [SerializeField] private float _speed = 7f;
    private Vector3 _currentPos;
    public void OnDrag(PointerEventData eventData)
    {
        _currentPos = _player.position;
        _currentPos.x = Mathf.Clamp(_player.position.x + eventData.delta.x * _sensivity, -3.4f, 3.1f);
        _player.position = _currentPos;
    }
    void Update()
    {
        if (_playerManager.IsStart)
        {
             _player.position += _speed * Time.deltaTime * Vector3.forward;
        }   
    }
}

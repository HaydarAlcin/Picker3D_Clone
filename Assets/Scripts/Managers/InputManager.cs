using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    //Self Variables
    private InputData _data;
    private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;


    private float _currentVelocity;
    private Vector3 _moveVector; //
    private Vector2? _mousePosition; //ref type


    private void Awake()
    {
        _data = GetInputData();
    }

    private InputData GetInputData()
    {
        return Resources.Load<CD_Input>(path: "Data/CD_Input").InputData;
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onReset += OnReset;
        InputSignals.Instance.onEnableInput += OnEnableInput;
        InputSignals.Instance.onDisableInput += OnDisableInput;
    }

    private void OnEnableInput()
    {
        _isAvailableForTouch = true;
    }

    private void OnDisableInput()
    {
        _isAvailableForTouch = false;
    }

    private void OnReset()
    {
        _isAvailableForTouch = false;
        //_isFirstTimeTouchTaken = false;
        _isTouching = false;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onReset -= OnReset;
        InputSignals.Instance.onEnableInput -= OnEnableInput;
        InputSignals.Instance.onDisableInput -= OnDisableInput;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void Update()
    {
        if (!_isAvailableForTouch) return;

        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
        }

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }
            _mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            if (_isTouching)
            {
                if (_mousePosition !=null)
                {
                    Vector2 mouseDeltaPos= (Vector2)Input.mousePosition - _mousePosition.Value;
                    if (mouseDeltaPos.x > _data.horizontalInputSpeed)
                    {
                        _moveVector.x = _data.horizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }

                    else if(mouseDeltaPos.x < _data.horizontalInputSpeed) 
                    {
                        _moveVector.x = -_data.horizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }

                    else
                    {
                        _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0, ref _currentVelocity, _data.clampSpeed);
                    }

                    _mousePosition = Input.mousePosition;

                    InputSignals.Instance.onInputDragged(new HorizontalInputParams
                    {
                        horizontalValue = _moveVector.x,
                        ClampValues = _data.ClampValues,
                    });
                }
            }
        }


    }

    //UI
    private bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        var result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);
        return result.Count > 0;
    }

}

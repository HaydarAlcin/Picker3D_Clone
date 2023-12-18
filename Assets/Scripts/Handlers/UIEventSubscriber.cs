using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventSubscriber : MonoBehaviour
{

    [SerializeField] private UIEventSubscriptionTypes types;
    [SerializeField] private Button button;

    private UIManager _manager;

    private void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        _manager = FindObjectOfType<UIManager>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        switch (types)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.AddListener(_manager.Play);
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.AddListener(_manager.NextLevel);
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.AddListener(_manager.RestartLevel);
                break;
        }
    }


    private void UnSubscribeEvents()
    {
        switch (types)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.RemoveListener(_manager.Play);
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.RemoveListener(_manager.NextLevel);
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.RemoveListener(_manager.RestartLevel);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}

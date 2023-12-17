using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Rendering.DebugUI;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private List<Transform> layers = new List<Transform>();

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
        CoreUISignals.Instance.onClosePanel += OnClosePanel;
        CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanel;

    }

    private void OnOpenPanel(UIPanelTypes panelType,int value)
    {
        Instantiate(Resources.Load<GameObject>(path:$"Screens/{panelType}Panel"), layers[value]);
    }

    private void OnClosePanel(int value)
    {
        if (layers[value].childCount <= 0) return;
       
        //DestroyImmediate(layers[value].GetChild(0).gameObject);
        Destroy(layers[value].GetChild(0).gameObject);
        
    }

    private void OnCloseAllPanel()
    {
        foreach (var layer in layers)
        {
            if (layers.Count <= 0) return;

            Destroy(layer.GetChild(0).gameObject);
        }
    }


    private void UnSubscribeEvents()
    {
        CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
        CoreUISignals.Instance.onClosePanel -= OnClosePanel;
        CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanel;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

}

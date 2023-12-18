using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onReset += OnReset;
        CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
    }

    private void OnLevelInitialize(byte i)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level,0);
        UISignals.Instance.onSetLevelValue?.Invoke((byte)CoreGameSignals.Instance.onGetLevelValue?.Invoke());
    }

    private void OnLevelSuccessful()
    {
        CoreUISignals.Instance.onOpenPanel(UIPanelTypes.Win, 2);
        Debug.Log("win");
    }

    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel(UIPanelTypes.Fail, 2);
    }

    private void OnReset()
    {
        CoreUISignals.Instance.onCloseAllPanels?.Invoke();
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start,1);
    }

    private void OnStageAreaSuccessful(byte stageValue)
    {
        UISignals.Instance.onSetStageColor?.Invoke(stageValue);
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onReset -= OnReset;
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
    }


    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    public void Play()
    {
        UISignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1);
        InputSignals.Instance.onEnableInput?.Invoke();
        CameraSignals.Instance.onSetCameraTarget?.Invoke();
    }

    public void NextLevel()
    {
        CoreGameSignals.Instance.onNextLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(2);
    }

    public void RestartLevel()
    {
        CoreUISignals.Instance.onClosePanel?.Invoke(2);
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
    }
}

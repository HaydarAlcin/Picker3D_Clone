using System;
using System.Collections;
using System.Collections.Generic;
using Data.ValueObjects;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Public Variables

    public byte StageValue;

    internal ForceBallsToPoolCommand ForceCommand;

    #endregion

    #region Serialized Variables

    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerMeshController meshController;
    [SerializeField] private PlayerPhysicsController physicsController;

    #endregion


    private PlayerData _data;

    private void Awake()
    {
        _data = GetPlayerData();
        SendDataToControllers();
        Init();
    }

    private PlayerData GetPlayerData()
    {
        return Resources.Load<CD_Player>(path: "Data/CD_Player").Data;
    }

    private void SendDataToControllers()
    {
        movementController.SetData(_data.MovementData);
        meshController.SetData(_data.MeshData);
    }


    public void Init()
    {
        ForceCommand = new ForceBallsToPoolCommand(this, _data.ForceData);
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        //Lambda could be used
        InputSignals.Instance.onInputTaken += OnInputTaken;
        InputSignals.Instance.onInputReleased += OnInputReleased;
        InputSignals.Instance.onInputDragged += OnInputDragged;
        UISignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
        CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
        CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset += OnReset;
    }
    private void OnInputTaken()
    {
        movementController.IsReadyToMove(true);
    }

    private void OnInputReleased()
    {
        movementController.IsReadyToMove(false);
    }

    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        movementController.UpdateInputParams(inputParams);
    }

    private void OnPlay()
    {
        movementController.IsReadyToPlay(true);
    }

    private void OnLevelFailed()
    {
        movementController.IsReadyToPlay(false);
    }

    private void OnLevelSuccessful()
    {
        movementController.IsReadyToPlay(false);
    }

    private void OnFinishAreaEntered()
    {
        CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
    }

    private void OnStageAreaSuccessful(byte stageValue)
    {
        StageValue = (byte)++stageValue;
        movementController.IsReadyToPlay(true);
        meshController.ScaleUpPlayer();
        meshController.ShowUpText();
        
    }

    private void OnStageAreaEntered()
    {
        movementController.IsReadyToPlay(false);
    }

    private void OnReset()
    {
        StageValue = 0;
        movementController.OnReset();
        meshController.OnReset();
        physicsController.OnReset();
    }


    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= OnInputTaken;
        InputSignals.Instance.onInputReleased -= OnInputReleased;
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        UISignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
        CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}

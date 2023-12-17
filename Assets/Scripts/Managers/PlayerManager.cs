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

    #endregion


    private PlayerData _data;

    private void Awake()
    {
        _data = GetPlayerData();
    }

    private PlayerData GetPlayerData()
    {
        return Resources.Load<CD_Player>(path: "Data/CD_Player").Data;
    }
}

using System.Collections.Generic;
using Data.UnityObjects;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PoolController : MonoBehaviour
{
    #region Variables

    [SerializeField] private List<Transform> animatedChilds = new List<Transform>();
    [SerializeField] private TextMeshPro poolText;
    [SerializeField] private byte stageID;
    [SerializeField] private new Renderer renderer;

    private PoolData _data;
    private int _collectedCount;
    private const string _collectable = "Collectable";

    #endregion

    private void Awake()
    {
        _data = GetPoolData();
    }

    private PoolData GetPoolData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()]
            .Pools[stageID];
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
        CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
    }

    private void OnChangePoolColor(byte stageValue)
    {
        if (stageValue != stageID) return;
        if (renderer==null) return;
        renderer.material.DOColor(new Color(0.2597365f, 0.4909722f, 0.9056604f), 1f).SetEase(Ease.Linear);
    }

    private void OnActivateTweens(byte stageValue)
    {
        if (stageValue !=stageID) return;

        foreach (var child in animatedChilds)
        {
            if (child == null) continue;  
            child.GetComponent<PoolChildController>().OnPlay();
        }
      
    }

    private void Start()
    {
        SetRequiredAmountText();
    }

    private void SetRequiredAmountText()
    {
        poolText.text = $"0/{_data.RequiredObjectCount}";
    }

    public bool TakeResults(byte managerStageValue)
    {
        if (stageID == managerStageValue)
        {
            return _collectedCount >= _data.RequiredObjectCount;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_collectable))return;

        UpdateCollectedAmount(1);
        SetCollectedAmountToPool();
    }

    private void SetCollectedAmountToPool()
    {
        poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
    }

    private void UpdateCollectedAmount(int collectedCount)
    {
        _collectedCount+= collectedCount;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_collectable)) return;
        UpdateCollectedAmount(-1);
        SetCollectedAmountToPool();
    }
}

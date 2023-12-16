using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals : MonoBehaviour
{
    //Singleton
    public static CoreGameSignals Instance;

    //Actions
    public UnityAction<byte> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onReset = delegate { };
    public Func<byte> onGetLevelValue = delegate { return 0; };

    private void Awake()
    {
        if (Instance != null && Instance !=this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
}

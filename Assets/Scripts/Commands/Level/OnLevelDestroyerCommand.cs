using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelDestroyerCommand
{
    private Transform _levelHolder;

    public OnLevelDestroyerCommand(Transform levelHolder)
    {
        _levelHolder=levelHolder;
    }

    //In CommandPattern, functions are generally not given special names; they are called Execute. (Translation Sentence)
    public void Execute()
    {
        if (_levelHolder.transform.childCount <= 0) return;
        Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
    }
}

using Data.ValueObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelLoaderCommand
{
    private Transform _levelHolder;


    public OnLevelLoaderCommand(Transform levelHolder)
    {
        _levelHolder = levelHolder;
    }

    //In CommandPattern, functions are generally not given special names; they are called Execute. (Translation Sentence)
    public void Execute(byte levelIndex)
    {
        Object.Instantiate(original:Resources.Load<GameObject>(path: $"Prefabs/LevelPrefabs/level {levelIndex}"),_levelHolder);

    }
}

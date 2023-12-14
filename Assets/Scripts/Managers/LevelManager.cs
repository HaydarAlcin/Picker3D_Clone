using Data.UnityObjects;
using Data.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //Serialized Variables
    [SerializeField] private Transform levelHolder;
    [SerializeField] private byte totalLevelCount;

    //Private Variables
    private OnLevelLoaderCommand _levelLoaderCommand;
    private OnLevelDestroyerCommand _levelDestroyerCommand;

    private byte _currentLevel;
    private LevelData _levelData;

    private void Awake()
    {
        _levelData = GetLevelData();
        _currentLevel = GetActiveLevel();

        Init();
    }

    private void Init()
    {
        _levelLoaderCommand=new OnLevelLoaderCommand(levelHolder);
        _levelDestroyerCommand=new OnLevelDestroyerCommand(levelHolder);
    }


    private LevelData GetLevelData()
    {
        return Resources.Load<CD_Level>(path: "Data/CD_Level").Levels[_currentLevel];
    }

    private byte GetActiveLevel()
    {
       return _currentLevel;
    }

   
}

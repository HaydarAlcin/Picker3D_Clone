using Data.UnityObjects;
using Data.ValueObjects;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] private Transform levelHolder;
    [SerializeField] private byte totalLevelCount;

    //Private Variables
    private OnLevelLoaderCommand _levelLoaderCommand;
    private OnLevelDestroyerCommand _levelDestroyerCommand;

    private byte _levelID;
    private byte _currentLevel;
    private LevelData _levelData;

    private void Awake()
    {
        _levelData = GetLevelData();
        _levelID = GetCurrentLevel();

        Init();
    }

    private void Init()
    {
        _levelLoaderCommand=new OnLevelLoaderCommand(levelHolder);
        _levelDestroyerCommand=new OnLevelDestroyerCommand(levelHolder);
    }


    private LevelData GetLevelData()
    {
        return Resources.Load<CD_Level>(path: "Data/CD_Level").Levels[_levelID];
    }

    //private byte GetActiveLevel()
    //{
    //    return (byte)_levelID;
    //}

    private byte GetCurrentLevel()
    {
        return Resources.Load<CD_Level>(path: "Data/CD_Level").CurrentLevel;
    }

    //private byte SetActiveLevel()
    //{
        
    //}
    


    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }


    //Adding listeners
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }

    private void OnNextLevel()
    {
        _levelID++;
        Resources.Load<CD_Level>(path: "Data/CD_Level").CurrentLevel = _levelID;
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_levelID));

    }

    private void OnRestartLevel()
    {
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_levelID));
    }

    private byte OnGetLevelValue()
    {
        return (byte)_levelID;
    }

    //Removing listeners
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
    }


    private void Start()
    {
        if (_levelID>=4)
        {
            _levelID = 0;
        }
        CoreGameSignals.Instance.onLevelInitialize?.Invoke( _levelID);
        //UISignals
        CoreUISignals.Instance.onOpenPanel(UIPanelTypes.Start, 1);
    }

    


}

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

    private short _currentLevel;
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
       return (byte) _currentLevel;
    }

    


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
        _currentLevel++;
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));

    }

    private void OnRestartLevel()
    {
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }

    private byte OnGetLevelValue()
    {
        return (byte)_currentLevel;
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
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte) (_currentLevel %totalLevelCount));

        //UISignals
        CoreUISignals.Instance.onOpenPanel(UIPanelTypes.Start, 0);
    }

    


}

using UnityEngine;
using UnityEngine.Events;

public class CameraSignals : MonoBehaviour
{
    #region Singleton
    public static CameraSignals Instance;
    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    #endregion



    public UnityAction onSetCameraTarget = delegate { };
}

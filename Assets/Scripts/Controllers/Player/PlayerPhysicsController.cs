using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    #region Variables

    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Collider collider;
    [SerializeField] private new Rigidbody rigidbody;


    #endregion

    #region Private Variables

    private const string STAGE_AREA = "StageArea";
    private const string FINISH = "FinishArea";
    

    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(STAGE_AREA))
        {
            manager.ForceCommand.Execute();
            CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();

            DOVirtual.DelayedCall(3, (() =>
            {
                var result = other.transform.parent.GetComponentInChildren<PoolController>().TakeResults(manager.StageValue);

                if (result)
                {
                    CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                    InputSignals.Instance.onEnableInput?.Invoke();
                }
                else
                {
                    CoreGameSignals.Instance.onLevelFailed?.Invoke();
                }
            }));

            return;
        }

        if (other.CompareTag(FINISH))
        {
            CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
        }

        //mini game
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    var transform1 = manager.transform;
    //    var position1 = transform1.position;

    //    Gizmos.DrawSphere(new Vector3(position1.x,position1.y,position1.z+.5f),1.2f);
    //}

    internal void OnReset()
    {

    }
}

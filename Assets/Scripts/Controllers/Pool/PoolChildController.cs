using DG.Tweening;
using UnityEngine;

public class PoolChildController : MonoBehaviour
{

    [SerializeField] PoolChild poolChildType;

    public void OnPlay()
    {
        switch (poolChildType)
        {
            case PoolChild.Door:
                Vector3 rotation = transform.rotation.eulerAngles;
                transform.DOLocalRotate(new Vector3(110,rotation.y, rotation.z), 1f);
                break;
            case PoolChild.Plane:
                transform.DOLocalMoveY(0, .3f);
                break;
        }
    }
}

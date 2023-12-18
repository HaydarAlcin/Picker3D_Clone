using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.ValueObjects;
using UnityEngine;

public class ForceBallsToPoolCommand
{
    private PlayerManager _playerManager;
    private PlayerForceData _playerForceData;

    public ForceBallsToPoolCommand(PlayerManager manager, PlayerForceData forceData)
    {
        _playerForceData=forceData;
        _playerManager = manager;
    }

    internal void Execute()
    {
        var transform1 = _playerManager.transform;
        var position1 = transform1.position;
        var forcePosition = new Vector3(position1.x, position1.y, position1.z + .5f);

        var collider = Physics.OverlapSphere(forcePosition, 1.25f);
        var collectableColliderListColliderList = collider.Where(col => col.CompareTag("Collectable")).ToList();

        foreach (var col in collectableColliderListColliderList)
        {
            if (col.GetComponent<Rigidbody>()==null)
            {
                continue;
            }

            var rb = col.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0,_playerForceData.forceParameters.y, _playerForceData.forceParameters.z),ForceMode.Impulse);

        }

        collectableColliderListColliderList.Clear();
    }
}

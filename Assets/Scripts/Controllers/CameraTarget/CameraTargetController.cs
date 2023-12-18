using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform playerTransform;
    #endregion


    private void Update()
    {
        Vector3 currentTransform=transform.position;
        Vector3 playerPosition = playerTransform.position;
        transform.position = new Vector3(currentTransform.x, currentTransform.y, playerPosition.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform follow;

    private Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        //Vector3 derivedPosition = follow.transform.position;
        tr.position = follow.transform.position;
        //tr.position = Vector3.Lerp(tr.position, derivedPosition, 0.125f);
    }
}

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
        tr.position = follow.transform.position;
    }
}

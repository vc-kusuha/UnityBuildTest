using System;
using UnityEngine;

public sealed class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateEulers;

#if !DEBUG
    private void Start()
    {
        死
    }
#endif

    private void Update()
    {
        transform.Rotate(_rotateEulers, Space.World);
    }
}
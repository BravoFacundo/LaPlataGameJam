using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCameraController : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    private Vector3 offset;

    void Start()
    {
        ballTransform = transform.parent;
        transform.parent = null;

        offset = transform.position - ballTransform.position;
    }

    void Update()
    {
        transform.position = new Vector3(
            ballTransform.position.x + offset.x,
            ballTransform.position.y + offset.y,
            ballTransform.position.z + offset.z);
    }
}

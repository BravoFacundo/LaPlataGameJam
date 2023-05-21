using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentAtAwake : MonoBehaviour
{
    private void Awake()
    {
        if (transform.parent != null) transform.parent = null;
    }
}

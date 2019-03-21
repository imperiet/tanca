using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotate : MonoBehaviour
{
    [SerializeField] float speed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * speed);
    }
}

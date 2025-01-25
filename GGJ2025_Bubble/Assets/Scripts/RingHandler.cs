using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHandler : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 Velocity { get; private set; }

    private void Update() {

        Velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }
}

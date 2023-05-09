using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStart : MonoBehaviour
{
    public Vector3 startPos;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            startPos = other.transform.position;
    }
}

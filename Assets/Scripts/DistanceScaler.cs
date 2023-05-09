using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceScaler : MonoBehaviour
{
    [Header("Components")]
    public Transform target;

    [Header("Parameters")]

    public LayerMask targetMask;
    public LayerMask ignoreTargetMask;
    public float offsetFactor;

    float originalDistance;
    float originalScale;
    Vector3 targetScale;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        ResizeTarget();

    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
          
            if (target == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Camera.main.transform.forward * 100, out hit, Mathf.Infinity, targetMask))
                {
                    Debug.Log("Rayo");
                    //target.GetComponent<Rigidbody>().useGravity = false;
                    target = hit.transform; 
                    target.GetComponent<Rigidbody>().useGravity = false;
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    //target.GetComponent<Rigidbody>().useGravity = false;
                    originalDistance = Vector3.Distance(transform.position, target.position);
                    originalScale = target.localScale.x;
                    targetScale = target.localScale;
                    Debug.Log(hit.transform);
                }
            }
            else
            {
                target.GetComponent<Rigidbody>().isKinematic = false;
                target.GetComponent<Rigidbody>().useGravity = true;
                target = null;
            }
        }
    }

    void ResizeTarget()
    {
        if (target == null)
        {
            return;
        }

        RaycastHit hit; 
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            target.position = hit.point - transform.forward * offsetFactor * targetScale.x;

            float currentDistance = Vector3.Distance(transform.position, target.position);
            float s = currentDistance / originalDistance;
            targetScale.x = targetScale.y = targetScale.z = s;

            target.transform.localScale = targetScale * originalScale;
        }
    }
}

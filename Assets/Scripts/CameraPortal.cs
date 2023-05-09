using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPortal : MonoBehaviour
{
    public Transform otherPortal;
    public GameObject player1;
    public Camera otherCam;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion direction = Quaternion.Inverse(transform.rotation) * Camera.main.transform.rotation;

        otherCam.transform.localEulerAngles = new Vector3(direction.eulerAngles.x, direction.eulerAngles.y + 180, direction.eulerAngles.z);

        Vector3 distance = transform.InverseTransformPoint(Camera.main.transform.position);

        otherCam.transform.localPosition = - new Vector3(distance.x, -distance.y, distance.z);

        player1 = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.tag == "Player")
        {
   
            Vector3 PlayerFromPortal = transform.InverseTransformPoint(player1.transform.position);


            if (PlayerFromPortal.z <= .02)
            {
                //player1.GetComponent<CharacterController>().enabled = false;

                player1.transform.position = otherPortal.position + new Vector3(-PlayerFromPortal.x, +PlayerFromPortal.y, -PlayerFromPortal.z);

                Quaternion ttt = Quaternion.Inverse(transform.rotation) * player1.transform.rotation;
                player1.transform.eulerAngles = Vector3.up * (otherPortal.eulerAngles.y - (transform.eulerAngles.y - player1.transform.eulerAngles.y) + 180);
                Vector3 camLEA = Camera.main.transform.localEulerAngles;
                Camera.main.transform.eulerAngles = Vector3.right * (otherPortal.eulerAngles.x + Camera.main.transform.position.x);
           
                
                Vector3 velocityLocalPlayer = transform.InverseTransformPoint(rb.velocity);
                rb.velocity = -otherPortal.transform.forward * rb.velocity.y * 2;

            Debug.Log("Teletransportado");
            }

        }
    }
}

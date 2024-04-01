using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]LayerMask playerMask;
    [SerializeField] float jumpSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        if(Extension.Contain(playerMask, collision.gameObject.layer))
        {
            Rigidbody playerRb=collision.gameObject.GetComponent<Rigidbody>();

            playerRb.AddForce(Vector3.up*jumpSpeed,ForceMode.Impulse);
        }
            
    }


    
}

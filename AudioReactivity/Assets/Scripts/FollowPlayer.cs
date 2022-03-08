using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;

    public float fallMultiplier = 2.5f;

    public float lowJumpMultiplier = 2f;

    [Range(1, 10)]
    public float jumpVelocity;

    // public GameObject col;
    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Player.transform.localPosition;

        if(rb.velocity.y <0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier) *Time.deltaTime;
        }else if(rb.velocity.y >0 && !Input.GetButton("Jump")){
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier) *Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump")){
            rb.velocity = Vector3.up * jumpVelocity;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float rollSpeed = 3;
    private bool isMoving;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Range(1,10)]
    public float jumpVelocity;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving) return;

        if(Input.GetKey(KeyCode.A)) Assemble(Vector3.left);
        if(Input.GetKey(KeyCode.D)) Assemble(Vector3.right);
        // if(Input.GetKey(KeyCode.W)) Assemble(Vector3.forward);
        // if(Input.GetKey(KeyCode.S)) Assemble(Vector3.back);

        void Assemble(Vector3 dir){
            var anchor = transform.position + (Vector3.down +dir) *0.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));
        }

        // if(rb.velocity.y <0){
        //     rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier) *Time.deltaTime;
        // }else if(rb.velocity.y >0 && !Input.GetButton("Jump")){
        //     rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier) *Time.deltaTime;
        // }

        // if(Input.GetButtonDown("Jump")){
        //     rb.velocity = Vector3.up * jumpVelocity;
        // }
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis){
        isMoving = true;
        for (int i =0;i<(90/rollSpeed);i++){
            transform.RotateAround(anchor,axis,rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        isMoving = false;
    }
}

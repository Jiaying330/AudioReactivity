using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public GameObject Col;
    public float rollSpeed = 3;
    private bool isMoving;

    public float velocity;

    public bool isOnGround = false;
    public bool isFalling = true;




    // Start is called before the first frame update
    void Start()
    {
        // velocity = 0.01f;
        
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Platform"){
            isOnGround = true;
        }
        Debug.Log("in");
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0,Col.transform.position.y,0);

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

        
    }

    
    IEnumerator Roll(Vector3 anchor, Vector3 axis){
        isMoving = true;
        for (int i =0;i<(90/rollSpeed);i++){
            transform.RotateAround(anchor,axis,rollSpeed);
            yield return new WaitForSeconds(0.00f);
        }
        isMoving = false;
    }
    
}

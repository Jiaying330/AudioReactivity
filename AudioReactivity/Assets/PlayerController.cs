using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    LayerMask groundLayers;

    [SerializeField]
    private float runSpeed = 8f;

    [SerializeField]
    private float jumpHeight = 2f;

    private float gravity = -50f;

    private CharacterController characterController;

    private Vector3 velocity;

    [SerializeField]
    private bool isGrounded;

    private float horizontalInput;

    private bool jumpPressed;

    private float jumpTimer;

    private float jumpGracePeriod = 0.2f;

    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 1;
        transform.forward =
            new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);
        isGrounded =
            Physics
                .CheckBox(transform.position - new Vector3(0.0f,0.1f,0.0f),
                new Vector3(1,1,1)/2,
                Quaternion.identity,
                groundLayers,
                QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        characterController
            .Move(new Vector3(horizontalInput * runSpeed, 0, 0) *
            Time.deltaTime);

        //jumping
        jumpPressed = Input.GetButtonDown("Jump");

        if (jumpPressed)
        {
            jumpTimer = Time.time;
        }
        if (
            isGrounded &&
            (
            jumpPressed ||
            (jumpTimer > 0 && Time.time < jumpTimer + jumpGracePeriod)
            )
        )
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpTimer = -1;
        }

        //vertical velocity
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        spawnManager.SpawnTriggerEntered();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    LayerMask groundLayers;

    [SerializeField]
    public float runSpeed;
    private float saveSpeed;

    [SerializeField]
    private float jumpHeight = 2f;

    private float gravity = -30f;

    private CharacterController characterController;

    private Vector3 velocity;

    [SerializeField]
    private bool isGrounded;

    // private bool isMoving;
    private float horizontalInput;

    private bool jumpPressed;

    private float jumpTimer;

    private float jumpGracePeriod = 0.2f;

    public SpawnManager spawnManager;

    public int Count;

    public TextMeshProUGUI countDisplay;

    public GameObject explode;

    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true; //allows unity to update when not in focus

        //************* Instantiate the OSC Handler...
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playseq", 1);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/walk", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/volum", 0.1f);

        //reset
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count1", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count2", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count3", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count4", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count5", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count6", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count7", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count8", 0);

        // start the PD file
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/start", 0);
        saveSpeed = runSpeed;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 1;
        transform.forward =
            new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);
        isGrounded =
            Physics.CheckBox(transform.position - new Vector3(0.0f, 0.1f, 0.0f), new Vector3(1, 1, 1) / 2, Quaternion.identity, groundLayers, QueryTriggerInteraction.Ignore);

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
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/jump", 1);
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

        OSCHandler
                .Instance
                .SendMessageToClient("pd",
                "/unity/count" + Count.ToString(),
                1);

        if (this.transform.position.y <= -2)
        {
            runSpeed = 0;
            velocity.y = 0;
            deathRespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("spawnTrigger"))
        {
            spawnManager.SpawnTriggerEntered();
        }

        if (other.gameObject.CompareTag("Note"))
        {
            Count++;
            countDisplay.text = "Count: " + Count.ToString();
            other.gameObject.SetActive(false);

            //************* Send the message to the client...
            OSCHandler
                .Instance
                .SendMessageToClient("pd", "/unity/trigger", Count);

            //*************

            // Play Collectable SFX
            OSCHandler
                .Instance
                .SendMessageToClient("pd", "/unity/collected", 1);
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("died");
            Instantiate(explode, gameObject.transform.position, explode.transform.rotation);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other);
            runSpeed = 0;
            deathRespawn();
        }

        if (other.gameObject.CompareTag("Block"))
        {
            // isGrounded = true;
            saveSpeed = runSpeed;
            runSpeed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            // isGrounded=false;
            runSpeed = saveSpeed;
        }
    }

    public void deathRespawn()
    {
        StartCoroutine(deathRespawnWait());
    }

    IEnumerator deathRespawnWait()
    {
        isDead = true;
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/start", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/death", 1);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/gameOver", 1);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.transform.position = new Vector3(transform.position.x+1, 2, 0);
        
        yield return new WaitForSeconds(1f);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/respawn", 1);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/gameStart", 1);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/start", 1);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        isDead = false;
        runSpeed = saveSpeed;
    }
}

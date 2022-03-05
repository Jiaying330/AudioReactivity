using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//************** use UnityOSC namespace...
using UnityOSC;

//*************
public class MovePlayer : MonoBehaviour
{
    public float speed;

    public Text countText;

    private Rigidbody rb;

    public int count;

    int ismoving = 0;

    //************* Need to setup this server dictionary...
    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();

    //*************
    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true; //allows unity to update when not in focus

        //************* Instantiate the OSC Handler...
        OSCHandler.Instance.Init();
        OSCHandler
            .Instance
            .SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playseq", 1);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/walk", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/volum", 0.1f);

        //*************
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();

        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count1", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count2", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count3", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count4", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count5", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count6", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count7", 0);
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/count8", 0);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Debug.Log(rb.position.x);
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        Debug.Log (movement);

        rb.AddForce(movement * speed);

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            Debug.Log("Here1");
            ismoving = 0;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/walk", 0);
        }
        else
        {
            Debug.Log("Here2");
            if (ismoving == 0)
            {
                ismoving = 1;
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/walk", 1);
            }
        }

        //************* Routine for receiving the OSC...
        OSCHandler.Instance.UpdateLogs();
        Dictionary<string, ServerLog> servers =
            new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;

        foreach (KeyValuePair<string, ServerLog> item in servers)
        {
            // If we have received at least one packet,
            // show the last received from the log in the Debug console
            if (item.Value.log.Count > 0)
            {
                int lastPacketIndex = item.Value.packets.Count - 1;

                //get address and data packet
                countText.text =
                    item.Value.packets[lastPacketIndex].Address.ToString();
                countText.text +=
                    item.Value.packets[lastPacketIndex].Data[0].ToString();
            }
        }
        //*************
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("-------- COLLISION!!! ----------");
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();

            if (count == 1)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count1", 1);
            }

            // change the tempo of the sequence based on how many obejcts we have picked up.
            if (count == 2)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count2", 1);
            }
            if (count == 3)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count3", 1);
            }
            if (count == 4)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count4", 1);
            }
            if (count == 5)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count5", 1);
            }
            if (count == 6)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count6", 1);
            }
            if (count == 7)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count7", 1);
            }
            if (count == 8)
            {
                OSCHandler
                    .Instance
                    .SendMessageToClient("pd", "/unity/count8", 1);
            }
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("-------- HIT THE WALL ----------");

            // trigger noise burst whe hitting a wall.
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/colwall", 1);
        }

        if (other.gameObject.CompareTag("Spike"))
        {
            transform.localPosition = new Vector3(0, 0.5f, 0);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();

        //************* Send the message to the client...
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", count);
        //*************
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Logic or Shack Door
    private bool doorIsOpen = false;
    private float doorTimer = 0.0f;
    public float doorOpenTime = 3.0f;


    // Door Sounds 
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    private new AudioSource audio;

    // Battery sound
    public AudioClip batteryCollectSound; 

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Timer that automaticlly shuts door once it's open
        if(doorIsOpen)
        {
            doorTimer += Time.deltaTime; 
        }
        if(doorTimer > doorOpenTime)
        {
            ShutDoor();
            doorTimer = 0.0f;
        }
    }

    // Collision detection 
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "shackDoor" && !doorIsOpen && BatteryCollect.charge >= 4) 
        {
            OpenDoor();
            BatteryCollect.chargeUI.enabled = false;
           
        }
        else if (hit.gameObject.tag == "shackDoor" && !doorIsOpen && BatteryCollect.charge < 4)
        {
            BatteryCollect.chargeUI.enabled = true;
            TextHints.message = "The door seems to need more power...";
            TextHints.textOn = true;
        }

    }

    // Battery collision
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "battery")
        {
            BatteryCollect.charge++;
            audio.PlayOneShot(batteryCollectSound);
            Destroy(coll.gameObject);
        }
        
    }


    void OpenDoor()
    {
        // Play audio
        audio.PlayOneShot(doorOpenSound);
        // Set doorIsOpen to true
        doorIsOpen = true;
        // Find the GameObject that has animation
        GameObject myShack = GameObject.Find("Shack");
        // Play animation 
        myShack.GetComponent<Animation>().Play("doorOpen");
    }



    void ShutDoor()
    {

        audio.PlayOneShot(doorCloseSound);
        doorIsOpen = false;
        GameObject myShack = GameObject.Find("Shack");
        myShack.GetComponent<Animation>().Play("doorShut");
    }


}

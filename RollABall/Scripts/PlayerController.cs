using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
    public GameObject exitRamp;
        private float movementX;
        private float movementY;
    public bool sphereOnTheGround = true; 
	private Rigidbody rb;
	private int count;
public AudioClip pickupSound, winSound, jumpSound;
	// At the start of the game..
	private Vector3 resetPosition;
	void Start ()
	{ 
		resetPosition = new Vector3 (0.5f, 12.74f, 100.22f);
		resetPlayer();
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		SetCountText ();

                // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
                winTextObject.SetActive(false);
	}
 void resetPlayer()
 {
	 transform.position = new Vector3 (0.0f, 0.5f, 0.0f);
	 GetComponent<Rigidbody>().velocity = new Vector3(0.0f,0.0f,0.0f);
 }
 void elevatorCheckpoint()
 {
	 resetPosition = new Vector3( 0.28f, 13.14f, 115.35f);
 }
	void FixedUpdate ()
	{
		float miscareOrizontala = Input.GetAxis("Horizontal");
        float miscareVerticala = Input.GetAxis("Vertical");
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		
Vector3 movement = new Vector3(miscareOrizontala, 0.0f, miscareVerticala);
        rb.AddForce(movement * speed);
		
	if (transform.position.y <= -110)
	{
		resetPlayer();
	}
if(Input.GetButtonDown("Jump") && sphereOnTheGround)
        {
            rb.AddForce(new Vector3(0,8,0), ForceMode.Impulse);
            sphereOnTheGround = false;
            AudioSource.PlayClipAtPoint(jumpSound,transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            sphereOnTheGround = true;
        }
    }
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "Portal")
		{
			Application.LoadLevel("WinScene");
		}
		if(other.gameObject.tag == "Enemy")
		{
			resetPlayer();
		}
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
			AudioSource.PlayClipAtPoint(pickupSound,transform.position);
			if(count >=12)
			{
				exitRamp.SetActive(true);
			}
		}
	}

        void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 20) 
		{
                    // Set the text value of your 'winText'
                    winTextObject.SetActive(true);
					AudioSource.PlayClipAtPoint(winSound, transform.position);
		}
	}
}

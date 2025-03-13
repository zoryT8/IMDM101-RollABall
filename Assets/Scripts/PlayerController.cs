using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private int numPickUps;
    private float movementX;
    private float movementY;

    // Called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent <Rigidbody>();
        count = 0;
        numPickUps = 10;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Called once per fixed frame-rate frame
    private void FixedUpdate() {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // Called when a move input is detected
    void OnMove (InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    // Called when the player collides with a trigger collider (pickups)
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    // Called when the player collides with another collidable object
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            // Destroy the current object
            Destroy(gameObject); 
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    // Update the counter text, also displays win message if counter becomes maximum number possible
    void SetCountText() {
        countText.text =  "Count: " + count.ToString();

        if (count >= numPickUps) {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            winTextObject.SetActive(true);
        }
    }
}

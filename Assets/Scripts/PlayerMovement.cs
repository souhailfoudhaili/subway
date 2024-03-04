using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    bool alive = true;

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;

    // Variables for touch input
    bool isDragging = false;
    Vector2 dragStartPosition;

    private void FixedUpdate ()
    {
        if (!alive) return;

        // Calculate movement based on touch input or keyboard input
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update () {
        // Handle touch input
        HandleTouchInput();

        // Check for falling off the platform
        if (transform.position.y < -5) {
            Die();
        }
	}

    void HandleTouchInput()
{
    // Handle touch input to move the player horizontally
    if (Input.touchCount > 0) {
        Touch touch = Input.GetTouch(0);
        switch (touch.phase) {
            case TouchPhase.Began:
                isDragging = true;
                dragStartPosition = touch.position;
                break;
            case TouchPhase.Moved:
                if (isDragging) {
                    horizontalInput = (touch.position.x - dragStartPosition.x) / Screen.width * 5f; 
                }
                break;
            case TouchPhase.Ended:
                isDragging = false;
                horizontalInput = 0f;
                break;
        }
    }
}


    public void Die ()
    {
        alive = false;
        // Restart the game
        Invoke("Restart", 2);
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

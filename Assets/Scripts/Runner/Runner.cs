using UnityEngine;

public class Runner : MonoBehaviour
{
    public static float distanceTraveled;

    public float acceleration = 5f;
    public Vector3 jumpVelocity;
    public float gameOverY;
    public Vector3 startPosition;

    private new Rigidbody rigidbody;
    private new Renderer renderer;
    private bool touchingPlatform;

   

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        GameEventManager.Jump += Jump;

        startPosition = transform.localPosition;
        renderer.enabled = false;
        rigidbody.isKinematic = true;

        enabled = false;
    }

    private void GameStart()
    {
        distanceTraveled = 0f;
        transform.localPosition = startPosition;
        renderer.enabled = true;
        rigidbody.isKinematic = false;

        enabled = true;
    }

    private void GameOver()
    {
        renderer.enabled = false;
        rigidbody.isKinematic = true;

        enabled = false;
    }

    private void Update()
    {
        if(touchingPlatform && Input.GetButtonDown("Jump"))
        {
            GameEventManager.TriggerJump();
        }
        distanceTraveled = transform.localPosition.x;

        if (transform.localPosition.y < gameOverY)
        {
            GameEventManager.TriggerGameOver();
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
        touchingPlatform = false;
    }

    private void FixedUpdate()
    {
        if (touchingPlatform)
        {
            rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter()
    {
        touchingPlatform = true;
    }

    private void OnCollisionExit()
    {
        touchingPlatform = false;
    }
}

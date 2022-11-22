
using UnityEngine;
using UnityEngine.UI;

// I use Physics.gravity a lot instead of Vector3.up because you can point the gravity to a different direction and i want the controller to work fine
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
 
    private Rigidbody rb;
    Health health;
   
    private bool enableMovement = true;

    [Header("Movement properties")]
   public float runSpeed = 15.0f;
    public float changeInStageSpeed = 10.0f; // Lerp from walk to run and backwards speed
    public float maximumPlayerSpeed = 20.0f;
    public float vInput, hInput;

    [Header("Jump")]
    public float jumpForce = 500.0f;
    public float jumpCooldown = 1.0f;
    private bool jumpBlocked = false;

 public  bool _isGrounded = false;
    public bool IsGrounded { get { return _isGrounded; } }

    private Vector3 inputForce;

  [SerializeField]  Transform groundChecker;
  public  float groundCheckerDist;
 
    private void Awake()
    {

        rb = this.GetComponent<Rigidbody>();
        health = GetComponentInParent<Health>();
        
    }
  

    private void FixedUpdate()
    {

        // I recieved several messages that there are some bugs and I found out that the ground check is not working properly
        // so I made this one. It's faster and all it needs is the velocity of the rigidbody in two frames.
        // It works pretty well!

        float distOverGround = Physics.OverlapSphere(groundChecker.position, groundCheckerDist).Length;
   
       _isGrounded = distOverGround > 1; // > 1 because it also counts the player
       //    prevY = rb.velocity.y; 


       if (health.CurrentHealth <= 0) {return; }

        // Input
        vInput = Input.GetAxisRaw("Vertical");
        hInput = Input.GetAxisRaw("Horizontal");


        // Clamping speed
        rb.velocity = ClampMag(rb.velocity, maximumPlayerSpeed);

        if (!enableMovement)
            return;
        inputForce = (transform.forward * vInput + transform.right * hInput).normalized * runSpeed;

        if (_isGrounded)
        {
            // Jump
            if (Input.GetButton("Jump") && !jumpBlocked)
            {

                rb.AddForce(-jumpForce * rb.mass * Vector3.down);
                jumpBlocked = true;
                Invoke("UnblockJump", jumpCooldown);
          
            }

            // Ground controller
            rb.velocity = Vector3.Lerp(rb.velocity, inputForce, changeInStageSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Air control
            rb.velocity = ClampSqrMag(rb.velocity + inputForce * Time.fixedDeltaTime, rb.velocity.sqrMagnitude);

        }

      
     
    }

    private static Vector3 ClampSqrMag(Vector3 vec, float sqrMag)
    {
        if (vec.sqrMagnitude > sqrMag)
            vec = vec.normalized * Mathf.Sqrt(sqrMag);
        return vec;
    }

    private static Vector3 ClampMag(Vector3 vec, float maxMag)
    {
        if (vec.sqrMagnitude > maxMag * maxMag)
            vec = vec.normalized * maxMag;
        return vec;
    }


    private void UnblockJump()
    {
        jumpBlocked = false;
    }


    // Enables jumping and player movement
    public void EnableMovement()
    {
        enableMovement = true;
    }

    // Disables jumping and player movement
    public void DisableMovement()
    {
        enableMovement = false;
    }


}

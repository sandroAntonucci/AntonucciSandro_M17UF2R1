using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private float acceleration = 8f; 

    [SerializeField] float moveSpeed = 2f;

    // Animations
    Animator anim;
    private Vector2 lastMoveDirection;


    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Animate();
    }

    void FixedUpdate()
    {
        // Interpolate between current velocity and target velocity
        currentVelocity = Vector2.Lerp(currentVelocity, moveInput * moveSpeed, acceleration * Time.fixedDeltaTime);
        rb.velocity = currentVelocity;
    }

    public void Move(InputAction.CallbackContext context)
    {

        // This is to set the right direction to an idle animation (detects if the player stopped input but was inputting last frame) 
        Vector2 moveInputCheck = context.ReadValue<Vector2>();

        if (moveInputCheck.x == 0 && moveInputCheck.y == 0 && (moveInput.x != 0 || moveInput.y != 0))
        {
            lastMoveDirection = moveInput;
        }

        moveInput = context.ReadValue<Vector2>();






    }

    void Animate()
    {
        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.y);
        anim.SetFloat("MoveMagnitude", moveInput.magnitude);
        anim.SetFloat("lastMoveX", lastMoveDirection.x);
        anim.SetFloat("lastMoveY", lastMoveDirection.y);

    }
}

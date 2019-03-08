using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    private bool isGliding = false;

    private bool isDashing = false;

    public enum Color { White, Red, Blue, Yellow, Green};
    public Color currentColor = Color.White;

    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    public Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    private Vector2 directionalInput;
    public bool wallSliding;
    private int wallDirX;

    private Rigidbody2D rb;

    private Animator anim;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        CalculateVelocity();
        HandleWallSliding();

        
        controller.Move(velocity * Time.deltaTime, directionalInput);
        

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }
        if (isGliding && velocity.y <= 0f)
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2) / 10f;
        }
        
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void Dash()
    {
        //rb.transform.position += transform.right * 2;
        if (currentColor == Color.Yellow)
        {
            rb.AddForce(transform.right * 10f, ForceMode2D.Impulse);
            StartCoroutine(DashTimer());
        }
    }

    public void DashLeft()
    {
        
         rb.AddForce(-transform.right * 10f, ForceMode2D.Impulse);
         StartCoroutine(DashTimer());
        
    }

    public void OnJumpInputDown()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                if (currentColor == Color.Green)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else
                {
                    
                }
            }
            else if (directionalInput.x == 0 && currentColor == Color.Green)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
                wallSliding = false;
            }
            else if (currentColor == Color.Green)
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
                wallSliding = false;
            }
            isDoubleJumping = false;
        }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
        }
        if (canDoubleJump && !controller.collisions.below && !isDoubleJumping && !wallSliding)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = true;
        }
        if (currentColor == Color.Red)
        {
            isGliding = true;
        }
    }

    public void OnJumpInputUp()
    {
        if (currentColor == Color.Red)
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            isGliding = false;
        }
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0f)
            {
                velocityXSmoothing = 0f;
                velocity.x = 0f;
                if (directionalInput.x != wallDirX && directionalInput.x != 0f)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    private void CalculateVelocity()
    {

        
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime * .82f;

        if (currentColor == Color.Green && wallSliding || isDashing)
        {
            velocity.y = -0.00001f;
        }
        if (isGliding && velocity.y < -1f)
        {
            velocity.y = -1f;
        }

        if (Mathf.Abs(velocity.x) < .1f)
        {
            velocity.x = 0f;
        }

       
        
        anim.SetFloat("xvelocity", velocity.x);

        if (velocity.x < 0f)
        {
            anim.SetBool("goingleft", true);
            
        } else if (velocity.x > 0f)
        {
            anim.SetBool("goingleft", false);
            
        }

       
        
    }

    public void TurnDoubleJumpOn()
    {
        canDoubleJump = true;
    }

    public void TurnDoubleJumpOff()
    {
        canDoubleJump = false;
    }

    private IEnumerator DashTimer()
    {
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        isDashing = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }
}

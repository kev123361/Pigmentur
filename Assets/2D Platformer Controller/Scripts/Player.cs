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
    public bool grounded;
    public bool jumping;

    private bool isGliding = false;

    private bool isDashing = false;
    private bool dashLeft = false;
    private bool dashRight = false;

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
        
        if (isDashing)
        {
            if (dashLeft)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-8f, 0f, 0f), Time.deltaTime);
            } else
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(8f, 0f, 0f), Time.deltaTime);

            }
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
            
        }
        if (controller.collisions.below) {
            anim.SetBool("grounded", true);
            grounded = true;
        }
        else {
            anim.SetBool("grounded", false);
            grounded = false;
        }
        
        if (isGliding && velocity.y <= 0f)
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2) / 10f;
        }

        anim.SetFloat("yvelocity", velocity.y);


    }

    private void FixedUpdate()
    {
        
    }

    


    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    //Inefficient, Handle color check firstunity
    public void HandleDash()
    {
        if (velocity.x > 0)
        {
            Dash();
        } else if (velocity.x < 0)
        {
            DashLeft();
        }
    }

    public void Dash()
    {
        //rb.transform.position += transform.right * 2;
        if (currentColor == Color.Yellow)
        {
            dashRight = true;
            StartCoroutine(DashTimer());
        }
    }

    public void DashLeft()
    {
        if (currentColor == Color.Yellow)
        {
            dashLeft = true;
            StartCoroutine(DashTimer());
        }
        
    }

    public void OnJumpInputDown()
    {
        if (grounded || (canDoubleJump && !isDoubleJumping) || 
            (currentColor == Color.Green && wallSliding) || (currentColor == Color.Red))
        {
            if (grounded)
            {
                anim.SetTrigger("jumpTrig");
            }
            StartCoroutine(DelayJump());
        }
        
    }

    public void OnJumpInputUp()
    {
        
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        isGliding = false;
        anim.SetBool("isGliding", false);
        
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
        velocity.y += gravity * Time.deltaTime * .85f;

        if (currentColor == Color.Green && wallSliding || isDashing)
        {
            velocity.y = -0.00001f;
        }
        if (isGliding && velocity.y < -5f)
        {
            velocity.y = -5f;
        }

        if (Mathf.Abs(velocity.x) < .1f)
        {
            velocity.x = 0f;
        }
        
        

        // This needs to be phased out eventually
        //if (velocity.y <= 0f)
        //{
        //    anim.SetBool("jumping", false);
        //}
       
        
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

        yield return new WaitForSeconds(.5f);

        isDashing = false;
        dashRight = false;
        dashLeft = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(.03f);
        
        jumping = true;
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                if (currentColor == Color.Green)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
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
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            velocity.y = maxJumpVelocity;
            anim.SetTrigger("doublejumpTrig");
            isDoubleJumping = true;
        }
        if (currentColor == Color.Red)
        {
            isGliding = true;
            anim.SetBool("isGliding", true);
        }
    }
}

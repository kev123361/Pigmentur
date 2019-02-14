using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;
    private ColorChange cc;

    public bool inBlue;
    public bool inRed;
    public bool inYellow;
    public bool inGreen;

    public float dCooler = .5f;
    private float dCounter;

    public float aCooler = .5f;
    private float aCounter;

    private void Start()
    {
        player = GetComponent<Player>();
        cc = GetComponent<ColorChange>();
    }

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (dCooler > 0 && dCounter == 1)
            {
                player.Dash();
                Debug.Log("Dashed");
            }
            else
            {
                dCooler = .5f;
                dCounter += 1;
            }
        }
        if (dCooler > 0)
        {
            dCooler -= 1 * Time.deltaTime;
        } else
        {
            dCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (aCooler > 0 && aCounter == 1)
            {
                player.DashLeft();
                Debug.Log("Dashed Left");
            }
            else
            {
                aCooler = .5f;
                aCounter += 1;
            }
        }
        if (aCooler > 0)
        {
            aCooler -= 1 * Time.deltaTime;
        }
        else
        {
            aCounter = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.OnJumpInputDown();
        }

        

        if (Input.GetButtonUp("Jump"))
        {
            player.OnJumpInputUp();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (inBlue)
            {
                cc.ChangeBlue();
                player.TurnDoubleJumpOn();
                player.currentColor = Player.PlayerColor.Blue;
            } else if (inRed)
            {
                cc.ChangeRed();
                player.TurnDoubleJumpOff();
                player.currentColor = Player.PlayerColor.Red;
            } else if (inYellow)
            {
                cc.ChangeYellow();
                player.TurnDoubleJumpOff();
                player.currentColor = Player.PlayerColor.Yellow;
            } else if (inGreen)
            {
                cc.ChangeGreen();
                player.TurnDoubleJumpOff();
                player.currentColor = Player.PlayerColor.Green;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Blue Pickup"))
        {
            inBlue = true;
        }
        else if (other.CompareTag("Red Pickup"))
        {
            inRed = true;
        }
        else if (other.CompareTag("Yellow Pickup"))
        {
            inYellow = true;
        }
        else if (other.CompareTag("Green Pickup"))
        {
            inGreen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Blue Pickup"))
        {
            inBlue = false;
        } else if (collision.CompareTag("Red Pickup"))
        {
            inRed = false;
        }
        else if (collision.CompareTag("Yellow Pickup"))
        {
            inYellow = false;
        }
        else if (collision.CompareTag("Green Pickup"))
        {
            inGreen = false;
        }
    }
}

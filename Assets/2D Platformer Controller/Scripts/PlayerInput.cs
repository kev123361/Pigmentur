using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;
    private ColorChange cc;

    public Fairy fairy;
    private ColorChange fairycc;
    public bool controllable;

    public bool inBlue;
    public bool inRed;
    public bool inYellow;
    public bool inGreen;
    public bool nearStone;
    public GameObject currStone;

    public Vector3 checkpoint;

    // Old system used for double tap dash
    //private float dCooler = .5f;
    //private float dCounter;
    //private float aCooler = .5f;
    //private float aCounter;

    private void OnEnable()
    {
        LevelManager.OnLevelComplete += DisableControl;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelComplete -= DisableControl;
    }

    private void Start()
    {
        player = GetComponent<Player>();
        cc = GetComponent<ColorChange>();
        checkpoint = transform.position;
        try
        {
            fairycc = fairy.GetComponent<ColorChange>();
        } catch (Exception e)
        {
        
            Debug.Log("No Fairy in scene");
        }
        
        
    }

    private void Awake()
    {
        controllable = true;
    }

    private void Update()
    {
        if (controllable)
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);

            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    if (dCooler > 0 && dCounter == 1)
            //    {
            //        player.Dash();
            //        Debug.Log("Dashed");
            //    }
            //    else
            //    {
            //        dCooler = .5f;
            //        dCounter += 1;
            //    }
            //}
            //if (dCooler > 0)
            //{
            //    dCooler -= 1 * Time.deltaTime;
            //} else
            //{
            //    dCounter = 0;
            //}

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    if (aCooler > 0 && aCounter == 1)
            //    {
            //        player.DashLeft();
            //        Debug.Log("Dashed Left");
            //    }
            //    else
            //    {
            //        aCooler = .5f;
            //        aCounter += 1;
            //    }
            //}
            //if (aCooler > 0)
            //{
            //    aCooler -= 1 * Time.deltaTime;
            //}
            //else
            //{
            //    aCounter = 0;
            //}

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //player.HandleDash();
            }


            if (Input.GetButtonDown("Jump"))
            {
                player.OnJumpInputDown();
            }


            if (Input.GetButtonUp("Jump"))
            {
                player.OnJumpInputUp();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (inBlue)
                {
                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Blue);
                    player.TurnDoubleJumpOn();
                    player.currentColor = Player.Color.Blue;
                }
                else if (inRed)
                {
                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Red);
                    player.TurnDoubleJumpOff();
                    player.currentColor = Player.Color.Red;
                }
                else if (inYellow)
                {
                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Yellow);
                    player.TurnDoubleJumpOff();
                    player.currentColor = Player.Color.Yellow;
                }
                else if (inGreen)
                {
                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Green);
                    player.TurnDoubleJumpOff();
                    player.currentColor = Player.Color.Green;
                }
                //Handle Stone filling
                else if (nearStone)
                {
                    if (currStone.GetComponent<Stone>().color == player.currentColor)
                    {
                        currStone.GetComponent<Stone>().Fill();
                        checkpoint = currStone.transform.position;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                HandleColorSwap();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                HandleFairyChange();
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
        } else if (other.CompareTag("Stone"))
        {
            nearStone = true;
            currStone = other.gameObject;
        } else if (other.CompareTag("CatchBox"))
        {
            transform.position = checkpoint;
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
        else if (collision.CompareTag("Stone"))
        {
            nearStone = false;
        }
    }

    private void HandleColorSwap()
    {
        Player.Color playerColor = player.currentColor;
        cc.TurnOffMesh(playerColor);
        cc.ChangeColor(fairy.GetColor());
        player.currentColor = fairy.GetColor();
        fairycc.TurnOffMesh(fairy.GetColor());
        fairycc.ChangeColor(playerColor);
        fairy.SetColor(playerColor);
        if (player.currentColor == Player.Color.Blue)
        {
            player.TurnDoubleJumpOn();
        } else { player.TurnDoubleJumpOff(); }
    }

    private void HandleFairyChange()
    {
        if (inBlue)
        {
            fairycc.ChangeColor(Player.Color.Blue);
            fairy.SetColor(Player.Color.Blue);
        }
        else if (inRed)
        {
            fairycc.ChangeColor(Player.Color.Red);
            fairy.SetColor(Player.Color.Red);
        }
        else if (inYellow)
        {
            fairycc.ChangeColor(Player.Color.Yellow);
            fairy.SetColor(Player.Color.Yellow);
        }
        else if (inGreen)
        {
            fairycc.ChangeColor(Player.Color.Green);
            fairy.SetColor(Player.Color.Green);
        }
    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(.1f);
        player.OnJumpInputDown();
    }

    public void DisableControl()
    {
        controllable = false;
    }
}

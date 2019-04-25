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
    [SerializeField] private GameObject currCrack;

    public Vector3 checkpoint;

    public GameObject blueFuzz;
    public GameObject yellowFuzz;
    public GameObject redFuzz;
    public GameObject greenFuzz;

    [SerializeField] private PlayerSFX playerSFX;

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
        playerSFX = GetComponent<PlayerSFX>();
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
                    GameObject fuzz = Instantiate(blueFuzz, currCrack.transform.position, Quaternion.identity);
                    fuzz.GetComponent<ParticlesToPlayer>().player = gameObject;

                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Blue);
                    player.TurnDoubleJumpOn();
                    player.currentColor = Player.Color.Blue;
                    playerSFX.PlayAbsorb();
                }
                else if (inRed)
                {
                    GameObject fuzz = Instantiate(redFuzz, currCrack.transform.position, Quaternion.identity);
                    fuzz.GetComponent<ParticlesToPlayer>().player = gameObject;
                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Red);
                    player.TurnDoubleJumpOff();
                    player.currentColor = Player.Color.Red;
                    playerSFX.PlayAbsorb();
                }
                else if (inYellow)
                {
                    GameObject fuzz = Instantiate(yellowFuzz, currCrack.transform.position, Quaternion.identity);
                    fuzz.GetComponent<ParticlesToPlayer>().player = gameObject;
                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Yellow);
                    player.TurnDoubleJumpOff();
                    player.currentColor = Player.Color.Yellow;
                    playerSFX.PlayAbsorb();
                }
                else if (inGreen)
                {
                    GameObject fuzz = Instantiate(greenFuzz, currCrack.transform.position, Quaternion.identity);
                    fuzz.GetComponent<ParticlesToPlayer>().player = gameObject;
                    cc.TurnOffMesh(player.currentColor);
                    cc.ChangeColor(Player.Color.Green);
                    player.TurnDoubleJumpOff();
                    player.currentColor = Player.Color.Green;
                    playerSFX.PlayAbsorb();
                }
                //Handle Stone filling
                else if (nearStone)
                {
                    playerSFX.PlayInsert();

                    GameObject newParticles;
                    if (player.currentColor == Player.Color.Red)
                    {
                        newParticles = Instantiate(redFuzz, transform.position, Quaternion.identity);
                    } else if (player.currentColor == Player.Color.Blue)
                    {
                        newParticles = Instantiate(blueFuzz, transform.position, Quaternion.identity);
                    }
                    else if (player.currentColor == Player.Color.Yellow)
                    {
                        newParticles = Instantiate(yellowFuzz, transform.position, Quaternion.identity);
                    }
                    else 
                    {
                        newParticles = Instantiate(greenFuzz, transform.position, Quaternion.identity);
                    }
                    currStone.GetComponent<Stone>().Fill(player.currentColor);
                    newParticles.GetComponent<ParticlesToPlayer>().player = currStone;
                    currStone.GetComponent<TurnColorOn>().ColorWorld();
                        
                        
                    checkpoint = currStone.transform.position;
                    
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (fairy)
                {
                    HandleColorSwap();
                }
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
            currCrack = other.gameObject;
        }
        else if (other.CompareTag("Red Pickup"))
        {
            inRed = true;
            currCrack = other.gameObject;
        }
        else if (other.CompareTag("Yellow Pickup"))
        {
            inYellow = true;
            currCrack = other.gameObject;
        }
        else if (other.CompareTag("Green Pickup"))
        {
            inGreen = true;
            currCrack = other.gameObject;
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
            currCrack = null;
        } else if (collision.CompareTag("Red Pickup"))
        {
            inRed = false;
            currCrack = null;
        }
        else if (collision.CompareTag("Yellow Pickup"))
        {
            inYellow = false;
            currCrack = null;
        }
        else if (collision.CompareTag("Green Pickup"))
        {
            inGreen = false;
            currCrack = null;
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

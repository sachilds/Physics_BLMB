using UnityEngine;
using System.Collections;

public enum PlayerNumber
{
    one,
    two
}

public class PlayerInputScript : MonoBehaviour
{
    private Player Player;
    public PlayerNumber playerNumber;

    void Start()
    {
        Player = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        handleGameplay();
    }

    private void handleGameplay()
    {
        switch (playerNumber)
        {
            case PlayerNumber.one:
                //Left and Right
                if (Input.GetAxis("P1.Horizontal") != 0)
                    Player.Movement(Input.GetAxis("P1.Horizontal"));

                if (Input.GetButton("P1.Jump"))
                {
                    Player.Jump();
                }
                if (Input.GetButton("P1.HatMechanic"))
                {
                    Player.UseHatMechanic();
                }
                if (Input.GetButtonDown("P1.Use"))
                {
                    if (Player.hatInRange && !Player.currentHat)
                        Player.AttachToPlayer();
                    else if (Player.currentHat)
                        Player.DetachFromPlayer();
                }
                break;


            case PlayerNumber.two:
                //Left and Right
                if (Input.GetAxis("P2.Horizontal") != 0)
                    Player.Movement(Input.GetAxis("P2.Horizontal"));

                if (Player.IsGrounded && Input.GetAxis("P2.Jump") > 0)
                {
                    Player.Jump();
                }
                if (Input.GetButton("P2.HatMechanic"))
                {
                    Player.UseHatMechanic();
                }
                if (Input.GetButtonDown("P2.Use"))
                {
                    if (Player.hatInRange && !Player.currentHat)
                        Player.AttachToPlayer();
                    else if (Player.currentHat)
                        Player.DetachFromPlayer();
                }
                break;
            default:
                break;
        }
    }
}

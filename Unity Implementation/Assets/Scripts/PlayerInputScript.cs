using UnityEngine;
using System.Collections;

public enum PlayerNumber{
	one,
	two
}

public class PlayerInputScript : MonoBehaviour {
    private Player Player;
    public PlayerNumber playerNumber;

    void Start() {
		Player = GetComponent<Player>();
		//delete if u want game to start at MainMenu 
		Game_Manager.gameState = Game_Manager.GameState.InGame;
    }

    // Update is called once per frame
	void Update () {
        switch (Game_Manager.gameState) {
            case Game_Manager.GameState.Menu:
                break;
            case Game_Manager.GameState.Credits:
                break;
            case Game_Manager.GameState.Pause:
                break;
            case Game_Manager.GameState.Options:
                break;
            case Game_Manager.GameState.InGame:
                handleGameplay();
                break;
            case Game_Manager.GameState.Tutorial:
                break;
            case Game_Manager.GameState.GameOver:
                break;
            default:
                break;
       
        }
        
	}

    private void handleGameplay() {
        switch (playerNumber) {
            case PlayerNumber.one:
                //Left and Right
				Player.MoveHorizontal(Input.GetAxis("P1.Horizontal"));

				if (Player.IsGrounded && Input.GetAxis("P1.Jump") > 0) {
                    Player.IsGrounded = false;
                    Player.Jump();
                    StartCoroutine("JumpDelay");
                }
                break;
            
            
            case PlayerNumber.two:
                //Left and Right
				Player.MoveHorizontal(Input.GetAxis("P2.Horizontal"));

                if (Player.IsGrounded && Input.GetAxis("P2.Jump") > 0) {
                    Player.IsGrounded = false;
                    Player.Jump();
                    StartCoroutine("JumpDelay");
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator JumpDelay() {
        yield return new WaitForSeconds(1.4f);
    }
}

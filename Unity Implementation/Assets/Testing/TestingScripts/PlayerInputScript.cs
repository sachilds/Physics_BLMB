using UnityEngine;
using System.Collections;
public enum PlayerNumber{
	one,
	two
}

public class PlayerInputScript : MonoBehaviour {
    
    private Rigidbody2D rigidbody;
    public PlayerNumber playerNumber;
    public float Speed;
    public float JumpSpeed; 
    
    //TEMP VARIABLES. not permanent
    bool isJumping = false;
   
    void Start(){
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Game_Manager.gameState = Game_Manager.GameState.InGame;////////TEMPORARY
    }
// Update is called once per frame
	void Update () {
        switch (Game_Manager.gameState)
        {
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

    private void handleGameplay(){
        switch (playerNumber)
        {
            case PlayerNumber.one:
                //Left and Right
                rigidbody.AddForce(new Vector2(Input.GetAxis("P1.Horizontal") * Speed, 0));

                if (!isJumping && Input.GetAxis("P1.Jump") > 0)
                {
                    isJumping = true;
                    rigidbody.AddForce(new Vector2(0, 1 * JumpSpeed));
                    StartCoroutine("JumpDelay");
                }
                break;
            
            
            case PlayerNumber.two:
                //Left and Right
                rigidbody.AddForce(new Vector2(Input.GetAxis("P2.Horizontal") * Speed, 0));

                if (!isJumping && Input.GetAxis("P2.Jump") > 0)
                {
                    isJumping = true;
                    rigidbody.AddForce(new Vector2(0, 1 * JumpSpeed));
                    StartCoroutine("JumpDelay");
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator JumpDelay(){
        yield return new WaitForSeconds(2);
        isJumping = false;
    }
}

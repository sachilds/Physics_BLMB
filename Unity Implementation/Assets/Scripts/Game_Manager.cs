using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {

    private class PauseMenu { 
        public const int WIDTH = 400;
        public const int HEIGHT = 500;
    }

    public enum GameState { 
		Menu, Credits, Pause, Options, InGame, Tutorial, GameOver, PlayerSelect
    }
    private static bool isCreated;
    public static GameState gameState;
    public static bool isPaused;

	//Cassie was here
	public static float soundFXVol = 1.0f;
	public static float backGroundVol = 1.0f;
    public GUISkin menuSkin;

	//player select stuff
	public GameObject single;
	public GameObject coop;

	// Use this for initialization
	void Start () {
        if (!isCreated) {
            isCreated = true;
            DontDestroyOnLoad(gameObject);

            gameState = GameState.Menu;
            single = Resources.Load("Prefabs/UI/Single-Player") as GameObject;
            //single.SetActive(false);
            coop = Resources.Load("Prefabs/UI/Coop-Player") as GameObject;
            //coop.SetActive(false);


        }
        else {
            Destroy(gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
	    if(gameState == GameState.Options)
		{
			soundFXVol = GameObject.Find("Options").GetComponent<OptionsScript>().getFXVol();
			backGroundVol = GameObject.Find("Options").GetComponent<OptionsScript>().getBGVol();
		}
	}

    // GUI
    void OnGUI() {
        GUI.skin = menuSkin;
        switch (gameState)
        {
            //MAIN MENU
            case GameState.Menu:
				//New Game
				if (Main_Menu_GUI.menuStatus.Equals("Play"))
			    {
					Debug.Log("Play the Game");
					gameState = GameState.PlayerSelect;
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("MenuGUI"))
                        go.SetActive(false);

                    Instantiate(single);
                    Instantiate(coop);
                    Debug.Log("HELLO MOFO");
                    Main_Menu_GUI.menuStatus = " ";
				}
				//Options
				if (Main_Menu_GUI.menuStatus.Equals("Options")) {
					Debug.Log("Open the Options");
					gameState = GameState.Options;
					Application.LoadLevel("Options");
				}
				if (Main_Menu_GUI.menuStatus.Equals("Quit")) {
					Debug.Log("Quit the Game");
					Application.Quit();
				}
                break;

            //IN-GAME	
            case GameState.InGame:
                if (Input.GetKey(KeyCode.Escape)) {
                    gameState = GameState.Pause;
                }
                break;

            //OPTIONS	
            case GameState.Options:
                break;

            //PAUSE
            case GameState.Pause:
                GUI.Box(GetPauseMenu(), "Paused");
                GUI.BeginGroup(GetPauseMenu());
                    Rect r = GetPauseMenu();
                    if (GUI.Button(new Rect(r.width / 2 - 75, 100, 150, 50), "Resume")) {
                        gameState = GameState.InGame;
                        isPaused = false;
                    }
                    if (GUI.Button(new Rect(r.width / 2 - 75, 200, 150, 50), "Quit")) {
                        gameState = GameState.Menu;
                        isPaused = false;
                        Application.LoadLevel("MainMenu");
                    }
                GUI.EndGroup();
                break;

            //GAME OVER	
            case GameState.GameOver:
                break;

            //TUTORIAL CONTROLS
            case GameState.Tutorial:
                break;

			//Player Select Stuff
			case GameState.PlayerSelect:
			    //single.SetActive(true);
			    //coop.SetActive(true);
			    if (PlayerSelect.menuStatus.Equals("Single"))
			    {
				    //PlayMenu();
				    Debug.Log("Single Player");
				    gameState = GameState.InGame;
                    PlayerSelect.menuStatus = " ";
				    Application.LoadLevel("CandyLand_1Player");
			    }
			    if (PlayerSelect.menuStatus.Equals("Coop"))
			    {
				    //PlayMenu();
				    Debug.Log("Coop Play");
				    gameState = GameState.InGame;
                    PlayerSelect.menuStatus = " ";
				    Application.LoadLevel("CandyLand_2Player");
			    }
			break;

            //DEFAULT	
            default:
                break;
        }
    }


    private Rect GetCenterScreen() {
        return new Rect(Screen.width / 2,
                        Screen.height / 2,
                        0,
                        0);
    }

    private Rect GetPauseMenu() {
        Rect r = GetCenterScreen();

        return new Rect(r.x - PauseMenu.WIDTH /2,
                        r.y - PauseMenu.HEIGHT / 2,
                        PauseMenu.WIDTH,
                        PauseMenu.HEIGHT);
    }
} // End of Class

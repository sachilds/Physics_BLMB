using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {

    public static class MenuLabel {
		public const int WIDTH = 190;
		public const int HEIGHT = 60;
		public const float PADDING = 15;
	}

    public static class GameLogoLabel {
		public const int WIDTH = 500;
		public const int HEIGHT = 150;		
	}


    public enum GameState { 
        Menu, Credits, Pause, Options, InGame, Tutorial, GameOver
    }
    private bool isCreated;
    public static GameState gameState;
    private GameState prevState;

	//Cassie was here
	public static float soundFXVol;
	public static float backGroundVol;

	// Use this for initialization
	void Start () {
        if (!isCreated) {
            isCreated = true;
            DontDestroyOnLoad(gameObject);

            gameState = GameState.Menu;
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
        switch (gameState)
        {
            //MAIN MENU
            case GameState.Menu:
				//New Game
				if (Main_Menu_GUI.menuStatus.Equals("Play"))
			    {
					//PlayMenu();
					Debug.Log("Play the Game - Continue from the XML File");
					//gameState = GameState.InGame;
					//LoadNewScreen("DemoLevel");
				}
				//Options
				if (Main_Menu_GUI.menuStatus.Equals("Options")) {
					//PlayMenu();
					Debug.Log("Open the Options");
					//gameState = GameState.Options;
				}
				if (Main_Menu_GUI.menuStatus.Equals("Quit")) {
					//PlayMenu();
					Debug.Log("Quit the Game");
					//Application.Quit();
				}
				//Quit
				/*
                GUI.Label(GetMenuLogo(), "BLB");
                GUI.BeginGroup(GetMainMenu());
                //New Game
                if (GUI.Button(GetMenuLabel(1), "Play Game"))
                {
                    PlayMenu();
                    Debug.Log("Play the Game - Continue from the XML File");
                    gameState = GameState.InGame;
                    LoadNewScreen("CandyLand_1Player");
                }

                //Options
                if (GUI.Button(GetMenuLabel(2), "Options")) {
                    PlayMenu();
                    Debug.Log("Open the Options");
                    gameState = GameState.Options;
					LoadNewScreen("Options");
                }

                //Quit
                if (GUI.Button(GetMenuLabel(3), "Quit")) {
                    PlayMenu();
                    Debug.Log("Quit the Game");
                    Application.Quit();
                }
                GUI.EndGroup();
                */
                break;

            //IN-GAME	
            case GameState.InGame:
                break;

            //OPTIONS	
            case GameState.Options:
                break;

            //PAUSE
            case GameState.Pause:
                break;

            //GAME OVER	
            case GameState.GameOver:
                break;

            //TUTORIAL CONTROLS
            case GameState.Tutorial:
                break;

            //DEFAULT	
            default:
                break;
        }
    }


    // Play Menu
    // Plays the Menu click sound and saves the game state to prevState before changing
    private void PlayMenu() {
        // audio.Play(); TODO: Add Sound Effects
        prevState = gameState;
    }

    //Load a New Screen
    private void LoadNewScreen(string ID) {
        transform.parent = null;

        if (ID != "")
            Application.LoadLevel(ID);
    }

    
    #region UI Mapping
    public Rect GetCenterScreen() {
        return new Rect(Screen.width / 2, 
                        Screen.height / 2,
                        Screen.width / 2,
                        Screen.height / 2);
    }

    #region MainMenu
    public Rect GetMainMenu() {
        Rect tmp = GetCenterScreen();

        return new Rect(tmp.x - (MenuLabel.WIDTH / 2),
                        tmp.y - (MenuLabel.HEIGHT / 2),
                        MenuLabel.WIDTH,
                        (MenuLabel.HEIGHT * 3) + (MenuLabel.PADDING * 3));
    }

    public Rect GetMenuLogo() {
        Rect tmp = GetCenterScreen();

        return new Rect(tmp.x - GameLogoLabel.WIDTH / 2,
                        tmp.y - GameLogoLabel.HEIGHT,
                        GameLogoLabel.WIDTH,
                        GameLogoLabel.HEIGHT);
    }

    public Rect GetMenuLabel(float yOffset) {
        return new Rect(0,
                        MenuLabel.HEIGHT * (yOffset - 1) + (yOffset * MenuLabel.PADDING),
                        MenuLabel.WIDTH,
                        MenuLabel.HEIGHT);
    }

    #endregion

    #endregion

} // End of Class

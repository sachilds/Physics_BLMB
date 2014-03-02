#ifndef SCENEMANAGER_H
#define SCENEMANGER_H
#include <vector>
#include "World.h"
#include <SDL.h>

using namespace std;

class SceneManager
{
public:
	enum GameState {
		MainMenu,
		InGame,
		Credits,
		Pause,
		GameOver
	};

	SceneManager() : currentWorld(0) {
		gameState = GameState::MainMenu;
	}

	~SceneManager();

	// Returns the gameState if requested outside of the class
	static GameState GetGameState() { return gameState; }
	
	// Sets the gameState if it needs to be reset outside of the scenemanager
	static void SetGameState(GameState value) { gameState = value; }
	
	void Update() {
		switch (gameState) {
			case GameState::MainMenu:
				break;
			case GameState::Pause:
				break;
			case GameState::GameOver:
				break;
			case GameState::Credits:
				break;
			case GameState::InGame:
				break;
			default:
				break;
		}
	}

	void Draw(SDL_Surface surface) {
		switch (gameState) {
			case GameState::MainMenu:
				break;
			case GameState::Pause:
				break;
			case GameState::GameOver:
				break;
			case GameState::Credits:
				break;
			case GameState::InGame:
				break;
			default:
				break;
		}
	}

private:
	static GameState gameState;
	vector<World> worlds;
	int currentWorld;
};
#endif

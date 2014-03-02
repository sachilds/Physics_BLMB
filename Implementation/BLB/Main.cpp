#include <SDL.h>
#include <iostream>

// Methods
bool Init(); // Starts up and creates the sdl window
void HandleEvents();
void Update();
void Draw();
void Close(); // Closes SDL

// Variables
SDL_Window* gameWindow = NULL; // The window to render to
SDL_Surface* screenSurface = NULL; // The surface contained by the window
bool isRunning;

int main(int argc, char* args[]) {
	// Start up SDL
	if (!Init())
		printf("Failed to initialize!\n");
	else {
		// Start the game!
		isRunning = true;

		while (isRunning) {
			HandleEvents(); // Get user input
			Update(); // Update the game state
			Draw(); // Draw the game
		}
	}

	// Close the Program
	// Close(); - Not yet, will call on "Quit" menu option
 	return 0;
}



bool Init() {
	// Initialization flag
	bool success = true;

	// Init SDL
	if (SDL_Init(SDL_INIT_EVERYTHING) < 0) {
		printf("SDL could not be initialized. SDL_Error: %s\n", SDL_GetError());
		success = false;
	}
	else {
		// Create the window
		gameWindow = SDL_CreateWindow("Bob Lob the Blob: The Blob Father", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 800, 600, SDL_WINDOW_SHOWN);

		if (gameWindow == NULL) {
			printf("Window could not be created! SDL_Error: %s\n", SDL_GetError());
			success = false;
		}
		else {
			// Get the window surface
			screenSurface = SDL_GetWindowSurface(gameWindow);
		}
	}

	return success;
}

void HandleEvents() {
	
}

void Update() {
	
}

void Draw() {
	//Just to test stuff
	SDL_Delay(3000); 
	isRunning = false;
}

void Close() {
	// Destroy window
	SDL_DestroyWindow(gameWindow); // Will also destroy the surface attached to the window
	gameWindow = NULL;

	// Quit SDL
	SDL_Quit();
}
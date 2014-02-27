#include <SDL.h>
#include <iostream>

using namespace std;


bool Init(); // Starts up and creates the sdl window
bool LoadMedia(); // loads media
void Close(); // Closes SDL

SDL_Window* gameWindow = NULL; // The window to render to
SDL_Surface* screenSurface = NULL; // The surface contained by the window
SDL_Surface* image = NULL; // The image we will load and show on the screen

int main(int argc, char* args[]) {
	
	// Start up SDL
	if (!Init())
		printf("Failed to initialize!\n");
	else {
		//Load the Media
		if (!LoadMedia())
			printf("Failed to load media!\n");
		else {
			// Apply the Image
			//SDL_BlitSurface(image, NULL, screenSurface, NULL);
			SDL_Rect rect;
			rect.x = 50;
			rect.y = 10;
			rect.h = 50;
			rect.w = 100;
			SDL_BlitSurface(image, &rect, screenSurface, &rect);
			// Update the Surface
			SDL_UpdateWindowSurface(gameWindow);

			// Wait two seconds
			SDL_Delay(2000);
		}
	}

	// Close the Program
	Close();
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

bool LoadMedia() {
	// Loading flag
	bool success = true;

	// Load splash image
	image = SDL_LoadBMP("Bob_As_BMP.bmp");

	if (image == NULL) {
		printf("Unable to load image %s! SDL Error: %s\n", "Bob_As_BMP.bmp", SDL_GetError());
		success = false;
	}

	return success;
}

void Close() {
	// Deallocate surfaces
	SDL_FreeSurface(image);
	image = NULL;

	// Destroy window
	SDL_DestroyWindow(gameWindow); // Will also destroy the surface attached to the window
	gameWindow = NULL;

	// Quit SDL
	SDL_Quit();
}
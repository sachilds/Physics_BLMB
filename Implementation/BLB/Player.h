#ifndef PLAYER_H
#define PLAYER_H
#include"Sprite.h"
#include"Hat.h"
#include <string>
using std::string;
class Player:public Sprite
{
private:
	int maxHealth;
	int health;
	Hat activeHat;
public:
	int playerNumber;
	Player(string& name, string& path, int playerNum);
	~Player();
	int GetHealth();
	void SetHealth(int value);
	Hat& GetHat();
	void SetHat(Hat hat);

};
#endif
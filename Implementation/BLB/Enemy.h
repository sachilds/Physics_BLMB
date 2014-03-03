#ifndef ENEMY_H
#define ENEMY_H
#include"Sprite.h"
#include<string>
using std::string;
class Enemy: public Sprite
{
private:
	int maxHealth;
	int health;
public:
	Enemy(string& name, string& path);
	~Enemy();
	int GetHealth();
	void SetHealth(int value);
};
#endif
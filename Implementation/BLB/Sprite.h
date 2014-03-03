#ifndef SPRITE_H
#define SPRITE_H
#include "GameObject.h"
#include <string>
using std::string;
class Sprite:public GameObject
{
protected:
	string path;
public:
	Sprite(string& name, string& path);
	~Sprite();
};
#endif
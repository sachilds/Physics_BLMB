#ifndef OBSTACLE_H
#define OBSTACLE_H
#include<string>
#include "GameObject.h"
using std::string;
class Obstacle:public GameObject
{
protected:
	string path;
public:
	Obstacle(string& name,string& path);
	~Obstacle();
};
#endif
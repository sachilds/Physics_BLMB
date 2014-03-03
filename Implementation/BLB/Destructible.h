#ifndef DESTRUCTIBLE_H
#define DESTRUCTIBLE_H
#include "Obstacle.h"
#include <string>
using std::string;
class Destructible:public Obstacle
{
public:
	Destructible(string& name,string& path);
	~Destructible();
};
#endif
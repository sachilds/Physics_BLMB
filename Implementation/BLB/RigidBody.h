#ifndef RIGIDBODY_H
#define RIGIDBODY_H
#include"Input.h"//for when there is an input.h
#include"AI.h"//for when there is an ai.h
class RigidBody
{
protected:
	Input input;
	AI ai;
	int layer;
public:
	RigidBody();
	~RigidBody();
	void Update();
	void Draw();
};
#endif
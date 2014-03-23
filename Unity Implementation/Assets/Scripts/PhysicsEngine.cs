using UnityEngine;
using System.Collections;

public class PhysicsEngine : MonoBehaviour {
    public const float GRAVITY = -9.8f;

    // The force applied by Friction
	public static Vector2 CalculateFrictionForce(float coeff, float mass) {
        float Fw = CalculateForceWeight(mass);
        return new Vector2(Fw * coeff, 0);
    }

    // Calculate the weight force
    private static float CalculateForceWeight(float mass) {
        return mass * GRAVITY;
    }

    // Calculate the horizontal net force acting on the object
    public static float HorizontalNetForce(float appliedForce, float coeffFriction, float mass) {
        float Ff = HorizontalFrictionForce(coeffFriction, mass);
        return appliedForce - Ff;
    }

    // Calculate the horizontal friction acting on the object
    public static float HorizontalFrictionForce(float coeffFriction, float mass) {
        float Fw = CalculateForceWeight(mass);
        float Fn = Fw;
        return Fn * coeffFriction;
    }

    // DELETE THIS SOON
    public static Vector2 CalculateNetForce(Vector2 appliedForce, float coeffFriction, float mass) {
        Vector2 Ff = CalculateFrictionForce(coeffFriction, mass);
        return new Vector2(appliedForce.x - Ff.x, 0);
    }
}

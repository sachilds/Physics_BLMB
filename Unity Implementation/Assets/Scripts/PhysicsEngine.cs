using UnityEngine;
using System.Collections;

public class PhysicsEngine : MonoBehaviour {
    public const float GRAVITY = -9.8f;
    private const float COEFF_AIR = 0.2f;
    private const float COEFF_GROUND = 0.8f;
    private const float COEFF_STICKY = 0.2f;
    private const float COEFF_SLIPPERY = 1;
    public const float COEFF_RESTITUTION_BOUNCY = 0.5f;
    public const int MAX_VELOCITY = 5;

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

    // For Jello
    public static float CalculateVerticalBounce(float maxHeight, float mass) {
        float value = 2 * GRAVITY * maxHeight;
        if (value < 0)
            value *= -1;

        float vf = Mathf.Sqrt(value) * COEFF_RESTITUTION_BOUNCY;
        return mass * (vf / Time.deltaTime);
    }




    // DELETE THIS SOON
    public static Vector2 CalculateNetForce(Vector2 appliedForce, float coeffFriction, float mass) {
        Vector2 Ff = CalculateFrictionForce(coeffFriction, mass);
        return new Vector2(appliedForce.x - Ff.x, 0);
    }

    // Returns the coeffecient of friction for the ground
    public static float GetCoeff(bool isGrounded, GroundType type) {
        if (!isGrounded) { // If Jumping
            return COEFF_AIR;
        }
        else {
            switch (type) {
                case GroundType.Regular:
                    return COEFF_GROUND;
                case GroundType.Slippery:
                    return COEFF_SLIPPERY;
                case GroundType.Sticky:
                    return COEFF_STICKY;
            }
        }

        return COEFF_GROUND;
    }


}

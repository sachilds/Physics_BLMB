using UnityEngine;
using System.Collections;

public class PhysicsEngine : MonoBehaviour {
    public const float GRAVITY = -9.8f;

    // The force applied by Friction
	public static Vector2 CalculateFrictionForce(float coeff, float mass, float slopeAngle) {
        float Fw = CalculateForceWeight(mass);
        float Fn = CalculateNormalForce(Fw, slopeAngle);
        return new Vector2(Fn * coeff, 0);
    }

    // The force applied upwards on the object
    // Slope Angle should be in RADIANS
    public static float CalculateNormalForce(float Fw, float slopeAngle) {
        if (slopeAngle != 0)
            //return new Vector2(Fw * Mathf.Sin(slopeAngle), Fw * Mathf.Cos(slopeAngle));
            return Fw * Mathf.Cos(slopeAngle);
        else
            return Fw;
    }

    private static float CalculateForceWeight(float mass) {
        return mass * GRAVITY;
    }

    public static Vector2 CalculateNetForce(Vector2 Fa, float coeff, float mass, float slopeAngle) {
        Vector2 Ff = CalculateFrictionForce(coeff, mass, slopeAngle);
        float Fw = CalculateForceWeight(mass);
        return new Vector2(Fa.x - Ff.x, Fw - CalculateNormalForce(Fw, slopeAngle));
    }

    public static void TestPhysics() {
        Debug.Log("Friction force: coeff(0.5f), mass(10), slopeAngle(0)" + CalculateFrictionForce(0.5f, 10, 0));
        Debug.Log("Friction force: coeff(0.5f), mass(10), slopeAngle(3.14)" + CalculateFrictionForce(0.5f, 10, 3.14f));
        Debug.Log("Friction force: coeff(0.8f), mass(5), slopeAngle(0)" + CalculateFrictionForce(0.8f, 5, 0));
        Debug.Log("Friction force: coeff(0.1f), mass(3), slopeAngle(0)" + CalculateFrictionForce(0.1f, 3, 0));
    }
}

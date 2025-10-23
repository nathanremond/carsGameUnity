using UnityEngine;
using UnityEngine.InputSystem;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    public float acceleration = 1100f;
    public float breakingForce = 1000f;
    public float maxTurnAngle = 25f;

    private float currentAcceleration = 0f;
    private float currentBreakingForce = 0f;
    private float currentTurnAngle = 0f;

    public InputActionReference breakingAction;
    public InputActionReference HorizontalAction;
    public InputActionReference VerticalAction;

    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.1f, 0);
    }

    private void Start()
    {
        SetFriction(frontRight);
        SetFriction(frontLeft);

        SetFriction(backRight);
        SetFriction(backLeft);
    }

    private void FixedUpdate()
    {
        currentAcceleration = acceleration * VerticalAction.action.ReadValue<float>();

        currentTurnAngle = maxTurnAngle * HorizontalAction.action.ReadValue<float>();
        frontRight.steerAngle = currentTurnAngle;
        frontLeft.steerAngle = currentTurnAngle;

        if (breakingAction.action.IsPressed())
        {
            currentBreakingForce = breakingForce;
        }
        else
        {
            currentBreakingForce = 0f;
        }

        backLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakingForce;
        frontLeft.brakeTorque = currentBreakingForce;
        backRight.brakeTorque = currentBreakingForce;
        backLeft.brakeTorque = currentBreakingForce;

        updateWheel(frontRight, frontRightTransform);
        updateWheel(frontLeft, frontLeftTransform);
        updateWheel(backRight, backRightTransform);
        updateWheel(backLeft, backLeftTransform);
    }

    void updateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }

    void SetFriction(WheelCollider wheel)
    {
        WheelFrictionCurve forwardFriction = wheel.forwardFriction;
        WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;

        forwardFriction.extremumSlip = 0.4f;
        forwardFriction.extremumValue = 1.2f;
        forwardFriction.asymptoteSlip = 0.8f;
        forwardFriction.asymptoteValue = 1f;
        forwardFriction.stiffness = 2.5f;

        sidewaysFriction.extremumSlip = 0.3f;
        sidewaysFriction.extremumValue = 1.5f;
        sidewaysFriction.asymptoteSlip = 0.6f;
        sidewaysFriction.asymptoteValue = 1.2f;
        sidewaysFriction.stiffness = 3.5f;

        wheel.forwardFriction = forwardFriction;
        wheel.sidewaysFriction = sidewaysFriction;
    }
}

using UnityEngine;

public class RealisticBikeController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontWheelCollider;
    public WheelCollider backWheelCollider;
    
    public Transform steeringHandle;

    [Header("Wheel Meshes")]
    public Transform frontWheelMesh;
    public Transform backWheelMesh;

    [Header("Bike Settings")]
    public float motorPower = 1000f;
    public float brakePower = 2000f;
    public float maxSteerAngle = 25f;

    [Header("Balance")]
    public Transform centerOfMass;

    [Header("Leaning")]
    public Transform bikeBody;

    public float maxLeanAngle = 25f;
    public float leanSpeed = 5f;

    [Header("Mobile")]
    public Joystick joystick;

    public bool mobileBrake;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass =
            centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        float keyboardVertical =
            Input.GetAxis("Vertical");

        float keyboardHorizontal =
            Input.GetAxis("Horizontal");

        float joystickVertical = 0;
        float joystickHorizontal = 0;

        if (joystick != null)
        {
            joystickVertical = joystick.Vertical;
            joystickHorizontal = joystick.Horizontal;
        }

        float moveInput =
            Mathf.Abs(joystickVertical) > 0.1f
            ? joystickVertical
            : keyboardVertical;

        float turnInput =
            Mathf.Abs(joystickHorizontal) > 0.1f
            ? joystickHorizontal
            : keyboardHorizontal;

        // Motor
        backWheelCollider.motorTorque =
            moveInput * motorPower;

        // Steering
        float speedFactor =
            Mathf.Clamp01(
                rb.linearVelocity.magnitude / 20f
            );

        float steerAngle =
            Mathf.Lerp(
                maxSteerAngle,
                maxSteerAngle * 0.4f,
                speedFactor
            );

        float targetSteer =
            turnInput * steerAngle;
            frontWheelCollider.steerAngle =
        Mathf.MoveTowards(
            frontWheelCollider.steerAngle,
            targetSteer,
            2500f * Time.fixedDeltaTime
        );
        Vector3 currentRotation =
        transform.eulerAngles;

        if (currentRotation.z > 180)
            currentRotation.z -= 360;

        float smoothZ =
            Mathf.Lerp(
                currentRotation.z,
                0,
                5f * Time.fixedDeltaTime
            );

        transform.rotation =
            Quaternion.Euler(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                smoothZ
            );

        // Brake
        if (Input.GetKey(KeyCode.Space) || mobileBrake)
        {
            frontWheelCollider.brakeTorque =
                brakePower;

            backWheelCollider.brakeTorque =
                brakePower;
        }
        else
        {
            frontWheelCollider.brakeTorque = 0;
            backWheelCollider.brakeTorque = 0;
        }

        rb.angularVelocity =
        new Vector3(
            rb.angularVelocity.x,
            rb.angularVelocity.y,
            rb.angularVelocity.z * 0.3f
        );

        UpdateWheelMeshes();
        UpdateHandle(turnInput);
        LeanBike(turnInput);
        StabilizeBike();
    }

    void UpdateWheelMeshes()
    {
        UpdateWheel(
            frontWheelCollider,
            frontWheelMesh
        );

        UpdateWheel(
            backWheelCollider,
            backWheelMesh
        );
    }

    void UpdateWheel(
        WheelCollider collider,
        Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;

        collider.GetWorldPose(
            out pos,
            out rot
        );

        mesh.position = pos;
        mesh.rotation = rot;
    }

    void StabilizeBike()
    {
        Vector3 predictedUp =
            Quaternion.AngleAxis(
                rb.angularVelocity.magnitude *
                Mathf.Rad2Deg *
                0.02f /
                2f,
                rb.angularVelocity
            ) * transform.up;

        Vector3 torqueVector =
            Vector3.Cross(
                predictedUp,
                Vector3.up
            );

        rb.AddTorque(
            torqueVector * 50f
        );
    }
    void UpdateHandle(float turnInput)
    {
        if (steeringHandle == null) return;

        Quaternion targetRotation =
            Quaternion.Euler(
                0,
                turnInput * 25f,
                0
            );

        steeringHandle.localRotation =
            Quaternion.Lerp(
                steeringHandle.localRotation,
                targetRotation,
                Time.deltaTime * 8f
            );
    }
    void LeanBike(float turnInput)
    {
        if (bikeBody == null) return;

        float speed =
            rb.linearVelocity.magnitude;

        float targetLean =
            -turnInput *
            maxLeanAngle *
            Mathf.Clamp01(speed / 10f);

        Vector3 currentRotation =
            bikeBody.localEulerAngles;

        // Convert weird Unity angles
        if (currentRotation.z > 180)
            currentRotation.z -= 360;

        float smoothLean =
            Mathf.Lerp(
                currentRotation.z,
                targetLean,
                leanSpeed * Time.deltaTime
            );

        bikeBody.localRotation =
            Quaternion.Euler(
                0,
                0,
                smoothLean
            );
    }
  
}
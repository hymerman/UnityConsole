using UnityEngine;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add a rigid body to the capsule
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSWalker script to the capsule

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes Axes = RotationAxes.MouseXAndY;
    public float SensitivityX = 15F;
    public float SensitivityY = 15F;

    public float MinimumX = -360F;
    public float MaximumX = 360F;

    public float MinimumY = -60F;
    public float MaximumY = 60F;

    private float _rotationX;
    private float _rotationY;

    private Quaternion _originalRotation;

    private void Update()
    {
        if (Axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            _rotationX += Input.GetAxis("Mouse X") * SensitivityX;
            _rotationY += Input.GetAxis("Mouse Y") * SensitivityY;

            _rotationX = ClampAngle(_rotationX, MinimumX, MaximumX);
            _rotationY = ClampAngle(_rotationY, MinimumY, MaximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(_rotationY, -Vector3.right);

            transform.localRotation = _originalRotation * xQuaternion * yQuaternion;
        }
        else if (Axes == RotationAxes.MouseX)
        {
            _rotationX += Input.GetAxis("Mouse X") * SensitivityX;
            _rotationX = ClampAngle(_rotationX, MinimumX, MaximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector3.up);
            transform.localRotation = _originalRotation * xQuaternion;
        }
        else
        {
            _rotationY += Input.GetAxis("Mouse Y") * SensitivityY;
            _rotationY = ClampAngle(_rotationY, MinimumY, MaximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(-_rotationY, Vector3.right);
            transform.localRotation = _originalRotation * yQuaternion;
        }
    }

    private void Start()
    {
        // Make the rigid body not change rotation
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
            rigidBody.freezeRotation = true;
        _originalRotation = transform.localRotation;
    }

	private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
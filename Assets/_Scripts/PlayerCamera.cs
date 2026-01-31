using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    [Range(0f, 20f)][SerializeField] float sensitivity = 2f;

    [SerializeField] float yClamp = 88f;
    [SerializeField] float xClamp = 88f;

    Vector2 rotation = Vector2.zero;
    void Update()
    {
        rotation.x += Input.GetAxis("Mouse X") * sensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * sensitivity;

        rotation.y = Mathf.Clamp(rotation.y, -yClamp, yClamp);
        rotation.x = Mathf.Clamp(rotation.x, -xClamp, xClamp);

        var xQat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQat * yQat;
    }
}

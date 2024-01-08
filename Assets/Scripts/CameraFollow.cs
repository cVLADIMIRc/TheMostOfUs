using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // —сылка на объект, за которым следит камера
    public float smoothSpeed = 0.125f; // —корость следовани€ камеры

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

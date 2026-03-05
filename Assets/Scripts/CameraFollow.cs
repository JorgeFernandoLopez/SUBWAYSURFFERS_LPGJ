using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;
      [SerializeField]
    private float smoothspeed = 0.125f;
      [SerializeField]
      private float rightlimit = 0.5f;
      [SerializeField]
      private float lefttlimit = 0.5f;

     private Vector3 initialPosition;
    private Vector3 offset;
    private void Start()
    {
        Vector3 initialPosition = transform.position;
        offset = initialPosition - target.position;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position,desiredPosition, smoothspeed);
        transform.position = smoothPosition;
    }
}

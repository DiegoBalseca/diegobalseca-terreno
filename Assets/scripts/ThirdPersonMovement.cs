using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0, v).normalized;

        // dirección basada en la cámara controlada por Cinemachine
        Transform cam = Camera.main.transform;

        if (input.magnitude > 0.1f)
        {
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 moveDir = camForward * v + camRight * h;

            // rotación del personaje
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation =
                Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

            // movimiento
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // gravedad
        controller.Move(Vector3.down * 9.81f * Time.deltaTime);
    }
}

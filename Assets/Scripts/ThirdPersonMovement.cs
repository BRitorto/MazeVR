using System.Threading;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float speed;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;
    public KeyCode downKey;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 velocity;
    bool isGrounded;
    bool rotate;

    // Update is called once per frame
    void Update()
    {
        rotate = true;
        float hor = 0f;
        float ver = 0f;
        if (Input.GetKey(leftKey))
        {
            hor -= 1f;
        }
        else if (Input.GetKey(rightKey))
        {
            hor += 1f;
        }
        if (Input.GetKey(upKey))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        else if (Input.GetKey(downKey))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }

        Vector3 direction = new Vector3(hor, 0f, ver).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            if(rotate)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyCharacter : MonoBehaviour
{
    public float walkSpeed, runSpeed, xLookSensitivity, yLookSensitivity;
    public float jumpForce;
    public ForceMode forceMode = ForceMode.Force;

    Camera mainCam;
    Rigidbody rigid;
    Vector3 motion, rotation, cameraRotation;
    float speed;
    bool jump, isGrounded;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //Check if jump was pressed
        if (Input.GetAxis("Jump") == 1 && !jump)
            jump = true;

        //Check if the left shift is active or not
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        //Calculate players movement
        Vector3 horizontalMotion = transform.right * Input.GetAxis("Horizontal");
        Vector3 verticalMotion = transform.forward * Input.GetAxis("Vertical");
        motion = (horizontalMotion + verticalMotion).normalized * speed;

        //Calculate vertical & horizontal rotation * the look Sensitivity;
        rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0) * xLookSensitivity;
        cameraRotation = new Vector3(Input.GetAxis("Mouse Y"), 0, 0) * yLookSensitivity;
    }

    private void FixedUpdate()
    {
        //For jumping
        if (jump && isGrounded)
        {
            rigid.AddForce(Vector3.up * jumpForce, forceMode);
        }

        //Moving using WASD
        rigid.AddForce(motion, forceMode);

        //Rotating the player horizontaly
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(rotation));

        //Rotating the camera vertically
        if (mainCam != null)
        {
            mainCam.transform.Rotate(-cameraRotation);
            if (mainCam.transform.localRotation.x > 0.5)
            {
                mainCam.transform.localRotation = new Quaternion(0.55f, 0, 0, mainCam.transform.localRotation.w);
            }
            if(mainCam.transform.localRotation.x < -0.5)
            {
                mainCam.transform.localRotation = new Quaternion(-0.55f, 0, 0, mainCam.transform.localRotation.w);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Entered: " + collision.collider.name);
        //If the object touches the ground reset jump
        if (collision.collider.name == "Plane")
        {
            jump = false;
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // if the player leaves the ground prevent additional jumping
        if (collision.collider.name == "Plane")
        {
            isGrounded = false;
        }
    }
}

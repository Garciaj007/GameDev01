using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour {

    [Range(0, 10)]
    public float speed;
    [Range(0, 10)]
    public float rotationSpeed;
    [Range(0, 10)]
    public float jumpSpeed;
    [Range(0, 30)]
    public float gravity;

    CharacterController control;
    Vector3 moveDir;
    
	void Start () {
        control = GetComponent<CharacterController>();
	}
	
	void Update () {
        /*
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
        float curSpeed = Input.GetAxis("Vertical") * speed;
        control.SimpleMove(transform.forward * curSpeed);
        */

        if (control.isGrounded)
        {
            moveDir = new Vector3(0, 0, Input.GetAxis("Vertical"));
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
                moveDir.y = jumpSpeed;
        }

        moveDir.y -= gravity * Time.deltaTime;

        control.Move(moveDir * Time.deltaTime);

	}
}

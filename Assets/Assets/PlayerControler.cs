using UnityEngine;
//using https://www.youtube.com/watch?v=PmIPqGqp8UY
public class PlayerControler : MonoBehaviour
{

    public Transform playerCamera = null; //what does a serializefield do?
    readonly float MSens = 1.5f; //mouse sensitivity
    [Range(5, 50)]
    public float WalkSens = 1f; //walking speed
    readonly float MSmoothT = 0.01f; //Walk smoothing (larger=more smoothing)
    readonly float gravity = -30f; //gravity affecting the player
    float velocityY = 0.0f; //result of gravity
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVel = Vector2.zero;
    float CPitch = 0.0f;
    bool lockCursor = true;
    CharacterController controller = null;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePos();
        UpdateMovement();
    }
    void UpdateMousePos()
    {
        Vector2 MouseDiff = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        CPitch -= MouseDiff.y * MSens;
        CPitch = Mathf.Clamp(CPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * CPitch;
        transform.Rotate(Vector3.up * MouseDiff.x * MSens);

    }

    void UpdateMovement()
    {


        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize(); //normalize everything to the unit circle instead of 1 & sqrt(2)
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVel, MSmoothT);

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * WalkSens + Vector3.up * velocityY;


        if (controller.isGrounded)
        {
            velocityY = 0.0f;
            if (Input.GetButton("Jump")) { velocityY = 13f; }
        }
        velocityY += gravity * Time.deltaTime;
        velocity.y = velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

}

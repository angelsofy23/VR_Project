using System.Collections;
using UnityEngine;
 
 
public class FPS_Player : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpkey) && characterController.isGrounded;
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;
 
    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = false;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
 
    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpkey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
 
    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
 
    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;
 
    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;
 
    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;
 
    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 5f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;
 
    //private Vector3 hitPointNormal;
    private Camera playerCamera;
    private CharacterController characterController;
 
    private Vector3 moveDirection;
    private Vector2 currentInput;
 
    private float rotationX = 0;
 
    [Header("Footsteps objects")]
    AudioSource audioSource;
   
 
    [Header("Etiqueta Madera")]
    public AudioClip[] Pasosmadera;
    //public Texture madera;
 
    [Header("Etiqueta Pasto")]
    public AudioClip[] Pasospasto;
    //public Texture pasto;
 
    [Header("Etiqueta Piedra")]
    public AudioClip[] Pasospiedra;
    //public Texture piedra;
 
    [Header("Intervalo de pasos")]
    public float TimeBetweenSteps;
    float tiempo;
    public bool isMoving;
    public bool isSpriting;
    public bool isAgachado;
    float airTime;
 
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
 
        audioSource = GetComponent<AudioSource>();
       
    }
 
   private void OnControllerColliderHit(ControllerColliderHit hit)
    {
    
    }
 
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();
           
 
            if (canJump)
                HandleJump();
 
            if (canCrouch)
                HandleCrouch();
 
            if (canUseHeadbob)
                HandleHeadbob();
 
            ApplyFinalMovements();
 
        }
         float horizontal = Input.GetAxis("Horizontal");
         float vertical = Input.GetAxis("Vertical");
 
         if (horizontal != 0 || vertical != 0 && characterController.isGrounded)
         {
             isMoving = true;
         }
         else
         {
             isMoving = false;
             tiempo = Time.deltaTime;
         }
 
         if (IsSprinting)
         {
             isSpriting = true;
             TimeBetweenSteps = 0.5f;
         }
         else
         {
             isSpriting = false;
             TimeBetweenSteps = 1f;
         }
 
         if (isCrouching)
         {
             isAgachado = true;
             TimeBetweenSteps = 1.5f;
         }
         else
         {
             isAgachado = false;
         }
 
        
 
    }
 
  
    private void HandleMovementInput()
    {
 
        currentInput = new Vector2((IsSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
 
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
 
    }
 
    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
 
    }
 
    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }
 
    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }
 
    private void HandleHeadbob()
    {
        if (!characterController.isGrounded) return;
 
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                 playerCamera.transform.localPosition.x,
                 defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                 playerCamera.transform.localPosition.z);
        }
    }
 
    private void ApplyFinalMovements()
    {
 
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
 
    }
    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;
 
        duringCrouchAnimation = true;
       
 
        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;
 
        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
 
        characterController.height = targetHeight;
        characterController.center = targetCenter;
 
        isCrouching = !isCrouching;
 
        duringCrouchAnimation = false;
        canJump = !canJump;
        canSprint = !canSprint;
    }
 
}
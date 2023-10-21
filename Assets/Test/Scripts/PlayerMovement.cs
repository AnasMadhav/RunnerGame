using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;

    // FORWARD
    public float forwardSpeed = 8f;
    public float forwardSpeedMultiplier = 0.1f;
    public float initialAnimationSpeed = 0.95f;
    public float maxSpeed = 20f;

   //HORIZONTAL
    public float horizontalSpeed = 10f;
    public Vector3 diff, moveDir;
    public int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between tow lanes

    //VERTICAL
    public  bool isGrounded;
    public float gravity = -12f;
    public float jumpHeight = 2;
    public  Vector3 velocity;
    
    //SLIDING
    private bool isSliding = false;
    public float slideDuration = 1.5f;

    //COLLISION
    [SerializeField] int playerMaxHp = 3;
    public int playerCurrentHp;
    private bool alreadyHit = false;
    [HideInInspector] public bool isMovable;
  
    [SerializeField] public Animator animator;
    [SerializeField] GameManager gameManager;
    bool isMoving;

    [SerializeField] float maxTime = 3f;
    public float currentTime = 0;
    [SerializeField] public int distanceCovered;
    [SerializeField] int totalDistance;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        gameManager = GameObject.FindWithTag("GameManager").gameObject.GetComponent<GameManager>();

        playerCurrentHp = playerMaxHp;
        gameManager.HealthUpdate(playerCurrentHp);
        isMovable = false;
        currentTime = maxTime;

        if(!PlayerPrefs.HasKey("Distance"))
        {
            PlayerPrefs.SetInt("Distance",(int)gameObject.transform.position.z);
        }
        totalDistance = PlayerPrefs.GetInt("Distance");
        StartCoroutine(CountTimer());
    }

    private void Update()
    {
      //  velocity.y += gravity * Time.deltaTime;
        animator.SetBool("Move", isMovable);
        distanceCovered = (int)gameObject.transform.position.z;
        if(distanceCovered > totalDistance) 
        {
            //map change

        }
        if (isMovable)
        {
            move.z = forwardSpeed;
            if (forwardSpeed < maxSpeed) { forwardSpeed += forwardSpeedMultiplier * Time.deltaTime; }
            animator.SetFloat("Speed", Mathf.Clamp(initialAnimationSpeed += 0.001f * Time.deltaTime , initialAnimationSpeed, 1f));
            animator.SetBool("Ground", isGrounded);
            if (isGrounded && velocity.y < 0)
                velocity.y = -1f;

            if (isGrounded)
            {
                if (SwipeManager.swipeUp)
                    Jump();

                if (SwipeManager.swipeDown && !isSliding)
                    StartCoroutine(Slide());
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
                if (SwipeManager.swipeDown && !isSliding && isGrounded)
                {
                    StartCoroutine(Slide());
                    velocity.y = -10;
                }

            }
            controller.Move(velocity * Time.deltaTime);

            //Gather the inputs on which lane we should be
            if (SwipeManager.swipeRight)
            {
                desiredLane++;
                if (desiredLane == 3)
                   desiredLane = 2;
            }
            if (SwipeManager.swipeLeft)
            {
                desiredLane--;
                if (desiredLane == -1)
                    desiredLane = 0;
            }
            
            //Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (desiredLane == 0)
                targetPosition += Vector3.left * laneDistance;
            else if (desiredLane == 2)
                targetPosition += Vector3.right * laneDistance;

            //transform.position = targetPosition;
            if (transform.position != targetPosition)
            {
                diff = targetPosition - transform.position;
                moveDir = diff.normalized * horizontalSpeed * Time.deltaTime;
                if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                    controller.Move(moveDir);
                else
                    controller.Move(diff);
            }

            controller.Move(move * Time.deltaTime);
            animator.SetFloat("Horizontal", diff.x);
          //  animator.SetBool("Right", SwipeManager.swipeRight);
          //  animator.SetBool("Left", SwipeManager.swipeLeft);

        }
    }

    private void Jump()
    {
        StopCoroutine(Slide());
        animator.SetTrigger("Jump");
        isSliding = false;
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
        isGrounded = false;
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetTrigger("Slide");
        controller.center = new Vector3(0,-0.1f, 0);
        controller.height = 0.5f;
        yield return new WaitForSeconds(slideDuration);
        animator.SetTrigger("Run");
        yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2f;
        isSliding = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isGrounded = hit.gameObject.CompareTag("Ground");

        if (alreadyHit) return;
            if (hit.gameObject.CompareTag("Obstacle"))
            {
                    alreadyHit = true;
                    animator.SetTrigger("Hit");
                    isMovable = false;
                    gameManager.GameOver();
                
            }

       
    }
    IEnumerator CountTimer()
    {
        while(currentTime>0)
        {
          //  gameManager.TimerUpdate();
            currentTime -= 1;
            yield return new WaitForSeconds(1f);
        }
        gameManager.DisableTimer();
        isMovable = true;
     //   yield return null;
    }

    private void FixedUpdate()
    {
        
    }
}
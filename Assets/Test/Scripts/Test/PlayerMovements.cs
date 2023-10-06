using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    bool canJump;
    [SerializeField] public float moveSpeedZ = 8f;
    [SerializeField] float moveSpeedX = 1f;
    [SerializeField] float jumpforce = 5f;
    [SerializeField] float gravity = 20f;
    [SerializeField] float maxSpeedZ = 15f;
    [SerializeField] float initialAnimationSpeed = 0.95f;
    [SerializeField] public float speedMultiplierZ = 0.1f;
    [SerializeField] float swipeThreshold = 50f;
    [SerializeField] float slideDuration = 1.5f;
    Coroutine slideCoroutine;
    Vector3 touchStartPos, touchEndPos;
    float horizontalInput=0,verticalInput =0;
    Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] CharacterController controller;
    CapsuleCollider collider;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
      //  controller.Move(new Vector3(horizontalInput,verticalInput,moveSpeedZ * Time.deltaTime));
       // horizontalInput = 0;
       // verticalInput = 0;
        transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z + moveSpeedZ * Time.deltaTime);
        if (moveSpeedZ < maxSpeedZ) { moveSpeedZ += speedMultiplierZ * Time.deltaTime; }
        animator.SetFloat("Speed", Mathf.Clamp(initialAnimationSpeed+=0.001f*Time.deltaTime, initialAnimationSpeed, 1f));
        animator.SetBool("Ground", canJump);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                Vector3 swipeDistance = touchEndPos- touchStartPos;


                if (Mathf.Abs(swipeDistance.x) > swipeThreshold)
                {
                 //  horizontalInput = Mathf.Sign(swipeDistance.x) * 2;
                    transform.position += Vector3.right * Mathf.Sign(swipeDistance.x) * 2;
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2, 2), transform.position.y, transform.position.z);
                }
                else if(Mathf.Abs(swipeDistance.y)> swipeThreshold && canJump )
                {
                    if(Mathf.Sign(swipeDistance.y) > 0)
                    {
                        if(slideCoroutine!=null)
                        {
                            StopCoroutine(slideCoroutine);
                        }
                        animator.SetTrigger("Jump");
                        rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
                        canJump = false;
                      //  StartCoroutine(Jump());
                       
                    }
                    else
                    {
                            if(slideCoroutine == null)
                            slideCoroutine = StartCoroutine(Sliding());
                    }
                   
                }
            }
        }
        if(!canJump)
        {
           // verticalInput -= gravity * Time.deltaTime;
        }
       
   
    }

    IEnumerator Jump()
    {
        animator.SetTrigger("Jump");
        float elapsedtime = 0;
        while(elapsedtime<0.5f)
        {
            elapsedtime += Time.deltaTime * jumpforce;
            verticalInput = elapsedtime;
            controller.Move(new Vector3 (0, verticalInput,0));
           //Debug.Log(elapsedtime);
        }
        canJump = false;
        yield return null;

    }
    IEnumerator Sliding()
    {
        collider.direction = 2;
        collider.center = Vector3.up * 1.1f;
        animator.SetTrigger("Slide");
        yield return new WaitForSeconds(slideDuration);
        collider.direction = 1;
        collider.center = Vector3.up;
        slideCoroutine = null;
        animator.SetTrigger("Run");
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour
{
    float dragDistance;
    Animator animator;
    public float playerSpeed = 13;
    Rigidbody rb;
    public int jumpForce = 5;
    public GameObject ui;
    private Vector3 fp;
    private Vector3 lp;
    public float desiredLerpDuration = 2f;
    int xPos = 0;
    float[] fixedXPos = { -2.5f, 0, 2.5f };
    bool firstTimeRightMove = true;
    string controlName;
    public Canvas buttonControlUi;
    bool isPlayerAlive = true;
    public ParticleSystem starParticle;
    bool isMovingUp = false;
    bool isMovingDown = false;
    int increaseSpeedAfter = 200;
    public GameObject hurdles;
    void Start()
    {
        dragDistance = Screen.height * 5 / 100;
        animator = GetComponent<Animator>();
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();

        if (PlayerPrefs.HasKey("Controls"))
        {
            controlName = PlayerPrefs.GetString("Controls");
            Debug.Log(controlName + "saved string");
        }
        hurdles.GetComponent<Hurdles>().setHurdleDistance();
    }
    void Update()
    {
        if (transform.position.z > increaseSpeedAfter && playerSpeed < 20)
        {
            playerSpeed++;
            increaseSpeedAfter += 240;
            hurdles.GetComponent<Hurdles>().increaseHurdlesDistance();
        }
        if (isPlayerAlive)
        {
            transform.Translate(new Vector3(0, 0, 1) * playerSpeed * Time.deltaTime);
        }
        if (controlName == "Touch")
        {
            buttonControlUi.gameObject.SetActive(false);
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                //else if (touch.phase == TouchPhase.Moved)
                //{
                //    lp = touch.position;
                //}
                else if (touch.phase == TouchPhase.Ended)
                {
                    lp = touch.position;
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {

                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {
                            if ((lp.x > fp.x) && (transform.position.x < 2f))
                            {
                                moveRight();
                            }
                            else if ((lp.x < fp.x) && (transform.position.x > -2f))
                            {
                                moveLeft();
                            }
                        }
                        else
                        {
                            if (lp.y > fp.y)
                            {
                                moveUp();
                            }
                            else
                            {
                                moveDown();
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Tap");
                    }
                }

            }
        }

    }
    public void moveLeft()
    {
        if ((transform.position.x > -2f) && isPlayerAlive)
        {
            firstTimeRightMove = false;
            if (xPos > 0)
            {
                xPos--;
                animationControl();
                animator.SetTrigger("leftMove");
            }
            Debug.Log("Left Swipe " + xPos);
            StartCoroutine(HorizontalMove());
        }
    }
    public void moveRight()
    {
        if (transform.position.x < 2f && isPlayerAlive)
        {
            if (xPos < 3)
            {
                xPos++;
                animationControl();
                animator.SetTrigger("RightMove");
                if (firstTimeRightMove == true)
                {
                    xPos++;
                    firstTimeRightMove = false;
                }
            }
            Debug.Log("Right Swipe " + xPos);
            StartCoroutine(HorizontalMove());
        }
    }
    public void moveUp()
    {
        if (transform.position.y < 0.4f && isPlayerAlive)
        {
            Debug.Log("Up Swipe");
            animationControl();
            animator.SetTrigger("Jump");
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            isMovingUp = true;
            Invoke("UpMoveFalse", .9f);
        }
    }
    public void moveDown()
    {
        if (isPlayerAlive)
        {
            animationControl();
            animator.SetTrigger("slide");
            Debug.Log("Down Swipe");
            isMovingDown = true;
            Invoke("downMoveFalse", .7f);
        }
    }
    void downMoveFalse()
    {
        isMovingDown = false;
        Debug.Log("false");
    }
    void UpMoveFalse()
    {
        isMovingUp = false;
        Debug.Log("false");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TrafficCone")
        {
            collideWithObstacle();
        }
        if (other.tag == "BarrierHigh" && isMovingDown == false) //Or we can check whether the animation completed or not animator.GetCurrentAnimatorStateInfo(0).IsName("Running Slide") == false
        {
            collideWithObstacle();
        }
        if (other.tag == "BarrierLow" && isMovingUp == false) //animator.GetCurrentAnimatorStateInfo(0).IsName("Running Jump") == false
        {
            collideWithObstacle();
        }
    }
    void collideWithObstacle()
    {
        Debug.Log("collide");
        isPlayerAlive = false;
        gameObject.transform.position += new Vector3(0, 0, -1);
        starParticle.transform.position = gameObject.transform.position + new Vector3(0, 1.5f, -.5f);
        starParticle.Play();
        animator.SetTrigger("KnockedDown");
        Invoke("gameOver", 3);
    }
    void gameOver()
    {
        ui.GetComponent<Ui>().GameOver();
    }

    void animationControl()
    {
        animator.Rebind();
    }
    IEnumerator HorizontalMove()
    {
        Vector3 targetPos = new Vector3(fixedXPos[xPos], transform.position.y, transform.position.z);
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;
        while (elapsedTime < desiredLerpDuration && isPlayerAlive)
        {
            transform.position = Vector3.Lerp(new Vector3(startPosition.x, transform.position.y, transform.position.z), new Vector3(targetPos.x, transform.position.y, transform.position.z), elapsedTime / desiredLerpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //transform.position = targetPos;
    }
    private void OnDestroy()
    {
        hurdles.GetComponent<Hurdles>().setHurdleDistance();
    }
}
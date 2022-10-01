using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MinionController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveDir;
    public float speed = 5f;
    int countDown = 10;
    public TextMeshPro countDownUI;
    ModeController modeController;
    float jumpPower = 10;
    SkinnedMeshRenderer ren;
    Animator animator;
    bool dead;
    float randomStartDir;
    bool justChangedDir;
    public Color deathColor;


    // Start is called before the first frame update
    void Start()
    {
        moveDir = new Vector3(1,0,0);
        rb = GetComponent<Rigidbody>();
        modeController = GameObject.FindWithTag("GameController").GetComponent<ModeController>();
        ren = GetComponentInChildren<SkinnedMeshRenderer>();
        animator = GetComponentInChildren<Animator>();
        randomStartDir = UnityEngine.Random.Range(0,2);
        if(randomStartDir < 0.5)
        {
            ChangeDir();
        }
    }

    private void Update()
    {
        
    }



    // Update is called once per frame
    void FixedUpdate()
    {
       rb.MovePosition(transform.position + moveDir * Time.deltaTime * speed);
    }

    public void ChangeDir()
    {
        moveDir.x = moveDir.x * -1;
        if (moveDir.x < 0)
        {
            transform.GetChild(1).localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else
        {
            transform.GetChild(1).localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Debug.Log("Jump");
        animator.SetTrigger("jump");
    }

    private void OnMouseDown()
    {
        if(modeController.mode == ModeController.Mode.ChangeDir)
        {
            ChangeDir();
        }
        if (modeController.mode == ModeController.Mode.Comfort)
        {
            Comfort();
        }
        if (modeController.mode == ModeController.Mode.Jump)
        {
            Jump();
        }

    }

    public void Discomfort()
    {
        if (!dead)
        {
            animator.SetBool("sad", true);
            StartCoroutine(CountingDown());
        }
    }

    private void Comfort()
    {
        if (!dead)
        {
            StopAllCoroutines();
            ren.materials[0].color = Color.white;
            countDown = 10;
            countDownUI.text = "";
            animator.SetBool("sad", false);
        }
    }

    IEnumerator CountingDown()
    {
        countDownUI.text = countDown.ToString();
        yield return new WaitForSeconds(1);
        if (countDown > 0)
        {
            ren.materials[0].color = Color.yellow;
            countDown--;
            StartCoroutine(CountingDown());
        }
        else
        {
            ren.materials[0].color = deathColor;
            animator.SetTrigger("die");
            StartCoroutine(WaitAndDie());
        }
        

    }

    IEnumerator WaitAndDie()
    {
        speed = 0;
        dead = true;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            animator.SetBool("falling", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            animator.SetBool("falling", false);

        if (collision.gameObject.CompareTag("Minion"))
        {
            
            if (!justChangedDir)
            {
                ChangeDir();
                justChangedDir = true;
                StartCoroutine(WaitAndReset());
            }
        }
        
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1);
        justChangedDir = false;
    }

}

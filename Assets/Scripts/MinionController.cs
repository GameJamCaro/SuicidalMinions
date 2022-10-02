using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

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
    public Color normalColor;
    public Color deathColor;
    public Color sadColor;


    // Start is called before the first frame update
    void Start()
    {
        moveDir = new Vector3(1,0,0);
        rb = GetComponent<Rigidbody>();
        modeController = GameObject.FindWithTag("GameController").GetComponent<ModeController>();
        ren = GetComponentInChildren<SkinnedMeshRenderer>();
        animator = GetComponentInChildren<Animator>();
       
        
    }

    public void RandomizeDir()
    {
        randomStartDir = UnityEngine.Random.Range(0, 2);

        if (randomStartDir < 0.5)
        {
            ChangeDir();
        }
    }

    public float raylength;
    public LayerMask layerMask;


    private void Update()
    {
    //    if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
    //    {
    //        RaycastHit hit;
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.Raycast(ray, out hit, raylength, layerMask))
    //        {
    //            if (modeController.mode == ModeController.Mode.ChangeDir)
    //            {
    //                ChangeDir();
    //            }
    //            if (modeController.mode == ModeController.Mode.Comfort)
    //            {
    //                Comfort();
    //            }
    //            if (modeController.mode == ModeController.Mode.Jump)
    //            {
    //                Jump();
    //            }
    //        }
    //    }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
       rb.MovePosition(transform.position + moveDir * Time.deltaTime * speed);
        if (moveDir.x < 0)
        {
            transform.GetChild(1).localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else
        {
            transform.GetChild(1).localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }


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
        if (moveDir.x < 0)
        {
            transform.GetChild(1).localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else
        {
            transform.GetChild(1).localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }

    //private void OnMouseDown()
    //{
    //    if (modeController.mode == ModeController.Mode.ChangeDir)
    //    {
    //        ChangeDir();
    //    }
    //    if (modeController.mode == ModeController.Mode.Comfort)
    //    {
    //        Comfort();
    //    }
    //    if (modeController.mode == ModeController.Mode.Jump)
    //    {
    //        Jump();
    //    }

    //}

    public void Discomfort()
    {
        if (!dead)
        {
            animator.SetBool("sad", true);
            StartCoroutine(CountingDown());
        }
    }

    public void Comfort()
    {
        if (!dead)
        {
            StopAllCoroutines();
            ren.materials[0].SetColor("_EmissionColor", normalColor);
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
            ren.materials[0].SetColor("_EmissionColor", sadColor); 
            countDown--;
            StartCoroutine(CountingDown());
        }
        else
        {
            ren.materials[0].SetColor("_EmissionColor", deathColor);
            animator.SetTrigger("die");
            StartCoroutine(WaitAndDie());
        }
        

    }

    IEnumerator WaitAndDie()
    {
        speed = 0;
        dead = true;
        
        yield return new WaitForSeconds(3);
        modeController.gameObject.GetComponent<Score>().AddLeavingtMinion();
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



    void UnityApiMouseEvents()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.back ,out hit))
        {
            if (modeController.mode == ModeController.Mode.ChangeDir)
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

    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1);
        justChangedDir = false;
    }

}

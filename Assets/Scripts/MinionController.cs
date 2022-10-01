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


    // Start is called before the first frame update
    void Start()
    {
        moveDir = new Vector3(1,0,0);
        rb = GetComponent<Rigidbody>();
        modeController = GameObject.FindWithTag("GameController").GetComponent<ModeController>();
       
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
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Debug.Log("Jump");
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
        StartCoroutine(CountingDown());
    }

    private void Comfort()
    {
        StopAllCoroutines();
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        countDown = 10;
        countDownUI.text = ""
;   }

    IEnumerator CountingDown()
    {
        countDownUI.text = countDown.ToString();
        yield return new WaitForSeconds(1);
        if (countDown > 0)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
            countDown--;
            StartCoroutine(CountingDown());
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            StartCoroutine(WaitAndDie());
        }
        

    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }


}

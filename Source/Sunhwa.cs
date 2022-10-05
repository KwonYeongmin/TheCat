using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR.WSA;

public class Sunhwa : MonoBehaviour
{
    private float mousePosX;

    private bool bisDLStart = false;


    private Vector2 Pos;
    private Animator animator;
   

    private bool bisMouseDown;
    private bool bisKeyDown;


    private ChapterManager chapter;

    private float Value = 1.2f;

    public void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        chapter = GameObject.FindWithTag("Chapter").GetComponent<ChapterManager>();
    }


    public void Update()
    {
        //마우스
        if (Input.GetMouseButtonDown(0)) { bisMouseDown = true; }
        if (Input.GetMouseButtonUp(0)) { bisMouseDown = false; }

        if (bisMouseDown)
        {
            mousePosX = Input.mousePosition.x;
            //왼쪽으로이동
            if (mousePosX > 0 && mousePosX < Screen.width / 4) { SH_controll(-0.05f, -Mathf.Abs(transform.localScale.x)); }
            //오른쪽으로 이동
            else if (mousePosX < Screen.width && mousePosX > Screen.width / 4 * 3) { SH_controll(0.05f, Mathf.Abs(transform.localScale.x)); }
        }
        else
           // chapter.AnimationSetBool(animator, "isSHWalk", false);
           animator.SetBool("isSHWalk", false); 


            //키보드
        if (Input.GetKeyDown(KeyCode.LeftArrow ) || Input.GetKeyDown(KeyCode.RightArrow))
            bisKeyDown = true;
        else if (Input.GetKeyUp(KeyCode.LeftArrow ) || Input.GetKeyUp(KeyCode.RightArrow))
            bisKeyDown = false;


        if (bisKeyDown)
        {
            keyboardMove();

            if (Input.GetKeyDown(KeyCode.LeftArrow)) { transform.localScale = new Vector3(- Mathf.Abs(transform.localScale.x), transform.localScale.y, 1); }
            else if(Input.GetKeyDown(KeyCode.RightArrow)) { transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1); }
        }
    }


    void keyboardMove()
    {
       float moveSpeed = 4f;

        if (!bisDLStart) 
        {
            moveSpeed = 4f;
            float horizontal_ = Input.GetAxis("Horizontal");
            Vector3 v = new Vector3(horizontal_, 0, 0);
            transform.Translate(v * moveSpeed * Time.deltaTime);
            animator.SetBool("isSHWalk", true);
            if (GameManager.Instance.Chapter==3)
                this.transform.GetChild(0).GetComponent<Animator>().SetBool("isSHWalk", true);
        }
        else 
        {
            moveSpeed = 0f;
            float horizontal_ = Input.GetAxis("Horizontal");
            Vector3 v = new Vector3(horizontal_, 0, 0);
            transform.Translate(v * moveSpeed * Time.deltaTime);
            animator.SetBool("isSHWalk", false);
            if (GameManager.Instance.Chapter == 3)
                this.transform.GetChild(0).GetComponent<Animator>().SetBool("isSHWalk", false);
        }
    }


    public void SH_controll(float value,float b) 
    {
            if (!bisDLStart)
            {
            transform.position = new Vector2(transform.position.x + value, transform.position.y);
            animator.SetBool("isSHWalk", true);
            transform.localScale = new Vector3(b, transform.localScale.y, 1);
            if (GameManager.Instance.Chapter == 3)
                this.transform.GetChild(0).GetComponent<Animator>().SetBool("isSHWalk", true);

        }
            else
            {
            transform.position = new Vector2(transform.position.x, transform.position.y);
            animator.SetBool("isSHWalk", false);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            if (GameManager.Instance.Chapter == 3)
                this.transform.GetChild(0).GetComponent<Animator>().SetBool("isSHWalk", false);
        }
    }


    public void SHNotMove() { bisDLStart = true;  }
    public void SHMove() { bisDLStart = false; }


    public bool bIsTrigger = false;
    public EventBox eventBox { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EventBox>())
        {
            eventBox = collision.gameObject.GetComponent<EventBox>();
            bIsTrigger = true;

            GameObject.FindWithTag("Chapter").GetComponent<ChapterManager>().TriggerEventBox();
        }
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    public float maxDis = 2;

    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;
    public Transform rightPos;
    public Transform leftPos;

    public LineRenderer rightLr;
    public LineRenderer leftLr;

    protected SpriteRenderer render;
    public Sprite hurted;

    public GameObject boom;//爆炸特效

    public float smooth = 3;

    public AudioClip select;
    public AudioClip flying;

    protected TestMyTrail myTrail;

    private bool isClick = false;
    [HideInInspector]
    public bool ifCtrled = false;
    private bool ifFly = false;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (ifCtrled)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;//true表示该物体运动状态不受外力，碰撞和关节的影响
            rightLr.enabled = true;
            leftLr.enabled = true;
        }
    }
    private void OnMouseUp()
    {
        if (ifCtrled)
        {
            isClick = false;
            rg.isKinematic = false;//松开时收到重力等影响
            rightLr.enabled = false;
            leftLr.enabled = false;

            Invoke("Fly", 0.2F);
            ifCtrled = false;//松开后不可控制
        }
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())//判断是否点击UI界面
        {
            return;
        }
        if (isClick)
        {//鼠标一直按下,进行位置跟随
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化向量
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }
            Line();
        }

        //相机跟随
        float posX = transform.position.x;
        Vector3 CameraPos = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.Lerp(CameraPos,new Vector3(Mathf.Clamp(posX,0,15),CameraPos.y,CameraPos.z),smooth*Time.deltaTime);

        if (ifFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    private void Fly()
    {
        ifFly = true;
        AudioPlay(flying);
        myTrail.StartTrail();
        sp.enabled = false;
        Invoke("Next", 5);
    }

    void Line()
    {
        rightLr.SetPosition(0, rightPos.position);
        rightLr.SetPosition(1, transform.position);
        rightLr.useWorldSpace = true;

        leftLr.SetPosition(0, leftPos.position);
        leftLr.SetPosition(1, transform.position);
        leftLr.useWorldSpace = true;

        //print(collision.relativeVelocity.magnitude);
    }

    /// <summary>
    /// 下一只小鸟的飞出
    /// </summary>
    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position,Quaternion.identity);

        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ifFly = false;
        myTrail.CleanTrail();
    }

    public void AudioPlay(AudioClip ac)
    {
        AudioSource.PlayClipAtPoint(ac, transform.position);
    }

    public virtual void ShowSkill()
    {
        ifFly = false;
    }

    public void Hurt()
    {
        render.sprite = hurted;
    }

    public bool GetIfFly()
    {
        return ifFly;
    }
 }

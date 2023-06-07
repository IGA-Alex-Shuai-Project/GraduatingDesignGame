using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using TMPro;

public class LaunchBall : MonoBehaviour
{
    HandControl handcontroL;
    public GameObject hand;
    [Header("SetBall")]
    public GameObject Ball;
    public Transform BallResetPoint;//球的生成点
    [Header("SetForce")]
    public float ForceRate;//力的增加速度
    [Header("SetTarget")]
    public Transform Target;
    public float MaxDistance;//框的最远距离
    public float MinDistance;//框的最近距离
    [Header("SetUI")]
    public TMP_Text ScoreText;

    public float Force = 0;
    Rigidbody rb;
    bool isHolding=false;
    bool canHold=false;
    GameObject curBall;
    float score=0;
    private float handValue ;
    void Start()
    {
        ResetBall();
        handcontroL = hand.GetComponent<HandControl>();
    }

    void Update()
    {
        handValue = handcontroL.FingerDegreeAll;


        if (canHold)
        {
            if (handValue>75) //替换为紧握阈值
            {
                isHolding = true;
            }
            if (handValue<15) //替换为松开阈值
            {
                isHolding = false;
                Launch();
            }
        }
        if (isHolding)
        {
            Force += Time.deltaTime * ForceRate;
        }
    }
    public void Launch()
    {
        if (Force > 0)
        {
            canHold = false;
        }
        Vector3 launchDeraction=BallResetPoint.up;
        rb.AddForce(launchDeraction * Force);

        Invoke(nameof(ResetBall), 3f);
        
     
    }

    public void ResetBall()
    {
        if(Force> 0)
        {
            Destroy(curBall);
            curBall = Instantiate(Ball, BallResetPoint.position, Quaternion.identity);
            rb = curBall.GetComponent<Rigidbody>();
            curBall.GetComponent<Ball>().launcher = this;
        }
        Force = 0;
        canHold = true;
    }

    public void ReaetTarget()
    {
        score += 1;
        ScoreText.text = score.ToString();
        float x = Random.Range(MinDistance, MaxDistance);
        Target.position = new Vector3(Target.position.x, Target.position.y, transform.position.z + x);
    }
}

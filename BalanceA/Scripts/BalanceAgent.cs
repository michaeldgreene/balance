﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceAgent : Agent
{

    public GameObject ball;
    public TextMesh tBox;
    public float level;
    public float maxLevel;
    public float timeCounter;
    public float dampFactor;
    public bool increment;
    public float[] rateA;


    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            Vector3 currentVel = gameObject.GetComponent<Rigidbody>().angularVelocity;
            dampFactor = increment ? Mathf.Pow(maxLevel, (level * 0.000206963F)) - 1F : dampFactor;

            float Xchange = Mathf.SmoothDamp(currentVel.x, vectorAction[0], ref rateA[0], dampFactor);
            float Ychange = Mathf.SmoothDamp(currentVel.y, vectorAction[1], ref rateA[1], dampFactor);
            float Zchange = Mathf.SmoothDamp(currentVel.z, vectorAction[2], ref rateA[2], dampFactor);

            gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(Xchange, Ychange, Zchange);

            timeCounter += Time.deltaTime;
            tBox.text = "DF: " + dampFactor.ToString("f4");
            tBox.text = increment ? "ML:" + maxLevel.ToString() + " L:" + level.ToString() + " || " + tBox.text : tBox.text;

            SetReward(Time.deltaTime);
        }
        if (timeCounter > 9)
        {
            level += 1;
            timeCounter = 0;
            AgentReset();
        }
        if (ball.transform.position.y < 0.3F)
        {
            level = 1;
            timeCounter = 0;
            Done();
        }
    }

    public override void CollectObservations()
    {
        AddVectorObs(Time.deltaTime);
        AddVectorObs(transform.rotation);
        AddVectorObs(ball.transform.position - new Vector3(0, 2.25F, -3));
        AddVectorObs(ball.GetComponent<Rigidbody>().velocity);
    }

    public override void AgentReset()
    {
        if (level > maxLevel)
        {
            maxLevel = level;
        }
        transform.position = new Vector3(0, 2, -3);
        transform.rotation = new Quaternion(0F, 0F, 0F, 0F);
        ball.transform.position = new Vector3(2, 5, -3);
        ball.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-4F, -1F), 0, 0);
        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0F, 0F, 0F);
        for (int i = 0; i < rateA.Length; i++)
        {
            rateA[i] = 0F;
        }
    }

}

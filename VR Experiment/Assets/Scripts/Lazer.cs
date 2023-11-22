using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEditor.Rendering;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

public class Lazer : MonoBehaviour
{
    public LayerMask targetMask;
    bool triggerValue;
    bool lastShot;
    bool fire;
    public int delayCountStart;
    int delayCount;
    float gameTime = 10;
    int totalShots = 0;
    int shotTargets = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0f)
        {
            Write();

            Debug.Log("Time is up");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;

        }
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        
        foreach (var device in inputDevices) if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue && !lastShot)
            {
                fire = true;
                lastShot = true;
                delayCount = delayCountStart;
                Debug.Log("Shots fired");
                totalShots += 1;
                
            }
        if (!triggerValue)
        {
            lastShot = false;
        }
    }

    void FixedUpdate()
    {
        if (fire)
        {
            delayCount -= 1;
            if(delayCount < 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, targetMask))
                {
                    hit.collider.gameObject.transform.parent.GetComponent<targetSpawner>().startSpawnTimer();
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Targets Successfully Hit");
                    shotTargets += 1;
                }
                fire = false;
            }
        }
    }

    void Write()
    {
        string filePath = "C:/Users/Jess/OneDrive - Colostate/Desktop" + "/SavedData.csv";
        StreamWriter writer = new StreamWriter(filePath);
        writer.WriteLine($"Hits: {shotTargets}");
        writer.WriteLine("TotalShots: " + totalShots);
        writer.Flush();
        writer.Close();
    }

}

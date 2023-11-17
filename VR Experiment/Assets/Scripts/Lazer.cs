using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class Lazer : MonoBehaviour
{
    public LayerMask targetMask;
    bool triggerValue;
    bool lastShot;
    bool fire;
    public int delayCount20msStart;
    int delayCount20ms;
    // Start is called before the first frame update
    void Start()
    {
        string filePath = getPath();
        StreamWriter writer = new StreamWriter(filePath);
        writer.WriteLine("Hello,World");
        writer.Flush();
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        
        foreach (var device in inputDevices) if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue && !lastShot)
            {
                fire = true;
                lastShot = true;
                delayCount20ms = delayCount20msStart;
                //Debug.Log("Cawabummer");
                
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
            delayCount20ms -= 1;
            if(delayCount20ms < 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, targetMask))
                {
                    hit.collider.gameObject.transform.parent.GetComponent<targetSpawner>().startSpawnTimer();
                    Destroy(hit.collider.gameObject);
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                }
                fire = false;
            }
        }
    }

    private string getPath()
    {
        return "/sdcard/Download" + "/" + "Saved_Data.csv";
    }
}

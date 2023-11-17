using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public LayerMask targetMask;
    bool triggerValue;
    bool lastShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        RaycastHit hit;
        foreach (var device in inputDevices) if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue && !lastShot)
            {
                lastShot = true;
                //Debug.Log("Cawabummer");
                if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, targetMask))
                {
                    hit.collider.gameObject.transform.parent.GetComponent<targetSpawner>().startSpawnTimer();
                    Destroy(hit.collider.gameObject);
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                }
            }
        if (!triggerValue)
        {
            lastShot = false;
        }
    }
}

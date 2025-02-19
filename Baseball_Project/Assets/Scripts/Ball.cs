using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float myTimeScale = 1.0f;
    public GameObject target;
    public float launchForce = 10f;
    Rigidbody rb;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Time.timeScale = myTimeScale; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Pitch());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FiringSolution fs = new FiringSolution();
            Nullable<Vector3> aimVector = fs.Calculate(transform.position, target.transform.position, launchForce, Physics.gravity);
            if (aimVector.HasValue)
            {
                rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // reset
            rb.isKinematic = true;
            transform.position = startPos;
            rb.isKinematic = false;
        }
        */
    }

    void ResetBall()
    {
        rb.isKinematic = true;
        transform.position = startPos;
        rb.isKinematic = false;
    }

    void ThrowBall()
    {
        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = fs.Calculate(transform.position, target.transform.position, launchForce, Physics.gravity);
        if (aimVector.HasValue)
        {
            rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
        }
    }

    void StartPitch()
    {
        StartCoroutine(Pitch());
    }

    IEnumerator Pitch()
    {
        yield return new WaitForSeconds(5);
        ResetBall();
        ThrowBall();
        StartPitch();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bat")
        {
            Debug.Log("HIT");
            rb.AddForce(Vector3.zero);
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            rb.AddForce(new Vector3(0, UnityEngine.Random.Range(30, 90), UnityEngine.Random.Range(-30,-90)), ForceMode.VelocityChange);
        }
    }
}

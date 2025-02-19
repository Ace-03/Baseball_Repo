using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    Rigidbody rb;

    private float startTime;

    private bool canSwing = true;

    public float swingSpeed = 1.0f;

    public GameObject BaseballBat;
    Material batMat;

   public Material[] materialList;

    // Start is called before the first frame update
    void Start()
    {
        batMat = BaseballBat.GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSwing)
        {
            //float swingComplete = (Time.time - startTime) / swingSpeed;


            /*
            float currentAngle = transform.eulerAngles.y;
            float targetAngle = -20;
            float newAngle = Mathf.Lerp(currentAngle, targetAngle, swingSpeed * Time.deltaTime);

            this.transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, newAngle, transform.eulerAngles.z));
            */

            //this.transform.rotation = Quaternion.Euler(Vector3.Lerp(this.transform.rotation.eulerAngles, new Vector3(0, -20, 0), swingSpeed * Time.deltaTime));
            
            canSwing = false;
            batMat = materialList[1];
            SwingBat();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }

    void SwingBat()
    {
        StartCoroutine(SwingingBat());
    }

    IEnumerator SwingingBat()
    {
        float currentAngle = transform.eulerAngles.y;
        while (currentAngle < 180 || currentAngle > 260)
        {
            this.transform.rotation = Quaternion.Euler(Vector3.Lerp(this.transform.rotation.eulerAngles, new Vector3(0, 220, 0), swingSpeed * Time.deltaTime));
            currentAngle = transform.eulerAngles.y;
            yield return null;
        }
        currentAngle = 0;
        //Debug.Log("angle = " + currentAngle);
        yield return new WaitForSeconds(3);
        batMat = materialList[0];
        canSwing = true;

        /*
        Debug.Log("Swing");
        this.transform.rotation = Quaternion.Euler(Vector3.Lerp(this.transform.rotation.eulerAngles, new Vector3(0, -20, 0), swingSpeed * Time.deltaTime));
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Baseball")
        {
            Debug.Log("BALL HIT");
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            batMat = materialList[0];
            canSwing = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bruh : MonoBehaviour
{
    int score = 0;
    public float walkSpeed = 10.0f;
    public float jumpSpeed = 5.0f;
    public GameObject HiddenItem;


    string someString;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void OnTriggerEnter(Collider other)
    {
        {
            Debug.Log(other.gameObject.tag);
        }


        if(other.gameObject.tag == "Item")
        {
            this.gameObject.GetComponent<SFX>().Play_item_sfx();

            Destroy(other.gameObject);
            score++;


            if(score == 5)
            {
                HiddenItem.SetActive(true);
            }
        }
    }

    void Update()
    {
        float moveForward = Input.GetAxis("Vertical") * walkSpeed;
        float moveSide = Input.GetAxis("Horizontal") * walkSpeed;

        moveForward = Time.deltaTime;
        moveSide= Time.deltaTime;

        this.transform.Translate(moveSide, 0, moveForward);

        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jump");
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1*jumpSpeed, 0));
        }


    }
}

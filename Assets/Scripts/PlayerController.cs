using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5.0f;
    [SerializeField]
    public float jumpForce = 3.0f;

    private Rigidbody rig;


    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        Debug.Assert(rig);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();

        }
        Move();
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        if (xInput != 0 || zInput != 0)
        {
            Debug.Assert(moveSpeed > 0f);

            //UberDebug.LogChannel("Player", "{0}, {1}", xInput, zInput);

            Vector3 direction = new Vector3(xInput, 0, zInput) * moveSpeed;

            rig.velocity = new Vector3(direction.x, rig.velocity.y, direction.z);

            Vector3 facing = new Vector3(xInput, 0, zInput);

            if (facing.magnitude > 0)
            {
                transform.forward = facing;
            }
        }
    }

    void TryJump()
    {
        UberDebug.LogChannel("Player", "In Try Jump");

        Ray[] rays = new Ray[4];
        rays[0] = new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down);
        rays[1] = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down);
        rays[2] = new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down);
        rays[3] = new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down);

        foreach (Ray ray in rays)
        {
            if(Physics.Raycast(ray, 0.7f))
            {
                rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

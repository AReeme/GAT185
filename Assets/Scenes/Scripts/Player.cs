using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    public GameObject prefab;

	private void Awake()
	{
        Debug.Log("Awake");
	}

	void Start()
    {
        Debug.Log("Start");
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        //if (Input.GetKey(KeyCode.A)) direction.x = -1;
        //if (Input.GetKey(KeyCode.D)) direction.x = +1;
        //if (Input.GetKey(KeyCode.S)) direction.z = -1;
        //if (Input.GetKey(KeyCode.W)) direction.z = +1;

        transform.position += direction * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Pew!");
            GetComponent<AudioSource>().Play();
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}

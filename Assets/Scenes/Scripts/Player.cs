using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 50)] public float speed = 5;
    [Range(1, 360)] public float rotationRate = 180;
    public GameObject prefab;
    public Transform bulletSpawnLocation;

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

        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);
        transform.rotation = transform.rotation * rotate;
        transform.Translate(direction * speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Pew!");
            //GetComponent<AudioSource>().Play();
            GameObject go = Instantiate(prefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerPlayer : MonoBehaviour
{
    [SerializeField] private Transform view;
    [SerializeField] private float maxForce = 5;
    [SerializeField] private float groundRayLength = 1;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] AudioSource jump;
    [SerializeField] int jumpPower = 5;

	private int score;

	private Vector3 force;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        view = Camera.main.transform;
        Camera.main.GetComponent<RollerCamera>().SetTarget(transform);

        GetComponent<Health>().onDamage += OnDamage;
        GetComponent<Health>().onDeath += OnDeath;
        GetComponent<Health>().onHeal += OnHeal;
        GetComponent<Health>().onJumpPower += OnJumpPower;
        GetComponent<Health>().onWin += OnWin;
		RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
	}

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

		Ray ray = new Ray(transform.position, Vector3.down);
		bool onGround = Physics.Raycast(ray, groundRayLength, groundLayer);
		Debug.DrawRay(transform.position, ray.direction * groundRayLength);

		Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        force = viewSpace * (direction * maxForce);

        if (onGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jump.Play();
        }
    }

	private void FixedUpdate()
	{
        rb.AddForce(force);
	}

    public void AddPoints(int points)
    {
        score += points;
        RollerGameManager.Instance.SetScore(score);
        
    }

    public void OnDamage()
    {
        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

	public void OnHeal()
	{
		RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
	}

	public void OnDeath()
    {
        RollerGameManager.Instance.SetGameOver();
        Destroy(gameObject);
    }

	public void OnWin()
	{
		RollerGameManager.Instance.SetWin();
        Destroy(gameObject);
	}

    public void OnJumpPower()
    {
        jumpPower += 5;
    }
}

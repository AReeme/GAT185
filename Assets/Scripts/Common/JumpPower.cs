using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPower : Interactable
{
	[SerializeField] AudioSource pickup;

	void Start()
	{
		GetComponent<CollisionEvent>().onEnter += OnInteract;
		GetComponent<AudioSource>();
	}

	public override void OnInteract(GameObject go)
	{
		if (go.TryGetComponent<RollerPlayer>(out RollerPlayer player))
		{
			player.OnJumpPower();
		}

		if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract)
		{
			Destroy(gameObject);
		}

	}
}

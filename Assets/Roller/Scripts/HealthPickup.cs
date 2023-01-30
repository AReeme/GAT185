using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class HealthPickup : Interactable
{
	[SerializeField] private float heal;
	[SerializeField] AudioSource pickup;
	void Start()
    {
		GetComponent<CollisionEvent>().onEnter += OnInteract;
		GetComponent<AudioSource>();
    }

	public override void OnInteract(GameObject target)
	{
		if (target.TryGetComponent<Health>(out Health health))
		{
			health.OnApplyHealth(heal);
		}

		if (destroyOnInteract)
		{
			Destroy(gameObject);
			pickup.Play();
		}

	}
}

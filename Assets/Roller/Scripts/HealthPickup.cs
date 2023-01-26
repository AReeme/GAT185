using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class HealthPickup : Interactable
{
	[SerializeField] private float heal;

	void Start()
    {
		GetComponent<CollisionEvent>().onEnter += OnInteract;
    }

	public override void OnInteract(GameObject target)
	{
		if (target.TryGetComponent<Health>(out Health health))
		{
			health.OnApplyHealth(heal);
		}

		if (destroyOnInteract) Destroy(gameObject);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
	[SerializeField] public float health = 0;
    private bool isDead = false;

	public Action onDamage;
	public Action onHeal;
	public Action onDeath;
	public Action onJumpPower;
	public Action onWin;

	private void Awake()
	{
		health = maxHealth;
	}

	public void OnApplyDamage(float damage)
	{
		if (isDead) return;

		health -= damage;
		health = Mathf.Clamp(health, 0, maxHealth);
		onDamage?.Invoke();
		if (health <= 0)
		{
			isDead = true;
			onDeath?.Invoke();
		}
	}

	public void OnApplyHealth(float heal)
	{
		if (isDead) return;

		health += heal;
		health = Mathf.Clamp(health, 0, maxHealth);
		onHeal?.Invoke();
	}

}

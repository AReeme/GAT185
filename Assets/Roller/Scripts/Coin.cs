using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collidable
{
    [SerializeField] GameObject pickupFx;

    void Start()
    {
        onEnter += OnCoinPickup;
    }

    void Update()
    {
        
    }

    void OnCoinPickup(GameObject go)
    {
        if (go.TryGetComponent<RollerPlayer>(out RollerPlayer player))
        {
            player.AddPoints(100);
        }

        Instantiate(pickupFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}

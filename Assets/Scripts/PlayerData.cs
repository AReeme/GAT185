using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{

    [SerializeField] public float speed = 5;
    [SerializeField] public float hitForce = 2;
    [SerializeField] public float gravity = Physics.gravity.y;
    [SerializeField] public float turnRate = 10;
    [SerializeField] public float jumpHeight = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

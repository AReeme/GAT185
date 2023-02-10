using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    [SerializeField] float time = 0;
    private void Start()
    {
        Destroy(gameObject, time);
    }
}

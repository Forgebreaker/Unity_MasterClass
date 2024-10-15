using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonShieldEffect : MonoBehaviour
{
    public static SingletonShieldEffect Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }
}

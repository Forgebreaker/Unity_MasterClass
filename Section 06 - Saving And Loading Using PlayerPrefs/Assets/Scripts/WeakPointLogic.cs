using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointLogic : MonoBehaviour
{
    [SerializeField] private GameObject HitEffect;
    private GameObject EffectControl;

    private void Update()
    {
        if (EffectControl != null) 
        { 
            Destroy(EffectControl, 1);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player") 
        { 
            EnemyLogic enemy = GetComponentInParent<EnemyLogic>();
            if (enemy != null) {
                if (enemy.CurrentEnemyState != EnemyState.Sleep && enemy.CurrentEnemyState != EnemyState.StandUp)
                {
                    EffectControl = Instantiate(HitEffect, this.gameObject.transform.position, HitEffect.transform.rotation);
                    enemy.SetEnemyState(EnemyState.Die);
                }
            }
        }
    }
}

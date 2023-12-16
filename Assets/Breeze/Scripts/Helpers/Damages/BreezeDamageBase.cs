using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Breeze.Core;

namespace Breeze.Core
{
    public class BreezeDamageBase : MonoBehaviour, BreezeDamageable
    {
        [Header("SETTINGS")] 
        [Space] 
        [Space] public bool CanReceiveDamage = true;
        [Range(1f, 10f)] 
        [Space] public float DamageMultiplier = 1f;

        public BreezeSystem System { get; set; }
        public void TakeDamage(float Amount, GameObject Sender, bool IsPlayer, bool HitReaction = true)
        {
            Debug.Log("Hit Recieved by damage base.");
            GetComponent<BreezeSystem>().TakeDamage(Amount * DamageMultiplier, Sender, IsPlayer, HitReaction);
            if (System == null || !CanReceiveDamage)
                return;
            Debug.Log(gameObject.name + " took damage: " + Amount);

            System.TakeDamage(Amount * DamageMultiplier, Sender, IsPlayer, HitReaction);
        }
    }
}
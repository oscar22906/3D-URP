using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class PlayerAim : MonoBehaviour
{
    public Behaviour playerAim;
    public float aimDuration = 0.3f;
    public Rig aimLayer;
    RaycastWeapon weapon;

    void Start()
    {
        weapon = GetComponentInChildren<RaycastWeapon>();

    }


    void Awake()
    {

    }


    void FixedUpdate()
    {


    }

    private void LateUpdate()
    {
        if (aimLayer)
        {
            if (Input.GetButton("Fire2"))
            {
                aimLayer.weight += Time.deltaTime / aimDuration;
                playerAim.enabled = true;

            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDuration;
                playerAim.enabled = false;
            }
        }

        if (Input.GetButtonDown("Fire1") && Input.GetButton("Fire2"))
        {
            weapon.StartFiring();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
    }

}

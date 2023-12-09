using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{

    [HideInInspector] public Cinemachine.CinemachineFreeLook playerCamera;
    public float verticalRecoil;
    public float duration;

    float time;

    public void GenerateRecoil()
    {
        time = duration;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

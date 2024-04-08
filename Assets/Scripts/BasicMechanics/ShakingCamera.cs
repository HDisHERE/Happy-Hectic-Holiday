using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakingCamera : MonoBehaviour
{
    public static ShakingCamera Instance;
    [SerializeField] private float globalForce=1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalForce);
    }
}

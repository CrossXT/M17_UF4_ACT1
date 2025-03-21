using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    [SerializeField] UnityEvent<HitCollider, HurtCollider> onHitReceived;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotifyHit(HitCollider hitCollider)
    {
        //Debug.Log("Notificando golpe");
        //Debug.Log("Golpe de: " + hitCollider);
        //Debug.Log("A : " + transform.GetComponent<HurtCollider>());
        onHitReceived.Invoke(hitCollider, transform.GetComponent<HurtCollider>());
    }
}

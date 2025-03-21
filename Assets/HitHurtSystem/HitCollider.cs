using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour
{
    [SerializeField] UnityEvent<HitCollider, HurtCollider> onHitDelivered;
    [SerializeField] List<string> hittableTags;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < hittableTags.Count; i++)
        {
            if(other.tag == hittableTags[i])
            {
                HurtCollider hurtCollider;
                hurtCollider = other.gameObject.GetComponent<HurtCollider>();

                if(hurtCollider != null)
                {
                    hurtCollider.NotifyHit(transform.GetComponent<HitCollider>()); //Mirar si se ha de hacer asi
                    onHitDelivered.Invoke(transform.GetComponent<HitCollider>(), hurtCollider);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < hittableTags.Count; i++)
        {
            if(collision.collider.tag == hittableTags[i])
            {
                HurtCollider hurtCollider;
                hurtCollider = collision.collider.gameObject.GetComponent<HurtCollider>();

                if (hurtCollider != null)
                {
                    hurtCollider.NotifyHit(transform.GetComponent<HitCollider>()); //Mirar si se ha de hacer asi
                    onHitDelivered.Invoke(transform.GetComponent<HitCollider>(), hurtCollider);
                }
            }
        }
    }
}

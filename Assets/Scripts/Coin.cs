using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioSource CoinSound;
    public  static int count = 0;
    public  static int Maxcount = 0;


    void Awake()
    {
        CoinSound = GetComponent<AudioSource>();
        Maxcount++;
    }

    private void Start()
    {
        
    }

    void Update()
    {

        transform.DORotate(Vector3.up * 360f, 1f).SetRelative().SetEase(Ease.Linear);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            CoinSound.Play(1);
            count++;
            transform.DOLocalMoveY(transform.position.y + 2, 0.5f).SetRelative().SetEase(Ease.InOutBounce);
            transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InOutBounce).OnComplete(() => Destroy(gameObject));
            GetComponent<Collider>().enabled = false;
        }
    }
}

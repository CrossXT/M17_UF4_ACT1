using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject Test;

    public PlayableDirector timel_final;
    // Start is called before the first frame update
    void Start()
    {
        timel_final.stopped += timel_Final;


    }

    // Update is called once per frame
    void Update()
    {
        if(Coin.count == Coin.Maxcount)
        {
            Test.SetActive(true);
        }
    }

    public void timel_Final(PlayableDirector timel_final)
    {
        //DOTween.KillAll();

        Coin.count = 0;
        Coin.Maxcount = 0;

        SceneManager.LoadScene("Laberinto1");
    }
}

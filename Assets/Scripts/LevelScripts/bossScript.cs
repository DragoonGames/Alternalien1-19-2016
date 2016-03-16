using UnityEngine;
using System.Collections;

public class bossScript : MonoBehaviour {

    public GameObject[] horizontalSpikes;             //This will hold all of the spikes that will be called down in his rage
    public GameObject[] verticalSpikes;
    public float timer;                             //Set the time for how often the spikes will fall

    int spikeEven1;                                 //One of two even spikes (min: 2, max: 2)
    int spikeEven2;
    int spikeOdd1;                                  //One of two odd spikes (min: 2, max: 2)
    int spikeOdd2;

    // Use this for initialization
    void Start()
    {
        if (timer == 0)
        {
            timer = 15.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;                // Deprecate time;
        if (timer <= 0)
        {                                       // When the timer ends...
            DropSpikes();
            timer = 15.0f;                       // Reset timer;
        }
    }

    int ReturnRandom(bool isOdd,int min, int max)       //Assume that min is 0
    {
        //print("isOdd: " + isOdd + "Min: " + min + " Max: " + max);
        int tempResult = 0;
        if (isOdd)                                      //Force an oddNumber
        {
            tempResult = Random.Range(min, max);
        }
        else
        {
            tempResult = Random.Range(min, max);
        }
        return tempResult;
    }
    void DropSpikes()
    {
        spikeEven1 = ReturnRandom(false, 0, horizontalSpikes.Length);
        spikeEven2 = ReturnRandom(false, 0, horizontalSpikes.Length);
        if (spikeEven2 == (spikeEven1 = ReturnRandom(false, 0, horizontalSpikes.Length)))
        {
            //Redo it if they match the original
            //print("Redo the spike even 2");
            spikeEven2 = ReturnRandom(false, 0, horizontalSpikes.Length);
        }
        
        spikeOdd1 = ReturnRandom(true, 0, verticalSpikes.Length);
        spikeOdd2 = ReturnRandom(true, 0, verticalSpikes.Length);
        if (spikeOdd2 == (spikeOdd1 = ReturnRandom(true, 0, verticalSpikes.Length)))
        {
            //Redo it if they match the original
            //print("Redo the spike odd 2");
            spikeOdd2 = ReturnRandom(true, 0, verticalSpikes.Length);
        }

        horizontalSpikes[spikeEven1].GetComponent<WallSpikes>().Kill();
        verticalSpikes[spikeOdd1].GetComponent<WallSpikes>().Kill();

        horizontalSpikes[spikeEven2].GetComponent<WallSpikes>().Kill();
        verticalSpikes[spikeOdd2].GetComponent<WallSpikes>().Kill();

        //Test and show all the results to ensure integrity
        /*print("Spike Even 1: " + (spikeEven1 + 1) + " In Array pos: " + spikeEven1);
        print("Spike Even 2: " + (spikeEven2 + 1) + " In Array pos: " + spikeEven2);
        print("Spike Odd 1: " + (spikeOdd1 + 1) + " In Array pos: " + spikeOdd1);
        print("Spike Odd 2: " + (spikeOdd2 + 1) + " In Array pos: " + spikeOdd2);*/

    }
}

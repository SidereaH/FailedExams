using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public int kills;
    public int gold;
    [SerializeField] GameObject soundRampage;
    [SerializeField] int time, castTime;
    CoinManager coinManager;

    // Start is called before the first frame update

    private void Start()
    {
        if (PlayerPrefs.HasKey("kills"))
        {
            kills = PlayerPrefs.GetInt("kills");

        }
        if(PlayerPrefs.HasKey("gold")){
            gold = PlayerPrefs.GetInt("gold");
  
        }
        else{
            gold = 0;
        }
        coinManager = GameObject.FindGameObjectWithTag("CoinManager").transform.GetComponent<CoinManager>();
        coinManager.UpdateCoin(gold);
    }
    // Update is called once per frame
    /*private void Update()
    {
        if (kills == 5)
        {
            if (time == 0)
            {
                time = castTime;
                GameObject _temp = Instantiate(soundRampage, transform.position, Quaternion.identity);
                //_temp.GetComponent<AudioSource>().Play();
                Destroy(_temp, 4);
            }
        }
}*/

    public int addKill()
    {

        return kills++;

    }
    public int addGold(int goldCost)
    {
        
        gold+=goldCost;
        coinManager.UpdateCoin(gold);
        return gold;
    }
    public void validateKill()
    {
        //kills--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject Player;
    private PlayerLogic PlayerLogicAccess;
    [SerializeField] private GameObject SaveEffect;

    [SerializeField] private GameObject[] Coins;
    [SerializeField] private CoinLogic[] CoinLogicsAccessObjects;

    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private EnemyLogic[] EnemyLogicsAccessObjects;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        { 
            Destroy(this);
        }
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null) 
        {
            PlayerLogicAccess = Player.GetComponent<PlayerLogic>();
        }

        Coins = GameObject.FindGameObjectsWithTag("Coin");
        CoinLogicsAccessObjects = new CoinLogic[Coins.Length];
        
        if (Coins != null)
        {
            for (int counter = 0; counter < Coins.Length; counter++)
            {
                CoinLogicsAccessObjects[counter] = Coins[counter].GetComponent<CoinLogic>();
            }
        }

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyLogicsAccessObjects = new EnemyLogic[Enemies.Length];

        if (Enemies != null)
        {
            for (int counter = 0; counter < Enemies.Length; counter++)
            {
                EnemyLogicsAccessObjects[counter] = Enemies[counter].GetComponent<EnemyLogic>();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Instantiate(SaveEffect, Player.transform.position,SaveEffect.transform.rotation);
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        { 
            Load();
        }
    }
    public void Save() 
    {
        PlayerLogicAccess.Save();
        PlayerPrefs.Save(); // Save all modified references

        for (int counter = 0; counter < Coins.Length; counter++)
        {
            CoinLogicsAccessObjects[counter].Save(counter);
        }

        for (int counter = 0; counter < Enemies.Length; counter++)
        {
            EnemyLogicsAccessObjects[counter].Save(counter);
        }
    }

    public void Load() 
    { 
        PlayerLogicAccess.Load();

        for (int counter = 0; counter < Coins.Length; counter++)
        {
            CoinLogicsAccessObjects[counter].Load(counter);
        }

        for (int counter = 0; counter < Enemies.Length; counter++)
        {
            EnemyLogicsAccessObjects[counter].Load(counter);
        }
    }
}

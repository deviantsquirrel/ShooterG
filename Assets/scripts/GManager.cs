using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class GManager : MonoBehaviour
{
    [SerializeField] GameObject edsScreen;

    [SerializeField] Transform[] _spawnRegions; 
    [SerializeField] GameObject _enemyPrefab;

    [SerializeField] GameObject _SaveButton;

    private int _enemiesKilled;

    private static GManager instance;
    [SerializeField] GameObject player;

    float _health = 100f;
    private Dictionary<string, int> _myDictionary = new Dictionary<string, int>();
    Dictionary<int, string> _myDictionary_Positions = new Dictionary<int, string>
        {
            { 1, "" },
            { 2, "" },
            { 3, "" }
        };


    public static GManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GManager");
                    instance = singletonObject.AddComponent<GManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        _SaveButton.SetActive(false);
        _enemiesKilled = 0;
        for (int i =0;i < 3; i++)
        {
            NewEnemy();
        }
    }
    public void EnemyKilled()
    {
        _enemiesKilled++;
        if(_enemiesKilled==3)
        {
            _SaveButton.SetActive(true);
        }
    }

    private void NewEnemy()
    {
        Transform spawnRegion = _spawnRegions[Random.Range(0, _spawnRegions.Length)];
        Vector2 randomPoint = RandomPointInBounds(spawnRegion.GetComponent<Collider2D>());
        Instantiate(_enemyPrefab, randomPoint, Quaternion.identity);
    }
    // Generate a random point within a 2D collider bounds
    Vector2 RandomPointInBounds(Collider2D bounds)
    {
        Vector2 center = bounds.bounds.center;
        Vector2 size = bounds.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float randomY = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(randomX, randomY);
    }

    public void PlayerDied()
    {
        Instantiate(edsScreen, edsScreen.transform.position, Quaternion.identity);
    }

    public void SaveData()
    {
        _health = player.GetComponent<playerMove>().ReturnHealth();
        _myDictionary = backpack.Instance.ReturnFirstDic();
        _myDictionary_Positions = backpack.Instance.ReturnSecondDic();
        SaveSystem.SaveStats(_myDictionary, _myDictionary_Positions, _health);

    }
    public void LoadData()
    {
        Playerdata data = SaveSystem.LoadStats();
        if (data != null)
        {
            _health = data._health;
            _myDictionary = data._myDictionary;
            _myDictionary_Positions = data._myDictionary_Positions;
        } 
    }

    public float ReturnHealth()
    {
        return _health;
    }
    public Dictionary<string, int> ReturnFirstDic()
    {
        return _myDictionary;
    }

    public Dictionary<int, string> ReturnSecondDic()
    {
        return _myDictionary_Positions;
    }
}

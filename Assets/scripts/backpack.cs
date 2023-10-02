using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using TMPro;

public class backpack : MonoBehaviour
{
    private static backpack instance;

    private Dictionary<string, int> myDictionary = new Dictionary<string, int>();
    Dictionary<int, string> myDictionary_Positions = new Dictionary<int, string>
        {
            { 1, "" },
            { 2, "" },
            { 3, "" }
        };


    [SerializeField] private Sprite _ak;
    [SerializeField] private Sprite _makar;
    [SerializeField] private Sprite _ammo;

    [SerializeField] private Image _firstButton;
    [SerializeField] private Image _secondButton;
    [SerializeField] private Image _thirdButton;

    [SerializeField] private GameObject _Delete;

    [SerializeField] private GameObject _IconPanel;

    private int _toDelete;

    private bool _panelOpened=false;

    [SerializeField] TextMeshProUGUI _testFirst;
    [SerializeField] TextMeshProUGUI _testSecond;
    [SerializeField] TextMeshProUGUI _testThird;

    // Public property to access the Singleton instance
    public static backpack Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<backpack>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("backpack");
                    instance = singletonObject.AddComponent<backpack>();
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

    private void Start()
    {
        GManager.Instance.LoadData();
        myDictionary = GManager.Instance.ReturnFirstDic();
        myDictionary_Positions = GManager.Instance.ReturnSecondDic();
        SetSavedData();
        _Delete.SetActive(false);
        _IconPanel.SetActive(false);
        GetSomeAmmo();

    }

    private void SetSavedData()
    {
        Image changingMee;
        for (int t = 1; t < 4; t++)
        {
            switch (t)
            {
                case 1:
                    changingMee = _firstButton;
                    break;
                case 2:
                    changingMee = _secondButton;
                    break;
                default:
                    changingMee = _thirdButton;
                    break;
            }
                
            if (myDictionary_Positions[t] != "")
            {
                Color nnewColor = changingMee.color;
                nnewColor.a = 1f;
                changingMee.color = nnewColor;
                switch (myDictionary_Positions[t])
                {
                    case "Ak":
                        changingMee.sprite = _ak;
                        break;
                    case "Makar":
                        changingMee.sprite = _makar;
                        break;
                    case "Ammo":
                        changingMee.sprite = _ammo;
                        break;
                    default:
                        break;
                }
                UpdateCount(myDictionary_Positions[t]);
            }
        }
    }

    private void GetSomeAmmo()
    {
        if (myDictionary.ContainsKey("Ammo"))
        {
            int items = myDictionary["Ammo"];
            items+=30;
            myDictionary["Ammo"] = items;
        }
        else
        {
            myDictionary.Add("Ammo", 35);
            AssignNewImage("Ammo");
        }
        UpdateCount("Ammo");
    }
    public void GainItem(string name)
    {
        if (myDictionary.ContainsKey(name))
        {
            int items = myDictionary[name];
            items++;
            myDictionary[name] = items;
            UpdateCount(name);
        }
        else
        {
            myDictionary.Add(name, 1);
            AssignNewImage(name);
        }
    }
    private void UpdateCount(string name)
    {
        if (myDictionary[name] < 2)
        {
            return;
        }
        if (myDictionary_Positions[1] == name)
        {
            _testFirst.text = myDictionary[name].ToString();
        }
        else if(myDictionary_Positions[2] == name)
        {
            _testSecond.text = myDictionary[name].ToString();
        }
        else
        {
            _testThird.text = myDictionary[name].ToString();
        }
    }
    private void AssignNewImage(string name)
    {
        Image changingMe;
        if (myDictionary_Positions[1] == "")
        {
            changingMe = _firstButton;
            myDictionary_Positions[1] = name;
            
            
        }else if (myDictionary_Positions[2] == "")
        {
            changingMe = _secondButton;
            myDictionary_Positions[2] = name;
        }
        else
        {
            changingMe = _thirdButton;
            myDictionary_Positions[3] = name;
        }
        Color newColor = changingMe.color;
        newColor.a = 1f;
        changingMe.color = newColor;
        switch (name)
        {
            case "Ak":
                changingMe.sprite = _ak;
                break;
            case "Makar":
                changingMe.sprite = _makar;
                break;
            case "Ammo":
                changingMe.sprite = _ammo;
                break;
            default:
                break;
        }
    }
    public void ShowPanel()
    {
        if (!_panelOpened)
        {
            _IconPanel.SetActive(true);
            _panelOpened = true;
        }
        else{
            _IconPanel.SetActive(false);
            _panelOpened = false;
            _Delete.SetActive(false);
        }
    }
    public void ShowDelete(int num)
    {
        if(myDictionary_Positions[num] != "")
        {
            _Delete.SetActive(true);
            _toDelete = num;
            Debug.Log(num);
        }
    }

    public void CommitDelete()
    {
        if (myDictionary_Positions[_toDelete] != "")
        {
            DeleteItem(_toDelete);
        }
    }
    private void DeleteItem(int num)
    {
        Image changingMe;
        if (num == 1)
        {
            _testFirst.text = "";
            changingMe = _firstButton;
        }else if (num == 2)
        {
            _testSecond.text = "";
            changingMe = _secondButton;
        }
        else
        {
            _testThird.text = "";
            changingMe = _thirdButton;
        }
        Color newColor = changingMe.color;
        newColor.a = 0f;
        changingMe.color = newColor;
        changingMe.sprite = null;

        if (myDictionary_Positions[num] == "Ak")
        {
            myDictionary.Remove("Ak");
        }else if (myDictionary_Positions[num] == "Makar")
        {
            myDictionary.Remove("Makar");
        }
        else
        {
            myDictionary.Remove("Ammo");
        }
        myDictionary_Positions[num] = "";
    }

    public int CheakAmmunition()
    {
        if (myDictionary.ContainsKey("Ammo"))
        {
            return myDictionary["Ammo"];
        } else
        {
            return 0;
        }
    }

    public void LoseDBullet()
    {
        int items = myDictionary["Ammo"];
        items--;
        myDictionary["Ammo"] = items;
        if(items == 0)
        {
            Image changingMe;
            if (myDictionary_Positions[1] == "Ammo")
            {
                _testFirst.text = "";
                changingMe = _firstButton;
                myDictionary_Positions[1] = "";
            }
            else if (myDictionary_Positions[2] == "Ammo")
            {
                _testSecond.text = "";
                changingMe = _secondButton;
                myDictionary_Positions[2] = "";
            }
            else
            {
                _testThird.text = "";
                changingMe = _thirdButton;
                myDictionary_Positions[3]="";
            }
            Color newColor = changingMe.color;
            newColor.a = 0f;
            changingMe.color = newColor;
            changingMe.sprite = null;
            myDictionary.Remove("Ammo");
        }
        else if(items == 1)
        {
            if (myDictionary_Positions[1] == "Ammo")
            {
                _testFirst.text = "";
            }
            else if (myDictionary_Positions[2] == "Ammo")
            {
                _testSecond.text = "";
            }
            else
            {
                _testThird.text = "";
            }
        }
        else
        {
            UpdateCount("Ammo");
        }
        
        
    }

    public Dictionary<string, int> ReturnFirstDic()
    {
        return myDictionary;
    }

    public Dictionary<int, string> ReturnSecondDic()
    {
        return myDictionary_Positions;
    }

}


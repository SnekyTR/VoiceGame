using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpPannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [HideInInspector] public string preSelectPlayer ;
    [HideInInspector] public string SelectPlayer;
    [HideInInspector] public string attackEnemies;
    // Start is called before the first frame update
    void Start()
    {
        preSelectPlayer = "Seleccionar: " + "\n";
        text = GameObject.Find("PlayerActions").GetComponent<TextMeshProUGUI>();
        int a = GameObject.Find("Players").transform.childCount;
        for(int i = 0;i< a; i++)
        {
            preSelectPlayer += GameObject.Find("Players").transform.GetChild(i).name + "\n";
        }
        SelectPlayer = "Mover" + "\n" + "Atacar";
        int b = GameObject.Find("Enemys").transform.childCount;
        for (int i = 0; i < b; i++)
        {
            attackEnemies += GameObject.Find("Enemys").transform.GetChild(i).name + "\n";
        }
        UpdateTextHelp(preSelectPlayer);
    }
    public void UpdateTextHelp(string helpText)
    {
        text.text = helpText;
    }
}

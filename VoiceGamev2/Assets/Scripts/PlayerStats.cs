using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int lifePoints;
    [SerializeField] public int strengthPoints;
    [SerializeField] public int intellectPoints;
    [SerializeField] public int agilityPoints;
    public int criticProb;
    public string actualWeapon;

    [Header("Weapons")]
    public GameObject weapon01;
    public GameObject weapon02;
    public GameObject weapon03;
    public GameObject weapon04;
    public GameObject weapon05;
    public GameObject weapon06;

    private float energy = 5f;
    private float energyActions = 5f;
    private EnergyScript energyLo;
    private Animator animator;

    private int lifeValue;
    private int strengthValue;
    private int agilityValue;
    private int intellectValue;
    private int shieldValue;

    private float maxLife;
    private float maxShield;
    [HideInInspector] public float maxEnergy;

    [HideInInspector] public GameObject structure;
    [HideInInspector] public GameObject selected;

    private SkillsColocation skillsColocation;

    private CameraFollow gameM;
    private Skills skill;
    private WinLoose winLoose;
    void Start()
    {
        //******* Aqui carga ya sabes todos los datos de lifepoint, strenghtpoint, agilitypoints, intellectpoints
        //******* cada player tiene su nombre normal escepto hammun el cual se llama jamun
        //******* ademas de ello tienes q determinar el arma de cada uno dependiendo de su nombre, los nombres de las armas como deberan ser reconocidos son
        //******* actualWeapon = ... - los nombres de las armas estan en el script "Skills", aunq son los nombres de las armas pero en ingles y todo minusculas
        //******* solo eso prueba bien las cosas, y recuerda lo de resucitar, en script "Skills" al final esta el resusitar, suerte con eso
        //******* faltaron pocas cosas como las pasivas de las habilidades y el funcionamiento del arco en si, asiq si metes en la build q aun no encuentre el arco mejor XD
        //******* agregue el comando "desbloquear" para si quieren usar todas las habilidades de su arma XD
        //LoadStatsPlayer();

        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        skillsColocation = GameObject.Find("CanvasManager").GetComponent<SkillsColocation>();
        skill = gameM.GetComponent<Skills>();
        winLoose = gameM.GetComponent<WinLoose>();
        winLoose.totalPlayers++;
        InitialWeapon();
        //calculo de valor de los stats

        //skills

        if(transform.name == "Magnus")
        {
            energyLo = gameM.playerSelected[0].transform.GetChild(1).GetComponent<EnergyScript>();
            structure = gameM.playerStructure[0];
            selected = gameM.playerSelected[0].transform.GetChild(0).GetChild(1).gameObject;
            skillsColocation.AssignMagnusSkills(actualWeapon, this.gameObject);
        }
        else if(transform.name == "Vagnar")
        {
            energyLo = gameM.playerSelected[1].transform.GetChild(1).GetComponent<EnergyScript>();
            structure = gameM.playerStructure[1];
            selected = gameM.playerSelected[1].transform.GetChild(0).GetChild(1).gameObject;
            skillsColocation.AssignVagnarSkills(actualWeapon, this.gameObject);
        }
        else
        {
            energyLo = gameM.playerSelected[2].transform.GetChild(1).GetComponent<EnergyScript>();
            structure = gameM.playerStructure[2];
            selected = gameM.playerSelected[2].transform.GetChild(0).GetChild(1).gameObject;
            skillsColocation.AssignHammundSkills(actualWeapon, this.gameObject);
        }

        lifeValue = 10;
        for(int i = 2; i <= lifePoints; i++)
        {
            lifeValue += (int)i / 2;
        }

        strengthValue = 3;
        for(int i = 2; i <= strengthPoints; i++)
        {
            strengthValue += (int)i / 3;
        }

        agilityValue = 3;
        for(int i = 2;i <= agilityPoints; i++)
        {
            agilityValue += (int)i / 3;
        }

        intellectValue = 5;
        for(int i = 2; i <= intellectPoints; i++)
        {
            intellectValue += (int)i / 3;
        }

        animator = GetComponent<Animator>();
        maxLife = lifeValue;
        maxShield = shieldValue;
        maxEnergy = energy;

        selected.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = (lifeValue + " / " + maxLife);


    }
    //Coge los stats guardados en el fichero antes de abrir el nivel
    private void LoadStatsPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(transform);

        lifePoints = data.healthStat;
        strengthPoints = data.strength;
        agilityPoints = data.agility;
        intellectPoints = data.intellectStat;
    }
    void Update()
    {

    }

    private void InitialWeapon()
    {
        if (actualWeapon == "sword")
        {
            weapon01.SetActive(true);
        }
        else if (actualWeapon == "axe")
        {
            weapon02.SetActive(true);
        }
        else if (actualWeapon == "spear")
        {
            weapon03.SetActive(true);
        }
        else if (actualWeapon == "bow")
        {
            weapon04.SetActive(true);
        }
        else if (actualWeapon == "fire staff")
        {
            weapon05.SetActive(true);
        }
        else if (actualWeapon == "sacred staff")
        {
            weapon06.SetActive(true);
        }
    }

    public void SetLife(int n)
    {
        if(shieldValue > 0 && n < 0)
        {
            shieldValue += n;

            if(shieldValue <= 0)
            {
                structure.transform.GetChild(2).GetComponent<Scrollbar>().size = 0;
                selected.transform.GetChild(1).GetComponent<Scrollbar>().size = 0;
                selected.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "";

                structure.transform.GetChild(2).gameObject.SetActive(false);
                selected.transform.GetChild(1).gameObject.SetActive(false);

                animator.SetInteger("A_Recieve", 1);

                n = shieldValue;
            }
            else
            {
                structure.transform.GetChild(2).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
                selected.transform.GetChild(1).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
                selected.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = (shieldValue + " / " + maxShield);

                animator.SetInteger("A_Recieve", 1);

                return;
            }
        }


        lifeValue += n;
        
        if (lifeValue <= 0)                    //death
        {
            winLoose.totalPlayers--;
            animator.SetInteger("A_Death", 1);
            gameM.EliminateElement(this.gameObject);
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else if (n < 0)                         //dmg recieve
        {
            animator.SetInteger("A_Recieve", 1);
        }
        else if(n > 0)
        {
            if (lifeValue > maxLife) lifeValue = (int)maxLife;
        }

        structure.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = (lifeValue + " / " + maxLife);

        print((lifeValue / maxLife));
        print(lifeValue + "vida");
        print(maxLife + "maxvida");
    }

    public void SetStrenght(float n)
    {
        strengthValue = (int)(strengthValue * n);
    }

    public void SetAgility(float n)
    {
        agilityValue = (int)(agilityValue * n);
    }

    public void SetIntellect(float n)
    {
        print(intellectValue);
        intellectValue = (int)(intellectValue * n);
        print(intellectValue);
    }

    public void MoreCriticProb(int n)
    {
        criticProb += n;
    }

    public void SetEnergy(float n)
    {
        energy += n;
        energyLo.NewEnergyIcon(energy);
    }

    public void SetEnergyActions(float n)
    {
        energyActions += n;
        energyLo.NewEnergyActionsIcon(energyActions);
    }

    public void FullEnergy()
    {
        energy = maxEnergy;
        energyActions = maxEnergy;
        energyLo.NewEnergyIcon(energy);
        energyLo.NewEnergyActionsIcon(energyActions);
    }

    public int GetLife()
    {
        return lifeValue;
    }

    public int GetStrenght()
    {
        return strengthValue;
    }

    public int GetIntellect()
    {
        return intellectValue;
    }

    public int GetAgility()
    {
        return agilityValue;
    }

    public float GetEnergy(int i)
    {
        if (i == 1) return energy;
        else return energyActions;
    }

    public void NewShield(int s)
    {
        structure.transform.GetChild(2).gameObject.SetActive(true);
        selected.transform.GetChild(1).gameObject.SetActive(true);

        shieldValue = s;
        maxShield = s;

        structure.transform.GetChild(2).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
        selected.transform.GetChild(1).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
        selected.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = (shieldValue + " / " + maxShield);
    }
}

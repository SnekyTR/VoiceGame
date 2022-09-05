using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int lifePoints;
    [SerializeField] public int strengthPoints;
    [SerializeField] public int intellectPoints;
    [SerializeField] public int agilityPoints;
    [SerializeField] public int shieldPoints;
    [SerializeField] public int criticPoints;
    [HideInInspector] public int criticProb;
    public string actualWeapon;
    [HideInInspector]
    public AudioSource audioSource;
    public List<Transform> cinemaCam;
    public List<AudioClip> characterSounds;

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
    private bool isStunned;

    [HideInInspector] public float maxLife;
    private float maxShield;
    [HideInInspector] public float maxEnergy;

    [HideInInspector] public GameObject structure;
    [HideInInspector] public GameObject selected;

    public GameObject stunFX;
    private GameObject stunPrefab;

    private SkillsColocation skillsColocation;

    private CameraFollow gameM;
    private Skills skill;
    private WinLoose winLoose;

    public GameObject poisonFx;
    private GameObject poisonPrefab;
    private int poisonTurns = 0;

    public AudioClip hurtS;

    void Start()
    {
        //******* Aqui carga ya sabes todos los datos de lifepoint, strenghtpoint, agilitypoints, intellectpoints
        //******* cada magnus tiene su nombre normal escepto hammun el cual se llama jamun
        //******* ademas de ello tienes q determinar el arma de cada uno dependiendo de su nombre, los nombres de las armas como deberan ser reconocidos son
        //******* actualWeapon = ... - los nombres de las armas estan en el script "Skills", aunq son los nombres de las armas pero en ingles y todo minusculas
        //******* solo eso prueba bien las cosas, y recuerda lo de resucitar, en script "Skills" al final esta el resusitar, suerte con eso
        //******* faltaron pocas cosas como las pasivas de las habilidades y el funcionamiento del arco en si, asiq si metes en la build q aun no encuentre el arco mejor XD
        //******* agregue el comando "desbloquear" para si quieren usar todas las habilidades de su arma XD
        
        if(System.IO.File.Exists(Application.persistentDataPath +"/"+ transform.name+".data")){
            LoadStatsPlayer();
        }
        
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        skillsColocation = GameObject.Find("CanvasManager").GetComponent<SkillsColocation>();
        skill = gameM.GetComponent<Skills>();
        winLoose = gameM.GetComponent<WinLoose>();
        audioSource = gameObject.GetComponent<AudioSource>();
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

        lifeValue = 25;
        for(int i = 2; i <= lifePoints; i++)
        {
            lifeValue += (int)(i * 1.5f);
        }

        strengthValue = 5;
        for(int i = 2; i <= strengthPoints; i++)
        {
            strengthValue += (int)i / 3;
        }

        agilityValue = 4;
        for(int i = 2;i <= agilityPoints; i++)
        {
            agilityValue += (int)i / 3;
        }

        intellectValue = 5;
        for(int i = 2; i <= intellectPoints; i++)
        {
            intellectValue += (int)i / 3;
        }

        shieldValue = 15;
        for (int i = 2; i <= shieldPoints; i++)
        {
            shieldValue += (int)(i * 0.5f);
        }

        criticProb = criticPoints * 10;

        animator = GetComponent<Animator>();
        maxLife = lifeValue;
        maxShield = shieldValue;
        maxEnergy = energy;

        NewShield(shieldValue);

        selected.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = (lifeValue + " / " + maxLife);

        float atkE = 0;

        if (actualWeapon == "sword") atkE = 2;
        else if (actualWeapon == "axe") atkE = 2.5f;
        else if (actualWeapon == "spear") atkE = 1.5f;
        else if (actualWeapon == "bow") atkE = 2f;
        else if (actualWeapon == "fire staff") atkE = 1.5f;
        else if (actualWeapon == "sacred staff") atkE = 1;

        selected.transform.parent.parent.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = atkE.ToString();

        print(selected.transform.parent.parent.GetChild(2).GetChild(0).GetChild(0).GetChild(0));
    }
    //Coge los stats guardados en el fichero antes de abrir el nivel
    private void LoadStatsPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer(this.transform);

        lifePoints = data.healthStat;
        strengthPoints = data.strength;
        agilityPoints = data.agility;
        intellectPoints = data.intellectStat;
        actualWeapon = data.weaponType;
        shieldPoints = data.armor;
        criticPoints = data.critStrikePoints;
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
                selected.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "";

                structure.transform.GetChild(2).gameObject.SetActive(false);
                selected.transform.GetChild(1).gameObject.SetActive(false);

                animator.SetInteger("A_Recieve", 1);

                n = shieldValue;
            }
            else
            {
                selected.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = (shieldValue).ToString();
                structure.transform.GetChild(2).GetComponent<Image>().fillAmount = (shieldValue / maxShield);

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
            audioSource.clip = characterSounds[0];
            audioSource.Play();

            if(poisonPrefab != null) Destroy(poisonPrefab);

            selected.transform.GetChild(3).gameObject.SetActive(false);
            structure.transform.GetChild(5).gameObject.SetActive(false);

            if (stunPrefab != null) Destroy(stunPrefab);

            structure.transform.GetChild(3).gameObject.SetActive(false);

            isStunned = false;
            lifeValue = 0;
        }
        else if (n < 0)                         //dmg recieve
        {
            animator.SetInteger("A_Recieve", 1);

            audioSource.clip = hurtS;
            audioSource.Play();
        }
        else if(n > 0)
        {
            if (lifeValue > maxLife) lifeValue = (int)maxLife;
        }

        structure.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = (lifeValue + " / " + maxLife);
    }

    private void SetLifePoison(int n)
    {
        lifeValue += n;

        if (lifeValue <= 0)                    //death
        {
            winLoose.totalPlayers--;
            animator.SetInteger("A_Death", 1);
            gameM.EliminateElement(this.gameObject);
            GetComponent<NavMeshAgent>().enabled = false;
            audioSource.clip = characterSounds[0];
            audioSource.Play();



        }
        else if (n < 0)                         //dmg recieve
        {
            animator.SetInteger("A_Recieve", 1);
        }
        else if (n > 0)
        {
            if (lifeValue > maxLife) lifeValue = (int)maxLife;
        }

        structure.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = (lifeValue + " / " + maxLife);
    }

    public void StunPlayer(bool t)
    {
        if (t)
        {
            Vector3 pos = transform.position;
            pos.y += 2;

            structure.transform.GetChild(3).gameObject.SetActive(true);

            stunPrefab = Instantiate(stunFX, pos, transform.rotation);
            isStunned = true;
        }
        else
        {
            Destroy(stunPrefab);

            structure.transform.GetChild(3).gameObject.SetActive(false);

            isStunned = false;
        }
    }

    public bool IsStunned()
    {
        return isStunned;
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

        selected.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = (shieldValue).ToString();
    }

    public void PoisonStart()
    {
        if(poisonTurns == 0)
        {
            Vector3 newPos = transform.position;
            newPos.y += 1;
            poisonPrefab = Instantiate(poisonFx, newPos, transform.rotation);

            poisonPrefab.transform.SetParent(transform);

            selected.transform.GetChild(3).gameObject.SetActive(true);
            structure.transform.GetChild(5).gameObject.SetActive(true);

            poisonTurns = 3;
        }
    }

    public void PoisonTimer()
    {
        if (poisonTurns == 0) return;

        poisonTurns -= 1;

        int newLife = (int)(maxLife * 0.1f);

        SetLifePoison(-newLife);

        if(poisonTurns == 0)
        {
            Destroy(poisonPrefab);

            selected.transform.GetChild(3).gameObject.SetActive(false);
            structure.transform.GetChild(5).gameObject.SetActive(false);
        }
    }
}

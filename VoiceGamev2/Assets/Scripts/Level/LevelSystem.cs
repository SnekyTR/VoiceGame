using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private PartyInformation partyInfo;
    [SerializeField]private IncreaseStats increaseStats;
    public int level;

    public float currentXp;
    public float requiredXp;
    private float lerpTimer;
    private float delayTimer;

    [SerializeField]private GameObject buttonsStats;

    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;

    [Header("Multipliers")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;


    // Start is called before the first frame update
    void Start()
    {
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
        requiredXp = CalculateRequireXp();
        //buttonsStats = GameObject.Find("buttons");
        //increaseStats = GameObject.Find("buttons_stats").GetComponent<IncreaseStats>();

    }

    // Update is called once per frame
    public void LoadLevel()
    {
        PlayerData data = SaveSystem.LoadPlayer(transform);

        level = data.level;
        currentXp = data.experience;
    }
    void Update()
    {
        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.A)) GainExperience(20);
        if (currentXp > requiredXp) LevelUP();
    }
    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float fXP = frontXpBar.fillAmount;
        if(fXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if(delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(fXP, backXpBar.fillAmount, percentComplete);
            }
        }
    }
    public void GainExperience(float xpGained)
    {
        currentXp += xpGained;
        lerpTimer = 0f;
    }
    public void LevelUP()
    {
        level++;
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        requiredXp = CalculateRequireXp();
        ActivateButtons();
        UpdateLevel();
    }
    private int CalculateRequireXp()
    {
        int solverForRequiredXp = 0;
        for(int levelCycle =1; levelCycle <=level; levelCycle++)
        {
            solverForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solverForRequiredXp / 4;
    }
    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        if(passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentXp += xpGained * multiplier;
        }
        else
        {
            currentXp += xpGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void ActivateButtons()
    {
        increaseStats.GetPlayer(this.gameObject);
        buttonsStats.SetActive(true);
        
    }
    public GameObject PlayerLvlUp()
    {
        return this.gameObject;
    }
    public void DeactivateButtons()
    {
        buttonsStats.SetActive(false);
        increaseStats.statOrders.Stop();
    }
    public void UpdateLevel()
    {
        string actualLevel;
        string actualPlayer = transform.name.ToString();
        actualLevel = level.ToString();
        partyInfo.UpdateLevel(actualLevel, actualPlayer);
    }
    public string UpdateXP()
    {
        string actualxp;
        actualxp = currentXp.ToString();
        return actualxp;
    }
}

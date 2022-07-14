using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBook : MonoBehaviour
{
    public GameObject SBGameObject;
    public GameObject magnusSB, vagnarSB, torekSB;
    public GameObject magnusA, vagnarA, torekA;
    public List<Sprite> skillImage = new List<Sprite>();

    private Skills skills;
    [HideInInspector]public List<PlayerStats> playerS = new List<PlayerStats>();

    public bool isStarted;
    private int psjNum = 1;

    private void Start()
    {
        skills = GameObject.Find("GameManager").GetComponent<Skills>();
    }

    public void StartSkillBook(int p, int a)
    {
        if (isStarted)
        {
            SBGameObject.SetActive(false);
            isStarted = false;
        }
        else if (!isStarted)
        {
            SBGameObject.SetActive(true);
            ChangeSkillBookPage(p);
            isStarted = true;
        }

        psjNum = a;
    }

    public void ChangeSkillBookPage(int p)
    {
        if (p == 1) MagnusBook();
        else if (p == 2) VagnarBook();
        else if (p == 3) TorekBook();
    }

    private void MagnusBook()
    {
        magnusSB.SetActive(true);
        vagnarSB.SetActive(false);
        torekSB.SetActive(false);
        magnusA.SetActive(false);
        if(psjNum == 2) vagnarA.SetActive(true);
        if(psjNum == 3) torekA.SetActive(true);

        SetMagnusSkillBook();
    }

    private void VagnarBook()
    {
        magnusSB.SetActive(false);
        vagnarSB.SetActive(true);
        torekSB.SetActive(false);
        magnusA.SetActive(true);
        if(psjNum == 2)vagnarA.SetActive(false);
        if(psjNum == 3)torekA.SetActive(true);
    }

    private void TorekBook()
    {
        magnusSB.SetActive(false);
        vagnarSB.SetActive(false);
        torekSB.SetActive(true);
        magnusA.SetActive(true);
        if(psjNum == 2)vagnarA.SetActive(true);
        if(psjNum == 3)torekA.SetActive(false);
    }

    private void SetMagnusSkillBook()
    {
        string wp = playerS[0].actualWeapon;
        int plyN = 0;

        magnusSB.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = wp.ToUpper();
        magnusSB.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Daño: " + GetBasicAtk(wp, plyN, true) + " - " +
            GetBasicAtk(wp, plyN, false) + "\n" + "Energía: " + GetEnergy(wp) + "\n" + "Rango: " +
            GetRange(wp) + "\n" + "Crítico: " + GetCritical(wp);

        if(ValidationSkill(wp, 0) >= 1)
        {
            magnusSB.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 1);
            magnusSB.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = "Daño: " + GetBasicAtk(wp, plyN, true) + " - " +
            GetBasicAtk(wp, plyN, false) + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 1)) + "\n" + "Rango: " +
            skills.GetRanges(GetSkillName(wp, 1));
        }
        else magnusSB.transform.GetChild(2).gameObject.SetActive(false);

        if (ValidationSkill(wp, 0) >= 2)
        {

        }
        else magnusSB.transform.GetChild(3).gameObject.SetActive(false);

        if (ValidationSkill(wp, 0) >= 3)
        {

        }
        else magnusSB.transform.GetChild(4).gameObject.SetActive(false);
    }

    private void SetVagnarSkillBook()
    {

    }

    private void SetTorekSkillBook()
    {

    }

    private int ValidationSkill(string w, int i)
    {
        if (w == "sword" || w == "axe")
        {
            if (playerS[i].strengthPoints >= 6) return 1;
            else if (playerS[i].strengthPoints >= 8) return 2;
            else if (playerS[i].strengthPoints >= 10) return 3;
        }
        else if (w == "spear" || w == "bow")
        {
            if (playerS[i].agilityPoints >= 6) return 1;
            else if (playerS[i].agilityPoints >= 8) return 2;
            else if (playerS[i].agilityPoints >= 10) return 3;
        }
        else if (w == "fire staff" || w == "sacred staff")
        {
            if (playerS[i].intellectPoints >= 6) return 1;
            else if (playerS[i].intellectPoints >= 8) return 2;
            else if (playerS[i].intellectPoints >= 10) return 3;
        }

        return 0;
    }

    private int GetBasicAtk(string w, int i, bool d)
    {
        if(w == "sword")
        {
            if (d) return (int)(playerS[i].GetStrenght() * 0.9f);
            else if (!d) return (int)(playerS[i].GetStrenght() * 1.3f);
        }
        else if(w == "axe")
        {
            if (d) return (int)(playerS[i].GetStrenght() * 1.2f);
            else if (!d) return (int)(playerS[i].GetStrenght() * 1.6f);
        }
        else if(w == "spear")
        {
            if (d) return (int)(playerS[i].GetAgility() * 1f);
            else if (!d) return (int)(playerS[i].GetAgility() * 1.2f);
        }
        else if(w == "bow")
        {
            if (d) return (int)(playerS[i].GetAgility() * 0.8f);
            else if (!d) return (int)(playerS[i].GetAgility() * 1.5f);
        }
        else if(w == "fire staff")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 1f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 2f);
        }
        else if(w == "sacred staff")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 0.6f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 0.9f);
        }

        return 0;
    }

    private string GetCritical(string w)
    {
        if (w == "sword")
        {
            return "20%";
        }
        else if (w == "axe")
        {
            return "15%";
        }
        else if (w == "spear")
        {
            return "25%";
        }
        else if (w == "bow")
        {
            return "25%";
        }
        else if (w == "fire staff")
        {
            return "20%";
        }
        else if (w == "sacred staff")
        {
            return "15%";
        }

        return "";
    }

    private float GetEnergy(string w)
    {
        if (w == "sword" || w == "bow") return 2f;
        else if (w == "axe" || w == "Demolicíon" || w == "Bola de fuego") return 2.5f;
        else if (w == "spear" || w == "fire staff") return 1.5f;
        else if (w == "sacred staff") return 1f;
        else if (w == "Partir" || w == "Curar") return 3f;
        else if (w == "Aumento de fuerza" || w == "Instinto asesino" || w == "Sacrificio de sangre") return 1.5f;
        else if (w == "Demacia" || w == "Juicio Final") return 4f;
        else if (w == "Lluvia de meteoritos" || w == "Revivir") return 5f;
        else if (w == "Lluvia de flechas") return 3.5f;

        return 0;
    }

    private string GetRange(string w)
    {
        if (w == "sword" || w == "axe" || w == "Partir") return "1";
        else if (w == "spear" || w == "Demacia" || w == "Revivir") return "2";
        else if (w == "bow" || w == "Demolicíon") return "7";
        else if (w == "fire staff" || w == "Bola de fuego") return "6";
        else if (w == "sacred staff" || w == "Curar") return "5";
        else if (w == "Aumento de fuerza" || w == "Instinto asesino" || w == "Sacrificio de sangre") return " - ";
        else if (w == "Lluvia de flechas" || w == "Lluvia de meteoritos" || w == "Juicio Final") return "10";

        return "";
    }

    private string GetSkillName(string w, int i)
    {
        if (w == "sword" || w == "axe")
        {
            if (i == 1) return "Partir";
            else if (i == 2) return "Aumento de fuerza";
            else if (i == 2) return "Demacia";
        }
        else if (w == "spear" || w == "bow")
        {
            if (i == 1) return "Demolicíon";
            else if (i == 2) return "Instinto asesino";
            else if (i == 2) return "Lluvia de flechas";
        }
        else if (w == "fire staff")
        {
            if (i == 1) return "Bola de fuego";
            else if (i == 2) return "Sacrificio de sangre";
            else if (i == 2) return "Lluvia de meteoritos";
        }
        else if (w == "sacred staff")
        {
            if (i == 1) return "Curar";
            else if (i == 2) return "Revivir";
            else if (i == 2) return "Juicio Final";
        }

        return "";
    }

    private int GetSkillDmg(string w, bool d, int i)
    {
        if (w == "Partir")
        {
            if (d) return (int)(playerS[i].GetStrenght() * 0.9f);
            else if (!d) return (int)(playerS[i].GetStrenght() * 1.3f);
        }
        else if (w == "Aumento de fuerza")
        {
            if (d) return (int)(playerS[i].GetStrenght() * 1.2f);
            else if (!d) return (int)(playerS[i].GetStrenght() * 1.6f);
        }
        else if (w == "Demacia")
        {
            if (d) return (int)(playerS[i].GetAgility() * 1f);
            else if (!d) return (int)(playerS[i].GetAgility() * 1.2f);
        }
        else if (w == "Demolicíon")
        {
            if (d) return (int)(playerS[i].GetAgility() * 0.8f);
            else if (!d) return (int)(playerS[i].GetAgility() * 1.5f);
        }
        else if (w == "Instinto asesino")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 1f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 2f);
        }
        else if (w == "Bola de fuego")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 0.6f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 0.9f);
        }
        else if (w == "Sacrificio de sangre")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 0.6f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 0.9f);
        }
        else if (w == "Lluvia de meteoritos")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 0.6f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 0.9f);
        }
        else if (w == "Curar")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 0.6f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 0.9f);
        }
        else if (w == "Revivir")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 0.6f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 0.9f);
        }
        else if (w == "Juicio Final")
        {
            if (d) return (int)(playerS[i].GetIntellect() * 0.6f);
            else if (!d) return (int)(playerS[i].GetIntellect() * 0.9f);
        }


        return 0;
    }
}

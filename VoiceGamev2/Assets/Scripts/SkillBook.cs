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
    public List<Sprite> weaponImage = new List<Sprite>();

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

        SetVagnarSkillBook();
    }

    private void TorekBook()
    {
        magnusSB.SetActive(false);
        vagnarSB.SetActive(false);
        torekSB.SetActive(true);
        magnusA.SetActive(true);
        if(psjNum == 2)vagnarA.SetActive(true);
        if(psjNum == 3)torekA.SetActive(false);

        SetTorekSkillBook();
    }

    private void SetMagnusSkillBook()
    {
        string wp = playerS[0].actualWeapon;
        int plyN = 0;

        magnusSB.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = WeaponNameSpanish(wp);
        magnusSB.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = ReturnWeaponImg(wp);
        magnusSB.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Daño: " + GetBasicAtk(wp, plyN, true) + " - " +
            GetBasicAtk(wp, plyN, false) + "\n" + "Energía: " + GetEnergy(wp) + "\n" + "Rango: " +
            GetRange(wp) + "\n" + "Crítico: " + GetCritical(plyN);

        if(ValidationSkill(wp, plyN) >= 1)
        {
            magnusSB.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 1);
            magnusSB.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 1));
            magnusSB.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 1));
            magnusSB.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 1), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 1)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 1));
        }
        else magnusSB.transform.GetChild(2).gameObject.SetActive(false);

        if (ValidationSkill(wp, plyN) >= 2)
        {
            magnusSB.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 2);
            magnusSB.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 2));
            magnusSB.transform.GetChild(3).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 2));
            magnusSB.transform.GetChild(3).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 2), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 2)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 2));
        }
        else magnusSB.transform.GetChild(3).gameObject.SetActive(false);

        if (ValidationSkill(wp, plyN) >= 3)
        {
            magnusSB.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 3);
            magnusSB.transform.GetChild(4).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 3));
            magnusSB.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 3));
            magnusSB.transform.GetChild(4).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 3), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 3)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 3));
        }
        else magnusSB.transform.GetChild(4).gameObject.SetActive(false);
    }

    private void SetVagnarSkillBook()
    {
        string wp = playerS[1].actualWeapon;
        int plyN = 1;

        vagnarSB.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = WeaponNameSpanish(wp);
        vagnarSB.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = ReturnWeaponImg(wp);
        vagnarSB.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Daño: " + GetBasicAtk(wp, plyN, true) + " - " +
            GetBasicAtk(wp, plyN, false) + "\n" + "Energía: " + GetEnergy(wp) + "\n" + "Rango: " +
            GetRange(wp) + "\n" + "Crítico: " + GetCritical(plyN);

        if (ValidationSkill(wp, plyN) >= 1)
        {
            vagnarSB.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 1);
            vagnarSB.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 1));
            vagnarSB.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 1));
            vagnarSB.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 1), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 1)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 1));
        }
        else vagnarSB.transform.GetChild(2).gameObject.SetActive(false);

        if (ValidationSkill(wp, plyN) >= 2)
        {
            vagnarSB.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 2);
            vagnarSB.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 2));
            vagnarSB.transform.GetChild(3).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 2));
            vagnarSB.transform.GetChild(3).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 2), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 2)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 2));
        }
        else vagnarSB.transform.GetChild(3).gameObject.SetActive(false);

        if (ValidationSkill(wp, plyN) >= 3)
        {
            vagnarSB.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 3);
            vagnarSB.transform.GetChild(4).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 3));
            vagnarSB.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 3));
            vagnarSB.transform.GetChild(4).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 3), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 3)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 3));
        }
        else vagnarSB.transform.GetChild(4).gameObject.SetActive(false);
    }

    private void SetTorekSkillBook()
    {
        string wp = playerS[2].actualWeapon;
        int plyN = 2;

        torekSB.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = WeaponNameSpanish(wp);
        torekSB.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = ReturnWeaponImg(wp);
        torekSB.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Daño: " + GetBasicAtk(wp, plyN, true) + " - " +
            GetBasicAtk(wp, plyN, false) + "\n" + "Energía: " + GetEnergy(wp) + "\n" + "Rango: " +
            GetRange(wp) + "\n" + "Crítico: " + GetCritical(plyN);

        if (ValidationSkill(wp, plyN) >= 1)
        {
            torekSB.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 1);
            torekSB.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 1));
            torekSB.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 1));
            torekSB.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 1), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 1)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 1));
        }
        else torekSB.transform.GetChild(2).gameObject.SetActive(false);

        if (ValidationSkill(wp, plyN) >= 2)
        {
            torekSB.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 2);
            torekSB.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 2));
            torekSB.transform.GetChild(3).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 2));
            torekSB.transform.GetChild(3).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 2), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 2)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 2));
        }
        else torekSB.transform.GetChild(3).gameObject.SetActive(false);

        if (ValidationSkill(wp, plyN) >= 3)
        {
            torekSB.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = GetSkillName(wp, 3);
            torekSB.transform.GetChild(4).GetChild(1).GetComponent<Image>().sprite = ReturnImg(GetSkillName(wp, 3));
            torekSB.transform.GetChild(4).GetChild(2).GetComponent<Text>().text = SkillDescription(GetSkillName(wp, 3));
            torekSB.transform.GetChild(4).GetChild(3).GetComponent<Text>().text = GetSkillDmg(GetSkillName(wp, 3), plyN)
            + "\n" + "Energía: " + GetEnergy(GetSkillName(wp, 3)) + "\n" + "Rango: " +
            GetRange(GetSkillName(wp, 3));
        }
        else torekSB.transform.GetChild(4).gameObject.SetActive(false);
    }

    private int ValidationSkill(string w, int i)
    {
        if (w == "sword" || w == "axe")
        {
            if (playerS[i].strengthPoints >= 10) return 3;
            else if (playerS[i].strengthPoints >= 8) return 2;
            else if (playerS[i].strengthPoints >= 6) return 1;
        }
        else if (w == "spear" || w == "bow")
        {
            if (playerS[i].agilityPoints >= 10) return 3;
            else if (playerS[i].agilityPoints >= 8) return 2;
            else if (playerS[i].agilityPoints >= 6) return 1;
        }
        else if (w == "fire staff" || w == "sacred staff")
        {
            if (playerS[i].intellectPoints >= 10) return 3;
            else if (playerS[i].intellectPoints >= 8) return 2;
            else if (playerS[i].intellectPoints >= 6) return 1;
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

    private string GetCritical(int i)
    {
        return playerS[i].criticProb + "%";
    }

    private float GetEnergy(string w)
    {
        if (w == "sword" || w == "bow") return 2f;
        else if (w == "axe" || w == "Demolicíon" || w == "Bola de fuego") return 2.5f;
        else if (w == "spear" || w == "fire staff") return 1.5f;
        else if (w == "sacred staff") return 1f;
        else if (w == "Partir" || w == "Curar") return 3f;
        else if (w == "Aumento de fuerza" || w == "Instinto asesino" || w == "Sacrificio") return 1.5f;
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
        else if (w == "Aumento de fuerza" || w == "Instinto asesino" || w == "Sacrificio") return " - ";
        else if (w == "Lluvia de flechas" || w == "Lluvia de meteoritos" || w == "Juicio Final") return "10";

        return "";
    }

    public string GetSkillName(string w, int i)
    {
        if (w == "sword" || w == "axe")
        {
            if (i == 1) return "Partir";
            else if (i == 2) return "Aumento de fuerza";
            else if (i == 3) return "Demacia";
        }
        else if (w == "spear" || w == "bow")
        {
            if (i == 1) return "Demolicíon";
            else if (i == 2) return "Instinto asesino";
            else if (i == 3) return "Lluvia de flechas";
        }
        else if (w == "fire staff")
        {
            if (i == 1) return "Bola de fuego";
            else if (i == 2) return "Sacrificio";
            else if (i == 3) return "Lluvia de meteoritos";
        }
        else if (w == "sacred staff")
        {
            if (i == 1) return "Curar";
            else if (i == 2) return "Revivir";
            else if (i == 3) return "Juicio Final";
        }

        return "";
    }

    private string GetSkillDmg(string w, int i)
    {
        if (w == "Partir")
        {
            return "Daño: " + (int)(playerS[i].GetStrenght() * 1.2f) + " - " + (int)(playerS[i].GetStrenght() * 2f);
        }
        else if (w == "Aumento de fuerza")
        {
            return "Aumento de fuerza: x1.5 ";
        }
        else if (w == "Demacia")
        {
            return "Daño: " + (int)(playerS[i].GetStrenght() * 2f) + " - " + (int)(playerS[i].GetStrenght() * 3.5f);
        }
        else if (w == "Demolicíon")
        {
            return "Daño: " + (int)(playerS[i].GetAgility() * 0.8f) + " - " + (int)(playerS[i].GetStrenght() * 1.5f) + " + Escudo";
        }
        else if (w == "Instinto asesino")
        {
            return "Aumento de agilidad: x1.5 & Crítico: +50%";
        }
        else if (w == "Lluvia de flechas")
        {
            return "Daño: " + (int)(playerS[i].GetAgility() * 0.2f) + " - " + (int)(playerS[i].GetAgility() * 0.4f) + " x hit";
        }
        else if (w == "Bola de fuego")
        {
            return "Daño: " + (int)(playerS[i].GetIntellect() * 1.5f) + " - " + (int)(playerS[i].GetIntellect()* 2.2f);
        }
        else if (w == "Sacrificio")
        {
            return "Aumento de inteligencia: x1.5 ";
        }
        else if (w == "Lluvia de meteoritos")
        {
            return "Daño: " + (int)(playerS[i].GetIntellect() * 0.4f) + " - " + (int)(playerS[i].GetIntellect() * 0.6f) + " x hit";
        }
        else if (w == "Curar")
        {
            return "Curacíon: " + (int)(playerS[i].GetIntellect() * 0.6f) + " - " + (int)(playerS[i].GetIntellect() * 1f);
        }
        else if (w == "Revivir")
        {
            return "Vida extra: " + (int)(playerS[i].GetStrenght() * 1f);
        }
        else if (w == "Juicio Final")
        {
            return "Daño: " + (int)(playerS[i].GetIntellect() * 1.5f) + " - " + (int)(playerS[i].GetIntellect() * 2.5f);
        }

        return "";
    }

    private Sprite ReturnImg(string w)
    {
        if (w == "Partir")
        {
            return skillImage[0];
        }
        else if (w == "Aumento de fuerza")
        {
            return skillImage[1];
        }
        else if (w == "Demacia")
        {
            return skillImage[2];
        }
        else if (w == "Demolicíon")
        {
            return skillImage[3];
        }
        else if (w == "Instinto asesino")
        {
            return skillImage[4];
        }
        else if (w == "Lluvia de flechas")
        {
            return skillImage[5];
        }
        else if (w == "Bola de fuego")
        {
            return skillImage[6];
        }
        else if (w == "Sacrificio")
        {
            return skillImage[7];
        }
        else if (w == "Lluvia de meteoritos")
        {
            return skillImage[8];
        }
        else if (w == "Curar")
        {
            return skillImage[9];
        }
        else if (w == "Revivir")
        {
            return skillImage[10];
        }
        else if (w == "Juicio Final")
        {
            return skillImage[11];
        }

        return null;
    }

    private Sprite ReturnWeaponImg(string w)
    {
        if (w == "sword")
        {
            return weaponImage[0];
        }
        else if (w == "axe")
        {
            return weaponImage[1];
        }
        else if (w == "spear")
        {
            return weaponImage[2];
        }
        else if (w == "bow")
        {
            return weaponImage[3];
        }
        else if (w == "fire staff")
        {
            return weaponImage[4];
        }
        else if (w == "sacred staff")
        {
            return weaponImage[5];
        }

        return null;
    }

    private string WeaponNameSpanish(string w)
    {
        if (w == "sword")
        {
            return "Espada";
        }
        else if (w == "axe")
        {
            return "Hacha";
        }
        else if (w == "spear")
        {
            return "Lanza";
        }
        else if (w == "bow")
        {
            return "Arco";
        }
        else if (w == "fire staff")
        {
            return "Báculo de fuego";
        }
        else if (w == "sacred staff")
        {
            return "Báculo sagrado";
        }

        return null;
    }

    private string SkillDescription(string w)
    {
        if (w == "Partir")
        {
            return "Ataca a los enemigos que se encuentren delante del personaje";
        }
        else if (w == "Aumento de fuerza")
        {
            return "El personaje obtiene un aumento de fuerza durante 2 turnos";
        }
        else if (w == "Demacia")
        {
            return "Ataque en salto que noquea a los enemigos en un rango amplio durante el siguiente turno";
        }
        else if (w == "Demolicíon")
        {
            return "Ataque que destruye cualquier escudo enemigo al instante, y hace daño extra";
        }
        else if (w == "Instinto asesino")
        {
            return "El personaje obtiene un aumento de agilidad y % critica durante 2 turnos";
        }
        else if (w == "Lluvia de flechas")
        {
            return "Multiples flechas caen del cielo realizando daño a los enemigos en un rango amplio";
        }
        else if (w == "Bola de fuego")
        {
            return "Ataque mágico, el cual quema al enemigo con el que colisiona";
        }
        else if (w == "Sacrificio")
        {
            return "El personaje obtiene un aumento de inteligencia, y sus ataques de fuego aplican marcas de fuego durante 2 turnos";
        }
        else if (w == "Lluvia de meteoritos")
        {
            return "Multiples rocas caen del cielo realizando gran daño a los enemigos en un rango amplio";
        }
        else if (w == "Curar")
        {
            return "Cura a un aliado seleccionado";
        }
        else if (w == "Revivir")
        {
            return "Resucita un aliado, solo se puede usar una vez";
        }
        else if (w == "Juicio Final")
        {
            return "Invoca una espada sagrada, la cual cae y daña a los enemigos";
        }

        return null;
    }
}

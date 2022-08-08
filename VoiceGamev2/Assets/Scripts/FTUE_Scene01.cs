using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE_Scene01 : MonoBehaviour
{
    private CameraFollow gameM;
    private PlayerMove plMove;

    public List<GameObject> tutos = new List<GameObject>();

    private bool p1, p2, p3, p4, p5, p6, p7, p8, p9, p10;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        plMove = gameM.GetComponent<PlayerMove>();

        gameM.nextTurnRestriction = true;
        gameM.cancelRestriction = true;
        plMove.moveRestriction = true;
        plMove.atkRestriction = true;
        plMove.spellRestriction = true;
        gameM.sbookRestriction = true;

        p1 = true;
    }

    void Update()
    {
        if (p1) Part01();
        if (p2) StartCoroutine(Part02());
        if (p3) Part03();
        if (p4) Part04();
        if (p5) Part05();
        if (p6) Part06();
        if (p7) Part07();
        if (p8) Part08();
        if (p9) Part09();
        if (p10) Part10();
    }

    public void Part01()
    {
        if (gameM.selectPjActive)
        {
            tutos[0].SetActive(false);
            tutos[1].SetActive(true);


            p1 = false;
            p2 = true;
        }
    }
    
    public IEnumerator Part02()
    {
        p2 = false;

        yield return new WaitForSeconds(3.5f);

        tutos[2].SetActive(true);

        plMove.moveRestriction = false;

        p3 = true;
    }

    public void Part03()
    {
        if (plMove.moveActive)
        {
            tutos[1].SetActive(false);
            tutos[2].SetActive(false);
            tutos[3].SetActive(true);

            p3 = false;
            p4 = true;
        }
    }

    public void Part04()
    {
        if (plMove.move2Active)
        {
            tutos[3].SetActive(false);

            StartCoroutine(StartPart04());

            p4 = false;
        }
    }

    private IEnumerator StartPart04()
    {
        yield return new WaitForSeconds(3.8f);

        tutos[4].SetActive(true);

        gameM.nextTurnRestriction = false;

        plMove.moveRestriction = true;

        p5 = true;
    }

    public void Part05()
    {
        if (!gameM.whoTurn)
        {
            tutos[4].SetActive(false);

            p5 = false;
            p6 = true;
        }
    }

    public void Part06()
    {
        if (gameM.whoTurn)
        {
            tutos[5].SetActive(true);

            plMove.move2Restriction = true;
            plMove.moveRestriction = false;

            p6 = false;
            p7 = true;
        }
    }

    public void Part07()
    {
        if (gameM.selectPjActive)
        {
            tutos[5].SetActive(false);
            tutos[6].SetActive(true);

            p7 = false;
            p8 = true;
        }
    }

    public void Part08()
    {
        if (plMove.moveActive)
        {
            tutos[6].SetActive(false);
            tutos[7].SetActive(true);

            gameM.cancelRestriction = false;

            p8 = false;
            p9 = true;
        }
    }

    public void Part09()
    {
        if (gameM.cancelActive)
        {
            tutos[7].SetActive(false);
            p9 = false;
            StartCoroutine(StartPart10());
        }
    }

    private IEnumerator StartPart10()
    {
        yield return new WaitForSeconds(1.5f);

        tutos[8].SetActive(true);

        plMove.atkRestriction = false;
        plMove.moveRestriction = true;

        p10 = true;
    }

    public void Part10()
    {
        if (plMove.atkActive)
        {
            tutos[8].SetActive(false);

            p10 = false;
        }
    }
}

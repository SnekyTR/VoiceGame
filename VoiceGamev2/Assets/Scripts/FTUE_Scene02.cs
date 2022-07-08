using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE_Scene02 : MonoBehaviour
{
    private CameraFollow gameM;
    private PlayerMove plMove;

    public List<GameObject> tutos = new List<GameObject>();

    private bool p0, p1, p2, p3, p4, p5, p6;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        plMove = gameM.GetComponent<PlayerMove>();

        gameM.nextTurnRestriction = true;
        plMove.moveRestriction = false;
        plMove.atkRestriction = false;
        plMove.atk2Restriction = true;
        plMove.spellRestriction = true;

        p0 = true;
    }

    void Update()
    {
        if (p0) Part00();
        if (p1) Part01();
        if (p2) Part02();
        if (p3) Part03();
        if (p4) Part04();
        if (p5) Part05();
        if (p6) Part06();
    }

    private void Part00()
    {
        if(plMove.atk0Active || plMove.moveActive)
        {
            p0 = false;

            tutos[5].SetActive(false);
            tutos[0].SetActive(true);

            plMove.moveRestriction = false;
            plMove.atkRestriction = false;

            p1 = true;
        }
    }

    private void Part01()
    {
        if (gameM.cancelActive)
        {
            p1 = false;

            plMove.spellRestriction = false;
            plMove.atk2Restriction = false;

            tutos[0].SetActive(false);
            tutos[1].SetActive(true);

            p2 = true;
        }
    }

    private void Part02()
    {
        if (plMove.spellActive)
        {
            p2 = false;

            tutos[1].SetActive(false);
            tutos[2].SetActive(true);

            p3 = true;
        }
    }

    private void Part03()
    {
        if (plMove.atkActive)
        {
            p3 = false;

            tutos[2].SetActive(false);
            tutos[3].SetActive(true);

            gameM.nextTurnRestriction = false;

            p4 = true;
        }
    }

    private void Part04()
    {
        if (gameM.nextTurnActive)
        {
            p4 = false;

            tutos[3].SetActive(false);

            StartCoroutine(WaitPart04());
        }
    }

    private IEnumerator WaitPart04()
    {
        yield return new WaitForSeconds(1f);

        p5 = true;
    }

    private void Part05()
    {
        if (gameM.whoTurn && gameM.selectPjActive)
        {
            p5 = false;

            tutos[4].SetActive(true);

            plMove.atkActive = false;
            plMove.spellActive = false;
            plMove.moveActive = false;
            plMove.move2Active = false;
            gameM.cancelActive = false;

            gameM.nextTurnRestriction = false;
            plMove.moveRestriction = false;
            plMove.atkRestriction = false;
            plMove.spellRestriction = false;

            p6 = true;
        }
    }

    private void Part06()
    {
        if(plMove.atkActive || plMove.moveActive || plMove.spellActive)
        {
            p6 = false;

            tutos[4].SetActive(false);
        }
    }
}

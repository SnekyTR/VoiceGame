using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE_Scene02 : MonoBehaviour
{
    private CameraFollow gameM;
    private PlayerMove plMove;
    private PlayerStats plStats;

    public List<GameObject> tutos = new List<GameObject>();

    private bool p0, p1, p2, p3, p4, p5, p6;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        plMove = gameM.GetComponent<PlayerMove>();

        gameM.nextTurnRestriction = true;
        plMove.moveRestriction = true;
        plMove.atkRestriction = true;
        plMove.atk2Restriction = true;
        plMove.spellRestriction = true;
        gameM.selectPjRestriction = true;
        gameM.sbookRestriction = true;

        StartCoroutine(InitialPart());
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

    private IEnumerator InitialPart()
    {
        yield return new WaitForSeconds(4f);

        p0 = true;
        gameM.selectPjRestriction = false;

        tutos[1].SetActive(true);
    }

    private void Part00()
    {
        if(gameM.selectPjActive)
        {
            p0 = false;

            tutos[0].SetActive(false);
            tutos[1].SetActive(false);
            tutos[2].SetActive(true);

            plMove.moveRestriction = false;
            plMove.atkRestriction = false;
            gameM.sbookRestriction = false;

            p1 = true;
        }
    }

    private void Part01()
    {
        if (gameM.sbookActive)
        {
            p1 = false;

            tutos[2].SetActive(false);
            tutos[3].SetActive(true);
            plStats = gameM.players[0].GetComponent<PlayerStats>();

            p2 = true;
        }
    }

    private void Part02()
    {
        if (!gameM.sbookActive)
        {
            p2 = false;

            plMove.spellRestriction = false;
            plMove.atk2Restriction = false;

            tutos[3].SetActive(false);
            tutos[4].SetActive(true);

            p3 = true;
        }
    }

    private void Part03()
    {
        if (plMove.spellActive)
        {
            p3 = false;

            tutos[4].SetActive(false);
            tutos[5].SetActive(true);

            StartCoroutine(CancelProgress());
        }
    }

    private IEnumerator CancelProgress()
    {
        tutos[4].SetActive(false);
        tutos[5].SetActive(true);

        yield return new WaitForSeconds(2f);

        tutos[5].transform.GetChild(1).gameObject.SetActive(true);

        gameM.cancelRestriction = false;

        p4 = true;
    }

    private void Part04()
    {
        if (gameM.cancelActive)
        {
            p4 = false;

            tutos[5].SetActive(false);

            p5 = true;
        }
    }

    private void Part05()
    {
        if (plStats.GetEnergy(1) <= 0.5f || plStats.GetEnergy(2) <= 1)
        {
            p5 = false;

            tutos[6].SetActive(true);

            gameM.nextTurnRestriction = false;

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
        if (gameM.nextTurnActive)
        {
            p6 = false;

            tutos[6].SetActive(false);

            plMove.atkActive = false;
            plMove.spellActive = false;
            plMove.moveActive = false;
            plMove.move2Active = false;
            gameM.cancelActive = false;

            gameM.nextTurnRestriction = false;
            plMove.moveRestriction = false;
            plMove.atkRestriction = false;
            plMove.spellRestriction = false;

            Destroy(transform.gameObject);
        }
    }
}

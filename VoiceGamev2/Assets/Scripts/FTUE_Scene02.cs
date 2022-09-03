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

    public List<Transform> objs = new List<Transform>();

    private float scaling = 1f;
    private float mult = 0.5f;

    private Transform playerTr;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        plMove = gameM.GetComponent<PlayerMove>();

        gameM.nextTurnRestriction = true;
        plMove.moveRestriction = true;
        plMove.atkRestriction = true;
        plMove.atk2Restriction = true;
        plMove.spellRestriction = true;
        gameM.selectPjRestriction = false;
        gameM.sbookRestriction = true;

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
        scaling += mult * Time.deltaTime;

        if (objs[0].localScale.x >= 1.3)
        {
            mult = (-0.5f);
        }
        else if (objs[0].localScale.x <= 0.8)
        {
            mult = (0.5f);
        }

        objs[0].localScale = new Vector3(scaling, scaling, 1);

        if (gameM.selectPjActive)
        {
            p0 = false;

            tutos[0].SetActive(false);
            tutos[1].SetActive(true);

            objs[0].localScale = new Vector3(1, 1, 1);

            gameM.sbookRestriction = false;

            p1 = true;
        }
    }

    private void Part01()
    {
        scaling += mult * Time.deltaTime;

        if (objs[1].localScale.x >= 1.3)
        {
            mult = (-0.5f);
        }
        else if (objs[1].localScale.x <= 0.8)
        {
            mult = (0.5f);
        }

        objs[1].localScale = new Vector3(scaling, scaling, 1);

        if (gameM.sbookActive)
        {
            p1 = false;

            tutos[1].SetActive(false);
            tutos[2].SetActive(true);
            plStats = gameM.players[0].GetComponent<PlayerStats>();

            p2 = true;
        }
    }

    private void Part02()
    {
        scaling += mult * Time.deltaTime;

        if (objs[1].localScale.x >= 1.4)
        {
            mult = (-0.5f);
        }
        else if (objs[1].localScale.x <= 0.8)
        {
            mult = (0.5f);
        }

        objs[1].localScale = new Vector3(scaling, scaling, 1);

        playerTr = gameM.playerParent;

        if (!gameM.sbookActive)
        {
            p2 = false;

            plMove.moveRestriction = false;
            gameM.nextTurnRestriction = false;

            objs[1].localScale = new Vector3(1, 1, 1);

            tutos[2].SetActive(false);

            p3 = true;
        }
    }

    private void Part03()
    {
        if (Vector3.Distance(gameM.enemys[0].transform.position, playerTr.position) <= 3 && gameM.whoTurn && gameM.selectPjActive || plMove.moveActive)
        {
            p3 = false;

            plMove.spellRestriction = false;
            plMove.atk2Restriction = false;

            tutos[3].SetActive(true);

            p4 = true;
        }
    }

    private IEnumerator AbilityProgress()
    {

        yield return new WaitForSeconds(1f);

        tutos[4].transform.GetChild(3).gameObject.SetActive(true);

        gameM.cancelRestriction = false;

        p4 = true;
    }

    private void Part04()
    {
        if (plMove.spellActive)
        {
            p4 = false;

            tutos[3].SetActive(false);
            tutos[4].SetActive(true);

            StartCoroutine(AbilityProgress());

            p5 = true;
        }
    }

    private void Part05()
    {
        if (plMove.atkActive)
        {
            p4 = false;

            tutos[4].SetActive(false);

            gameM.nextTurnRestriction = false;
            plMove.moveRestriction = false;
            plMove.atkRestriction = false;
            plMove.atk2Restriction = false;
            plMove.spellRestriction = false;
            gameM.selectPjRestriction = false;
            gameM.sbookRestriction = false;
        }
    }

    private void Part06()
    {

    }
}

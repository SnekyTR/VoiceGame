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
        if(gameM.selectPjActive)
        {
            p0 = false;

            tutos[0].SetActive(false);
            tutos[1].SetActive(true);

            gameM.sbookRestriction = false;

            p1 = true;
        }
    }

    private void Part01()
    {
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
        if (!gameM.sbookActive)
        {
            p2 = false;

            plMove.moveRestriction = false;
            gameM.nextTurnRestriction = false;

            tutos[2].SetActive(false);

            p3 = true;
        }
    }

    private void Part03()
    {
        if (Vector3.Distance(gameM.enemys[0].transform.position, gameM.playerParent.position) <= 3)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE_Scene01 : MonoBehaviour
{
    private CameraFollow gameM;
    private PlayerMove plMove;

    public List<GameObject> tutos = new List<GameObject>();

    private bool p1, p2, p3, p4, p5;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        plMove = gameM.GetComponent<PlayerMove>();

        gameM.nextTurnRestriction = true;
        gameM.cancelRestriction = true;
        plMove.moveRestriction = true;
        plMove.atkRestriction = true;
        plMove.spellRestriction = true;

        p1 = true;
    }

    void Update()
    {
        if (p1) Part01();
        if (p2) StartCoroutine(Part02());
        if (p3) Part03();
        if (p4) Part04();
        if (p5) Part05();
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

        plMove.atkRestriction = false;
        plMove.moveRestriction = true;

        p5 = true;
    }

    public void Part05()
    {
        if (plMove.atkActive)
        {
            tutos[4].SetActive(false);

            p5 = false;
        }
    }
}

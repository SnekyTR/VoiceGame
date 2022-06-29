using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;

public class VoiceDestinations : MonoBehaviour
{
    [SerializeField]Transform objectTransform;
    public CombatEnter combatEnter;
    public bool entered;
    private NavMeshAgent agent;
    private Dictionary<string, Action> mapActions = new Dictionary<string, Action>();
    private KeywordRecognizer mapDestinations;
    public bool combat;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        AddDestinations();
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position = new Vector3(objectTransform.position.x, objectTransform.position.y, objectTransform.position.z) * (0.1f * Time.deltaTime);
    }

    private void AddDestinations()
    {
        mapActions.Add("reposo", SelectDestination);
        mapDestinations = new KeywordRecognizer(mapActions.Keys.ToArray());
        mapDestinations.OnPhraseRecognized += RecognizedVoice;
        mapDestinations.Start();
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        mapActions[speech.text].Invoke();
    }
    private void SelectDestination()
    {
        objectTransform = GameObject.Find("Rest of the Giant").transform;

        //combatEnter.www();
        if (entered)
        {
            StopPlayer();
            combatEnter.CheckLvlPanel();
        }
        else
        {
            agent.SetDestination(objectTransform.position);
        }
        //if (combatEnter.combat == true) { print("Se ha parado player"); StopPlayer(); } else {  }
        //else { ); }
    }
    public void StopPlayer()
    {
        agent.SetDestination(transform.position);
        
    }

}

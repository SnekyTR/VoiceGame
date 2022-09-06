using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class VoiceDestinations : MonoBehaviour
{
    [SerializeField]Transform objectTransform;
    [SerializeField]FTUE_Progresion fTUE_Progresion;

    private String selectedLocation;
    public CombatEnter combatEnter;
    public bool entered = false;
    [SerializeField] private NavMeshAgent agent;
    private Dictionary<string, Action> mapActions = new Dictionary<string, Action>();
    public KeywordRecognizer mapDestinations;
    private BoxCollider boxCollider;
    private UIMovement uIMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        AddDestinations();
    }
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        uIMovement = GameObject.Find("Canvas").GetComponent<UIMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position = new Vector3(objectTransform.position.x, objectTransform.position.y, objectTransform.position.z) * (0.1f * Time.deltaTime);
    }

    private void AddDestinations()
    {
        mapActions.Add("reposo del gigante", SelectDestination);
        mapActions.Add("bastion negro", SelectDestination);
        mapActions.Add("bosque de los aullidos", SelectDestination);
        mapActions.Add("olerfeld", SelectDestination);
        mapActions.Add("Last", SelectDestination);
        mapDestinations = new KeywordRecognizer(mapActions.Keys.ToArray());
        mapDestinations.OnPhraseRecognized += RecognizedVoice;
    }
    public void CloseDestinations()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, SelectDestination);
        mapDestinations = new KeywordRecognizer(zero1.Keys.ToArray());
        mapDestinations.OnPhraseRecognized += RecognizedVoice;
        
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        selectedLocation = speech.text.ToString();
        mapActions[speech.text].Invoke();
        
        
    }
    private void SelectDestination()
    {
        if (uIMovement.inOptions) return;
        objectTransform = GameObject.Find(selectedLocation).transform;
        boxCollider.enabled = true;
        //combatEnter.www();
        if (entered)
        {
            StopPlayer();
            combatEnter.CheckLvlPanel();
        }
        else
        {
            agent.SetDestination(objectTransform.position);
            if(fTUE_Progresion.ftueProgression == 6)
            {
                fTUE_Progresion.ftueProgression++;
                fTUE_Progresion.FTUEProgression();
            }
        }
        //if (combatEnter.combat == true) { print("Se ha parado magnus"); StopPlayer(); } else {  }
        //else { ); }
    }
    public void StopPlayer()
    {
        agent.SetDestination(transform.position);
      
        
    }

}

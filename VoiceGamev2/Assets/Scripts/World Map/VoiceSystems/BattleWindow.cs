using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class BattleWindow : MonoBehaviour
{
    private Dictionary<string, Action> combatPanelActions = new Dictionary<string, Action>();
    public KeywordRecognizer combatPanel;
    private GameSave gameSave;
    private FTUE_Progresion fTUE_Progresion;
    int index;
    private CombatEnter combatEnter;
    [SerializeField] private VoiceDestinations voices;
    [SerializeField] private IncreaseStats increaseStats;
    [SerializeField] private LoadingScreen loadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameSave = GameObject.Find("GameSaver").GetComponent<GameSave>();
        fTUE_Progresion = GameObject.Find("Canvas").GetComponent<FTUE_Progresion>();
        AddVoice();
    }

    private void AddVoice()
    {
        combatPanelActions.Add("luchar", EnterBatle);
        //combatPanelActions.Add("cancelar", ClosePannel);
        combatPanel = new KeywordRecognizer(combatPanelActions.Keys.ToArray());
        combatPanel.OnPhraseRecognized += RecognizedVoice;
    }
    public void CloseCombatOrder()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, EnterBatle);
        combatPanel = new KeywordRecognizer(zero1.Keys.ToArray());
        combatPanel.OnPhraseRecognized += RecognizedVoice;
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        combatPanelActions[speech.text].Invoke();
    }

    public void EnterBatle()
    {
        //fTUE_Progresion.CloseOrders();
        gameSave.SaveGame();
        print(index);
        //SceneManager.UnloadSceneAsync(0);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        combatPanel.Stop();
        voices.CloseDestinations();
        CloseCombatOrder();
        increaseStats.CloseOrders();
        loadingScreen.LoadScene(combatEnter.sceneIndex);
    }
    private void ClosePannel()
    {
        combatPanel.Stop();
        this.gameObject.SetActive(false);
    }
    public void GetCombatEnter(GameObject combatScript)
    {
        combatEnter = combatScript.GetComponent<CombatEnter>();
    }
}

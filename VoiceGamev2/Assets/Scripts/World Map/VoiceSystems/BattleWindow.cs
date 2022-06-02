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
    int index;
    private CombatEnter combatEnter;

    // Start is called before the first frame update
    void Start()
    {
        gameSave = GameObject.Find("GameSaver").GetComponent<GameSave>();
        AddVoice();
    }

    private void AddVoice()
    {
        combatPanelActions.Add("luchar", EnterBatle);
        combatPanelActions.Add("cancelar", ClosePannel);

        combatPanel = new KeywordRecognizer(combatPanelActions.Keys.ToArray());
        combatPanel.OnPhraseRecognized += RecognizedVoice;

    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        combatPanelActions[speech.text].Invoke();
    }

    public void EnterBatle()
    {
        gameSave.SaveGame();
        print(index);
        SceneManager.LoadScene(combatEnter.sceneIndex);
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

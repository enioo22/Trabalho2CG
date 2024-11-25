using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//IDictionary<string, List<List<string>>>

class DialogTitleHead
{
    public Sprite DialogHead;
    public string DialogTitle;
    
    public DialogTitleHead(Sprite dialoghead, string title)
    {
        this.DialogHead = dialoghead;
        this.DialogTitle = title;
    }
}




//void TriggerDialog(string DialogArrayTitle)
//{
//    return;
//}

public class DialogManager : MonoBehaviour
{
    public Sprite TutorialCrazyCientistHeadSprite;
    public Sprite OtherHeadSprite;
    public UnityEngine.UI.Image DialogBoxHead;
    public TextAsset dialogsjson;

    public GameObject DialogPanel;
    public TextMeshProUGUI DialogBoxTitle;
    public TextMeshProUGUI DialogBoxText;

    int dialogoAtual;
    int stepAtualDialogo = 0;

    Dialogs dialogboxes;

    DialogTitleHead TutorialCrazyCientist;



    [System.Serializable]
    public class DialogHeadTitleText
    {
        public int head;
        public string title;
        public string text;
    }

    [System.Serializable]
    public class Dialogs
    {
        public List<DialogHeadTitleText> TutorialDialog; // ID = 0
        public List<DialogHeadTitleText> DefaultDialog; // ID = -1

    }

    List<DialogHeadTitleText> getDialogHTT(int id)
    {
        switch (id)
        {
            case 0:
                return dialogboxes.TutorialDialog;
            default:
                return dialogboxes.DefaultDialog;

        }
    }

    Sprite getDialogBoxHeadSprite(int id)
    {
        switch (id)
        {
            case 0:
                return OtherHeadSprite;
            case 1:
                return TutorialCrazyCientistHeadSprite;
            default:
                return OtherHeadSprite;
        }
    }

    public void onDialogClick()
    {
        if(dialogoAtual == -1)
        {
            DialogPanel.SetActive(false);
            return;
        }
        stepAtualDialogo++;
        callDialog(dialogoAtual);
    }

    public void callDialog(int id)
    {
        List<DialogHeadTitleText> dialogo = getDialogHTT(dialogoAtual);
        if (stepAtualDialogo >= dialogo.Count)
        {
            dialogoAtual = -1;
            DialogPanel.SetActive(false);
            return;
        }
        dialogoAtual = id;
        DialogPanel.SetActive(true);
        
        DialogBoxHead.sprite = getDialogBoxHeadSprite(dialogo[stepAtualDialogo].head);
        DialogBoxText.text = dialogo[stepAtualDialogo].text;
        DialogBoxTitle.text = dialogo[stepAtualDialogo].title;

        
    }

    
    

    // Start is called before the first frame update
    void Start()
    {
        dialogboxes = JsonUtility.FromJson<Dialogs>(dialogsjson.ToString());
        TutorialCrazyCientist = new DialogTitleHead(TutorialCrazyCientistHeadSprite, "Eco-Cientista");
        
        //DialogBoxTitle.text = TutorialCrazyCientist.DialogTitle;
        //DialogBoxText.text = "Puta que pariu...";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

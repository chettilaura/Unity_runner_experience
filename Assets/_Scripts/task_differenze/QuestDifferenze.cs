using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDifferenze : QuestNPC
{
    public GameObject startTask;
    public GameObject dialoguebox_DOP;
    private GameObject dialogueBoxClone;
    private GameObject spiegazione_canvas;
    public GameObject infoDOP;

    //public GameObject dialoguebox_diff_inProgress; //qui non lo facciamo perché deve finirlo per forza una volta iniziato 
    public GameObject dialoguebox_diff_completed;
    private bool info = false; //info diventa true quando la spiegazione è stata fatta vedere  
    private bool nonCompletedYet = true; //questa variabile diventa true quando torna dal NPC ma non ha ancora raccolto tutti i suoni 
    private int inizio_task = 0; //0-> spiegazione, 1-> primo dialogue, 2-> resto


    void Update()

    {


         //istanzia il primo dialogo di partenza se è stato premuto spazio dopo aver visto la spiegazione
         if( inizio_task == 1){
            if (Input.GetKeyDown(KeyCode.Return)){
                Destroy(spiegazione_canvas);
                dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_DOP, transform.position, Quaternion.identity);
                inizio_task = 2;
            }
         }



        if (questNPC._inTrigger && Input.GetKeyDown(KeyCode.E))
        {

            if (QuestManager.questManager.FirstTaskDone)
            {
                if (inizio_task == 0)
                {
                    spiegazione_canvas = (GameObject)GameObject.Instantiate(infoDOP, transform.position, Quaternion.identity);
                    inizio_task = 1;

                }

                QuestManager.questManager.QuestRequest(this);


                if (QuestManager.questManager.currentQuest.id == 5)
                    startTask.GetComponent<Collider>().enabled = true; //attiva collider sulla sedia 

                else
                    startTask.GetComponent<Collider>().enabled = false;

                if (QuestManager.questManager.currentQuest.progress == Quest.QuestProgress.DONE && inizio_task == 2)
                {
                    //esce dialogo " hai completato il task" & duiventa verde 
                    dialogueBoxClone = (GameObject)GameObject.Instantiate(dialoguebox_diff_completed, transform.position, Quaternion.identity);
                    nonCompletedYet = false;
                }
            } else
            {
                //dialogo task caffè non fatta
            }

        }

        SetQuestMarker();
        
    }
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    [Header("Mini-tasks")]
    public DrawerTask drawerA;
    public DrawerTask drawerB;
    public CleaningTask cleaningTask;
    public TrashBinScorer trashTask;
    public CoffeeTask coffeeTask;

    [Header("Run Control")]
    public GameRunController runController;

    [Header("Progess tracking")]
    public TextMeshProUGUI progressTrackerText;
    public AudioSource taskCompleteAudio;

    // to avoid spamming the console
    bool printedDrawerA, printedDrawerB, printedCleaning, printedTrash, printedCoffee;

    // prevent double-finish
    bool announced;

    void Update()
    {
        if (runController == null || !runController.Started) return;

        // Announce individual task completions once
        if (drawerA && drawerA.IsComplete && !printedDrawerA)
        {
            taskCompleteAudio.Play();
            Debug.Log("Task complete: Drawer A");
            printedDrawerA = true;
        }

        if (drawerB && drawerB.IsComplete && !printedDrawerB)
        {
            taskCompleteAudio.Play();
            Debug.Log("Task complete: Drawer B");
            printedDrawerB = true;
        }

        if (cleaningTask && cleaningTask.IsComplete && !printedCleaning)
        {
            taskCompleteAudio.Play();
            Debug.Log("Task complete: Cleaning");
            printedCleaning = true;
        }

        if (trashTask && trashTask.IsComplete && !printedTrash)
        {
            taskCompleteAudio.Play();
            Debug.Log("Task complete: Trash");
            printedTrash = true;
        }

        if (coffeeTask && coffeeTask.IsComplete && !printedCoffee)
        {
            taskCompleteAudio.Play();
            Debug.Log("Task complete: Coffee");
            printedCoffee = true;
        }

        if (progressTrackerText)
        {
            int completedTaskCounter = 0;
            if (drawerA && drawerA.IsComplete && drawerB && drawerB.IsComplete)
            {
                completedTaskCounter++;
            }
            if (cleaningTask && cleaningTask.IsComplete)
            {
                completedTaskCounter++;
            }
            if (trashTask && trashTask.IsComplete)
            {
                completedTaskCounter++;
            }
            if (coffeeTask && coffeeTask.IsComplete)
            {
                completedTaskCounter++;
            }
            progressTrackerText.text = $"Tasks: {completedTaskCounter} / 4";
        }

        // Are all tasks complete?
        bool allDone =
            drawerA && drawerA.IsComplete &&
            drawerB && drawerB.IsComplete &&
            cleaningTask && cleaningTask.IsComplete &&
            trashTask && trashTask.IsComplete &&
            coffeeTask && coffeeTask.IsComplete;

        if (allDone && !announced)
        {
            announced = true;

            // Gather completed task names (only those that are actually complete)
            var completed = GetCompletedTasks();

            taskCompleteAudio.Play();
            Debug.Log("ALL TASKS COMPLETE ✅");
            // Requires GameRunController.FinishRunAndWriteJson(List<string>)
            runController.FinishRunAndWriteJson(completed);
        }

        // If you allow tasks to be undone mid-run, this re-arms the final announce
        if (!allDone)
        {
            announced = false;
        }
    }

    List<string> GetCompletedTasks()
    {
        var list = new List<string>();
        if (drawerA && drawerA.IsComplete) list.Add("Drawer A");
        if (drawerB && drawerB.IsComplete) list.Add("Drawer B");
        if (cleaningTask && cleaningTask.IsComplete) list.Add("Cleaning");
        if (trashTask && trashTask.IsComplete) list.Add("Trash");
        if (coffeeTask && coffeeTask.IsComplete) list.Add("Coffee");
        return list;
    }
}

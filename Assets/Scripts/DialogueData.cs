using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueData 
{
    public static string QuestName = "Помощь селянину ";
    public static List<string> QuestTask = new List<string>()
    { 
        "Убить 5 монстров ",
        "Поговорить c селянином "
    }
    ;
    public static string WaitingReplic = "Ты уже убил монстров?";
    public static List<string> Replics = new List<string>()
    {
        "Привет, тебе нужно задание?",
        "Убей 5 монстров.",
        "Вы хотите принять задание?"
    }
    ;
    public static List<string> FinishReplics = new List<string>()
    {
        "Спасибо что помог!",
        "Вот твоя награда"
    }
    ;
}

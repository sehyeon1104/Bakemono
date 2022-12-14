using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("QuestUI")]
    public GameObject QuestPanel;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questContent;

    private void Start()
    {
        ToggleQuestUI(false);
    }

    public void ToggleQuestUI(bool toggle)
    {
        QuestPanel.SetActive(toggle);
    }


}

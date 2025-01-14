using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI texMeshPro;
    [SerializeField] private Button button;

    public void SetBaseAction(BaseAction baseAction)
    {
        texMeshPro.text = baseAction.GetActionName();
        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.GetSelectedUnit().GetAction(baseAction).ExecuteAction();
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TestTools;

public class PopUpMessage : MonoBehaviour
{
    [SerializeField] private GameObject _popUpMessage;
    private TextMeshProUGUI _textMeshProGUI;
    private bool _isMessageShowedUp;

    private void Start()
    {
        _textMeshProGUI = _popUpMessage.GetComponent<TextMeshProUGUI>();

        _popUpMessage.SetActive(false);
        _isMessageShowedUp = false;
    }

    public void CharacterArrived(string characterName)
    {
        if (!_isMessageShowedUp)
        {
            _popUpMessage.SetActive(true);
            _textMeshProGUI.text = $"{characterName} arrived first!";
            _isMessageShowedUp = true;
        }
    }
}
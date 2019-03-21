using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] TMP_InputField _addPlayerField, _player1Field, _player2Field;
    [SerializeField] Button _AddPlayerButton, _EnterMatchResults;
    [SerializeField] Slider _player1Slider, _player2Slider;
    [SerializeField] TMP_InputField _player1scoreText, _player2scoreText;

    public string AddPlayerFieldValue() { return _addPlayerField.text; }
    public string Player1FieldValue() { return _player1Field.text; }
    public string Player2FieldValue() { return _player2Field.text; }

    private void Awake()
    {
        _player1Slider.onValueChanged.AddListener(delegate { OnSliderChange(1); });
        _player2Slider.onValueChanged.AddListener(delegate { OnSliderChange(2); });
        _player1scoreText.onValueChanged.AddListener(delegate { OnScoreInputFieldChange(1); });
        _player2scoreText.onValueChanged.AddListener(delegate { OnScoreInputFieldChange(2); });

        _AddPlayerButton.onClick.AddListener(delegate { AddNewPlayer(); });
        _EnterMatchResults.onClick.AddListener(delegate { EnterMatchResults(); });
    }

    private void OnSliderChange(int sliderIndex)
    {
        if (sliderIndex == 1) _player1scoreText.text = _player1Slider.value + "";
        if (sliderIndex == 2) _player2scoreText.text = _player2Slider.value + "";
    }

    private void OnScoreInputFieldChange(int inputFieldIndex)
    {
        if (inputFieldIndex == 1) _player1Slider.value = int.Parse(_player1scoreText.text);
        if (inputFieldIndex == 2) _player2Slider.value = int.Parse(_player2scoreText.text);
    }

    private void AddNewPlayer()
    {
        if (_addPlayerField.text != "")
        {
            ScoreBoardPositioner.Instance.AddNewPlayer(DataBaseAccessor.Instance.AddNewPlayer(_addPlayerField.text));
            ScoreBoardPositioner.Instance.UpdateRating();
        }

    }

    private void EnterMatchResults()
    {
        Player p1 = DataBaseAccessor.Instance.GetPlayerData(_player1Field.text);
        Player p2 = DataBaseAccessor.Instance.GetPlayerData(_player2Field.text);

        Debug.Log(p1.userName + "   " + p2);

        if (p1 != null && p2 != null)
        {
            EloAlgorithm.Instance.UpdateElo(p1, p2, (int)_player1Slider.value, (int)_player2Slider.value);

            ScoreBoardPositioner.Instance.UpdateRating();
        }
    }
}

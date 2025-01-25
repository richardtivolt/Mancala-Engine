using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    [Header("Table UI")]
    [SerializeField] private TMP_Text _currentPlayerText;

    [SerializeField] private TMP_Text[] _player1Fields;
    [SerializeField] private TMP_Text _player1End;
    [SerializeField] private TMP_Text[] _player2Fields;
    [SerializeField] private TMP_Text _player2End;
   
    [SerializeField] private Button[] _player1Buttons;
    [SerializeField] private Button[] _player2Buttons;
    [SerializeField] private Color[] _buttonColors;
    private Button _bestMoveButton;

    [Header("Engine Config")]
    [SerializeField] private int _searchDepth;
    [SerializeField] private Algorithm _algorithm;

    private Table _table;
    private Engine _engine;

    private void Start()
    {
        _table = new Table();
        if (_algorithm == Algorithm.Debug)
        {
            _engine = new EngineDebug();
        }
        else if (_algorithm == Algorithm.Fast)
        {
            _engine = new EngineFast();
        }
        else
        {
            _engine = new EngineUltra();
        }

        UpdateTable();
    }

    public void UpdateTable()
    {
        _currentPlayerText.text = $"Current Player: {_table.CurrentPlayer + 1}";

        for (int i = 0; i < 6; i++)
        {
            _player1Fields[i].text = _table.Data[i].ToString();
            _player2Fields[i].text = _table.Data[i + 7].ToString();
        }
        _player1End.text = _table.Data[6].ToString();
        _player2End.text = _table.Data[13].ToString();

        bool player1Move = _table.CurrentPlayer == 0;
        for (int i = 0; i < 6; i++)
        {
            _player1Buttons[i].interactable = player1Move && _table.Data[i] > 0;
            _player2Buttons[i].interactable = !player1Move && _table.Data[i + 7] > 0;
        }

        if (_table.IsGameOver())
        {
            int winner = _table.GetWinner();
            if (winner == 2)
            {
                _currentPlayerText.text = "Draw";
            }
            else
            {
                _currentPlayerText.text = $"Winner: Player {winner + 1}";
            }
        }
    }

    public void MakeMove(int index)
    {
        if (!_table.Move(index))
        {
            throw new Exception($"Invalid move: {index}");
        }
        if (_bestMoveButton != null)
        {
            _bestMoveButton.image.color = _buttonColors[0];
        }
        UpdateTable();
    }

    public void ShowBestMove()
    {
        SearchData searchData = _engine.CalculateMove(_table, _searchDepth);
        _currentPlayerText.text = $"Current Player: {_table.CurrentPlayer + 1}\n<size=20%>\n<size=50%>{searchData}</size></size>";

        if (searchData.BestPath.Count > 0)
        {
            Move bestMove = searchData.BestPath[0];
            if (_bestMoveButton != null)
            {
                _bestMoveButton.image.color = _buttonColors[0];
            }
            _bestMoveButton = _table.CurrentPlayer == 0 ? _player1Buttons[bestMove.Index] : _player2Buttons[bestMove.Index];
            _bestMoveButton.image.color = _buttonColors[1];
        }
    }
}

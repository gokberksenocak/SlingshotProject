using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCalculation : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Material _green;
    [SerializeField] private Material _red;
    [SerializeField] private Material _purple;
    private int _greenBallCount;
    private int _redBallCount;
    private int _purpleBallCount;
    public void ColorBallCount()
    {
        _greenBallCount = 0;
        _redBallCount = 0;
        _purpleBallCount = 0;
        _uiManager.ColorTexts[0].text = _greenBallCount.ToString();
        _uiManager.ColorTexts[1].text = _redBallCount.ToString();
        _uiManager.ColorTexts[2].text = _purpleBallCount.ToString();

        for (int i = 0; i < _playerManager.CollectedBalls.Count; i++)
        {
            Material ballMaterial= _playerManager.CollectedBalls[i].GetComponent<MeshRenderer>().sharedMaterial;
            if (ballMaterial == _green)
            {
                _greenBallCount++;
                _uiManager.ColorTexts[0].text = _greenBallCount.ToString();
            }
            else if (ballMaterial == _red)
            {
                _redBallCount++;
                _uiManager.ColorTexts[1].text = _redBallCount.ToString();
            }
            else if (ballMaterial == _purple)
            {
                _purpleBallCount++;
                _uiManager.ColorTexts[2].text = _purpleBallCount.ToString();
            }
        }
    }
}

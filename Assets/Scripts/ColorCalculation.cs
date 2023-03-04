using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCalculation : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Material _green;
    [SerializeField] private Material _red;
    [SerializeField] private Material _blue;
    private int _greenBallCount;
    private int _redBallCount;
    private int _blueBallCount;
    public void ColorBallCount()
    {
        _greenBallCount = 0;
        _redBallCount = 0;
        _blueBallCount = 0;
        _uiManager.ColorTexts[0].text = _greenBallCount.ToString();
        _uiManager.ColorTexts[1].text = _redBallCount.ToString();
        _uiManager.ColorTexts[2].text = _blueBallCount.ToString();

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
            else if (ballMaterial == _blue)
            {
                _blueBallCount++;
                _uiManager.ColorTexts[2].text = _blueBallCount.ToString();
            }
        }
    }
}

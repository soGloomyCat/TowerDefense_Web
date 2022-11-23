using UnityEngine;

public class BlockFrameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _horizontalBlockFrames;
    [SerializeField] private GameObject _verticalBlockFrames;

    private bool _isActive = true;

    public void ActivateFrame(int frameIndex)
    {
        for (int i = 0; i < _horizontalBlockFrames.transform.childCount; i++)
        {
            if (frameIndex == i)
            {
                _horizontalBlockFrames.transform.GetChild(i).gameObject.SetActive(true);
                _verticalBlockFrames.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _horizontalBlockFrames.transform.GetChild(i).gameObject.SetActive(false);
                _verticalBlockFrames.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    public void ActivateHorizontalFrames()
    {
        if (_isActive)
        {
            _verticalBlockFrames.SetActive(false);
            _horizontalBlockFrames.SetActive(true);
        }
    }

    public void ActivateVerticalFrames()
    {
        if (_isActive)
        {
            _horizontalBlockFrames.SetActive(false);
            _verticalBlockFrames.SetActive(true);
        }
    }

    public void DeactivateAllFrames()
    {
        _isActive = false;
        _horizontalBlockFrames.SetActive(false);
        _verticalBlockFrames.SetActive(false);
    }
}

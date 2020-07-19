using UnityEngine;
using UnityEngine.UI;

public class OptionsPopupController : MonoBehaviour
{
    [SerializeField] private InputField gridSizeInput;
    [SerializeField] private InputField totalObstaclesInput;
    [SerializeField] private XYInput startPositionInput;
    [SerializeField] private XYInput endPositionInput;

    private GameOptions options;

    // Start is called before the first frame update
    void Start()
    {
        options = GameOptions.Options;
        PopulateFieldsFromOptions();
        RegisterCallbacks();
    }

    private void PopulateFieldsFromOptions()
    {
        gridSizeInput.text = options.GridSize.ToString();
        totalObstaclesInput.text = options.TotalObstacles.ToString();
        startPositionInput.SetValue(options.StartPosition);
        endPositionInput.SetValue(options.EndPosition);
    }

    private void RegisterCallbacks()
    {
        gridSizeInput.onValueChanged.AddListener(OnGridSizeChanged);
        totalObstaclesInput.onValueChanged.AddListener(OnTotalObstaclesChanged);
        startPositionInput.onValueChanged = OnStartPositionChanged;
        endPositionInput.onValueChanged = OnEndPositionChanged;
    }

    public void OnGridSizeChanged(string newSize)
    {
        int parsedSize;
        if(int.TryParse(newSize, out parsedSize))
        {
            options.GridSize = parsedSize;
            options.SaveStateToDisk();
        }
    }

    public void OnTotalObstaclesChanged(string newTotal)
    {
        int parsedTotal;
        if(int.TryParse(newTotal, out parsedTotal))
        {
            options.TotalObstacles = parsedTotal;
            options.SaveStateToDisk();
        }
    }
    public void OnStartPositionChanged(Vector2Int newValue)
    {
        options.StartPosition = newValue;
        options.SaveStateToDisk();
    }

    public void OnEndPositionChanged(Vector2Int newValue)
    {
        options.EndPosition = newValue;
        options.SaveStateToDisk();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class OptionsPopupController : MonoBehaviour
{
    [SerializeField] private OptionSettings options;

    [SerializeField] private InputField gridSizeInput;
    [SerializeField] private InputField totalObstaclesInput;
    [SerializeField] private XYInput startPositionInput;
    [SerializeField] private XYInput endPositionInput;

    // Start is called before the first frame update
    void Start()
    {
        PopulateFieldsFromOptions();
        RegisterCallbacks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateFieldsFromOptions()
    {
        gridSizeInput.text = options.gridSize.ToString();
        totalObstaclesInput.text = options.totalObstacles.ToString();
        startPositionInput.SetValue(options.startPosition);
        endPositionInput.SetValue(options.endPosition);
    }

    private void RegisterCallbacks()
    {
        gridSizeInput.onValueChanged.AddListener(OnGridSizeChanged);
        totalObstaclesInput.onValueChanged.AddListener(OnTotalObstaclesChanged);
        startPositionInput.onValueChanged = OnStartPositionChanged;
        startPositionInput.onValueChanged = OnEndPositionChanged;
    }

    public void OnGridSizeChanged(string newSize)
    {
        int parsedSize;
        if(int.TryParse(newSize, out parsedSize))
        {
            options.gridSize = parsedSize;
        }
    }

    public void OnTotalObstaclesChanged(string newTotal)
    {
        int parsedTotal;
        if(int.TryParse(newTotal, out parsedTotal))
        {
            options.totalObstacles = parsedTotal;
        }
    }
    public void OnStartPositionChanged(Vector2Int newValue)
    {
        options.startPosition = newValue;
    }

    public void OnEndPositionChanged(Vector2Int newValue)
    {
        options.endPosition = newValue;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class OptionsPopupController : MonoBehaviour
{
    [SerializeField] private InputField gridSizeInput;
    [SerializeField] private InputField totalObstaclesInput;
    [SerializeField] private XYInput startPositionInput;
    [SerializeField] private XYInput endPositionInput;
    [SerializeField] private Button buttonConfirm;
    [SerializeField] private Button buttonOptions;
    [SerializeField] private PopupAnimator pupupAnimator;

    private GameOptions options;

    // Start is called before the first frame update
    void Start()
    {
        options = GameOptions.Options;
        PopulateFieldsFromOptions();

        // TODO: on enable/desible should register/unregister callbacks
        RegisterUICallbacks();
    }

    private void PopulateFieldsFromOptions()
    {
        gridSizeInput.text = options.GridSize.ToString();
        totalObstaclesInput.text = options.TotalObstacles.ToString();
        startPositionInput.SetValue(options.StartPosition);
        endPositionInput.SetValue(options.EndPosition);
    }

    private void RegisterUICallbacks()
    {
        gridSizeInput.onValueChanged.AddListener(OnGridSizeChanged);
        totalObstaclesInput.onValueChanged.AddListener(OnTotalObstaclesChanged);
        startPositionInput.onValueChanged = OnStartPositionChanged;
        endPositionInput.onValueChanged = OnEndPositionChanged;
        buttonConfirm.onClick.AddListener(OnConfirmButtonClicked);
        buttonOptions.onClick.AddListener(OnOptionsButtonClicked);
    }

    public void OnGridSizeChanged(string newSize)
    {
        int parsedSize;
        if(int.TryParse(newSize, out parsedSize))
        {
            options.GridSize = parsedSize;
        }
    }

    public void OnTotalObstaclesChanged(string newTotal)
    {
        int parsedTotal;
        if(int.TryParse(newTotal, out parsedTotal))
        {
            options.TotalObstacles = parsedTotal;
        }
    }
    public void OnStartPositionChanged(Vector2Int newValue)
    {
        options.StartPosition = newValue;
    }

    public void OnEndPositionChanged(Vector2Int newValue)
    {
        options.EndPosition = newValue;
    }

    public void OnConfirmButtonClicked()
    {
        options.SaveStateToDisk();
        pupupAnimator.Animate(up: false);
        buttonOptions.gameObject.SetActive(true);
    }

    public void OnOptionsButtonClicked()
    {
        pupupAnimator.Animate(up: true);
        buttonOptions.gameObject.SetActive(false);
    }
}

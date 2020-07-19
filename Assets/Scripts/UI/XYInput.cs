using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class XYInput : MonoBehaviour
{
    [SerializeField] private InputField xInput;
    [SerializeField] private InputField yInput;

    public UnityAction<Vector2Int> onValueChanged;

    void Start()
    {
        RegisterCallbacks();
    }
    
    public void SetValue(Vector2Int newValue)
    {
        xInput.text = newValue.x.ToString();
        yInput.text = newValue.y.ToString();
    }

    private void RegisterCallbacks()
    {
        xInput.onValueChanged.AddListener(OnValuesChanged);
        yInput.onValueChanged.AddListener(OnValuesChanged);
    }

    private void OnValuesChanged(string value)
    {
        if(onValueChanged != null)
        {
            Vector2Int newValue = new Vector2Int();
            int x, y;
            if(int.TryParse(xInput.text, out x))
            {
                newValue.x = x;
            }
            if(int.TryParse(yInput.text, out y))
            {
                newValue.y = y;
            }

            onValueChanged(newValue);
        }
    }
}
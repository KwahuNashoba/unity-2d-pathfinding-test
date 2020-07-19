using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// NOTE: all of this could have been implemented inside OptionSettings with no problems
// this serves as an example of how this can be done via singleton, that way eliminating
// the need to manually assign Options asset to all objects using it
public class GameOptions : MonoBehaviour
{

    [SerializeField] private OptionSettings scriptableOptions;

    public int GridSize
    {
        get { return scriptableOptions.gridSize; }
        set { scriptableOptions.gridSize = value; dirty = true; }
    }

    public int TotalObstacles
    {
        get { return scriptableOptions.totalObstacles; }
        set { scriptableOptions.totalObstacles = value; dirty = true; }
    }

    public Vector2Int StartPosition
    {
        get { return scriptableOptions.startPosition; }
        set { scriptableOptions.startPosition = value; dirty = true; }
    }

    public Vector2Int EndPosition
    {
        get { return scriptableOptions.endPosition; }
        set { scriptableOptions.endPosition = value; dirty = true; }
    }


    private string persistentSettingsPath;
    private static GameOptions options;
    public static GameOptions Options
    {
        get
        {
            if (options != null)
            {
                if (options.dirty)
                {
                    options.LoadFromDisk();
                }
                return options;
            } else
            {
                Debug.LogWarning(
                    $"Options still not loaded! Either no GameOptions game object added" +
                    $" or object still not awaken, try adding GameOptions prefab to the scene" +
                    $" or rearanging objects in the Hierarchy View");
                return null;
            }
        }
    }

    private bool dirty;

    private void Awake()
    {
        if(options == null)
        {
            DontDestroyOnLoad(this);
            options = this;

            persistentSettingsPath = $"{Application.persistentDataPath}/options.td";
            options.LoadFromDisk();
        } else
        {
            Debug.LogWarning($"Multiple instances of {GetType()} detected!" +
                $"\nDestroying object {gameObject.name}");
            Destroy(gameObject);
        }
    }

    public void SaveStateToDisk()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.OpenWrite(persistentSettingsPath);
        binaryFormatter.Serialize(fileStream, JsonUtility.ToJson(scriptableOptions));

        fileStream.Close();
    }

    private void LoadFromDisk()
    {
        if (File.Exists(persistentSettingsPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.OpenRead(persistentSettingsPath);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(fileStream) as string, scriptableOptions);

            fileStream.Close();
        }
    }
}

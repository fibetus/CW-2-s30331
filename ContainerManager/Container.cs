namespace ContainerManager;

public abstract class Container
{
    private static int ID = 1;
    private static HashSet<int> containerIDs = new HashSet<int>();

    public double CurrentWeight { get; set;  }
    public double Height { get; }
    public double ConatinerWeight { get; }
    public double Depth { get; }
    public string SerialNumber { get; }
    public double MaxLoadWeight { get; }

    protected Container(string containgerType, double height, double conatinerWeight, double depth, double maxLoadWeight)
    {
        int id;
        do
        {
            id = ID++;
        } while (containerIDs.Contains(id));
        containerIDs.Add(id);
        
        CurrentWeight = 0.0;
        Height = height;
        ConatinerWeight = conatinerWeight;
        Depth = depth;
        SerialNumber = $"KON-{containgerType}-{id}";
        MaxLoadWeight = maxLoadWeight;
    }

    protected virtual void LoadContainer(double weight)
    {
        if (weight + CurrentWeight > MaxLoadWeight)
        {
            throw new OverfillException($"OverfillExcpetion: Overfilling of container {SerialNumber}");
        }
        CurrentWeight += weight;
    }

    protected virtual void UnloadContainer()
    {
        CurrentWeight = 0.0;
    }

    public override string ToString()
    {
        return $"{SerialNumber}: {CurrentWeight}/{MaxLoadWeight} kg";
    }
    
}
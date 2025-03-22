namespace ContainerManager;

public class ContainerL : Container, IHazardNotifier
{
    public bool IsHazardous { get; }
    
    public ContainerL(bool isHazardous, double height, double conatinerWeight, double depth, double maxLoadWeight) 
        : base("L", height, conatinerWeight, depth, maxLoadWeight)
    {
        IsHazardous = isHazardous;
    }

    protected override void LoadContainer(double weight)
    {
        double limit = IsHazardous ? MaxLoadWeight * 0.5 : MaxLoadWeight * 0.9;

        if (weight + CurrentWeight > limit)
        {
            NotifyHazard(SerialNumber);
        }
        else
        {
            base.LoadContainer(weight);
        }
    }

    public void NotifyHazard(string serialNumber)
    {
        Console.WriteLine($"Warning: Hazardous event happened with container: {serialNumber}");
    }
}
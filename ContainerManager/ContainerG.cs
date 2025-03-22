namespace ContainerManager;

public class ContainerG : Container, IHazardNotifier
{
    public double Pressure { get; }


    public ContainerG(double pressure, double height, double conatinerWeight, double depth, double maxLoadWeight) 
        : base("G", height, conatinerWeight, depth, maxLoadWeight)
    {
        Pressure = pressure;
    }

    public override void UnloadContainer()
    {
        CurrentWeight *= 0.05;
    }

    public void NotifyHazard(string serialNumber)
    {
        Console.WriteLine($"Warning: Hazardous event happened with container: {serialNumber}");
    }
}
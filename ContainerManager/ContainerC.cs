
namespace ContainerManager;

public class ContainerC : Container
{
    public double Temperature { get; }
    public string ProductType { get; }
    
    public ContainerC(string productType, double temperature, double height, double conatinerWeight, double depth, double maxLoadWeight) 
        : base("C", height, conatinerWeight, depth, maxLoadWeight)
    {
        Temperature = temperature;
        ProductType = productType;
    }
    
}
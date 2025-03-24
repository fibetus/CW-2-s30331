
namespace ContainerManager;

public class ContainerC : Container
{
    public double Temperature { get; }
    public string ProductType { get; }
    
    public ContainerC(string productType, double temperature, double height, double conatinerWeight, double depth, double maxLoadWeight) 
        : base("C", height, conatinerWeight, depth, maxLoadWeight)
    {
        ProductType = productType;
        
        double requiredTemperature = CheckRequiredTemperature(productType);
        if (requiredTemperature > temperature)
        {
            throw new Exception($"Temperature for {productType} has to be greater than {requiredTemperature}");
        }
        Temperature = temperature;
    }

    private double CheckRequiredTemperature(string productType)
    {
        return productType switch
        {
            "Bananas" => 13.3,
            "Chocolate" => 18,
            "Fish" => 2,
            "Meat" => -15,
            "Ice cream" => -18,
            "Frozen pizza" => -30,
            "Cheese" => 7.2,
            "Sausages" => 5,
            "Butter" => 20.5,
            "Eggs" => 19,
            _ => throw new Exception($"Unknown product type: {productType}")
        };
    }



    
}
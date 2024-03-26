namespace Microservices.Domain.Entities;
public class Recomendation : Entity
{
    public Recomendation(string clothes, string food)
    {
        Clothes = clothes;
        Food = food;
    }

    public string Clothes {  get; set; }
    public string Food { get; set; }
}

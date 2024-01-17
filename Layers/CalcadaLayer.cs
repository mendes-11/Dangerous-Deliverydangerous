using System.IO;
using System.Linq;

public class calcadasLayer : Layer
{
    public calcadasLayer(float velocidade) : base(velocidade)
    {
        this.Planos.AddRange(
            Directory.GetFiles("./Image/Sidewalk")
            .Select(path => new Calcada(path, 426, 1000, 300))
        );

    }
}
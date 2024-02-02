using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System;

public class OleoLayer : ObjectCollision
{
    public OleoLayer(float velocidade, Game game)
        : base(velocidade, game)
    {
        List<Image> frames = new List<Image>();
        string[] imagePaths = Directory.GetFiles("./Image/Oleo");

        foreach (string imagePath in imagePaths)
        {
            frames.Add(Image.FromFile(imagePath));
        }
        Object obj = new Object(frames, 650);
        Objects.Add(obj);
    }
}

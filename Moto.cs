using System.Drawing;

public class Moto : IPlano
{
    private Image Img;  
    private int X; 
    private int Y; 
    private int Width; 
    private int Height; 

    public Moto(string imagePath, int x, int y, int width, int height)
    {
        this.Img = Image.FromFile(imagePath);
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }

    float IPlano.Width { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Draw(Graphics g, DrawPlanoParameters parameters)
    {
        g.DrawImage(Img, X, Y, Width, Height);
    }
}
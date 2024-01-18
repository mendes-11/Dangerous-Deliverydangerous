using System.Drawing;

public class Rua : IPlano
{
    private Image Img;

    private float Y;
    private float Width;
    private float Height;

    public Rua(string imagePath, float y, float width, float height)
    {
        this.Img = Image.FromFile(imagePath)
            .GetThumbnailImage((int)width, (int)height, null, nint.Zero);
        this.Y = y;
        this.Width = (int)width;
        this.Height = (int)height;
    }

    float IPlano.Width { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Draw(Graphics g, DrawPlanoParameters parameters)
    {
        throw new System.NotImplementedException();
    }
}

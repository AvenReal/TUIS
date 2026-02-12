using System.Diagnostics;

namespace Terminal.Components.Masks;
using System.Drawing;
public class ImageMask : Mask
{
    private Bitmap _image;
    private int _imageHeight;
    private int _imageWidth;

    public ImageMask(string path)
    {
        _image = new Bitmap(path); // Windows Only...
        _imageHeight = _image.Height;
        _imageWidth = _image.Width;
    }
    
    
    
    protected override void Behaviour()
    {
        int height = _imageHeight / Component!.Height;
        int width = _imageWidth / Component.Width;
        
        for (byte i = 0; i < Component!.Height; i++)
        {
            for (byte j = 0; j < Component.Width; j++)
            {
                DrawChar(i, j, GetChar(i * height, j * width,  height, width)  );
            }
        }
    }

    private char? GetChar(int yoffset, int xoffset, int height, int width)
    {
        float sum = 0;
        float nb_pixels = 0;
        float alpha = 0;
        
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Color c = _image.GetPixel(xoffset + j, yoffset + i);
                sum += c.R + c.G + c.B;
                alpha += c.A;
                nb_pixels ++;
            }
        }

        if (alpha <= 0.5)
        {
            return null;
        }
        float gray = sum / (3 * nb_pixels) / 255.0f;

        switch (gray)
        {
            case >= 0.8f:
                return '█';
            case >= 0.6f:
                return '▓';
            case >= 0.4f:
                return '▒';
            case >= 0.2f:
                return '░';
            default:
                return ' ';
        }
        
    }
    
    
    
}
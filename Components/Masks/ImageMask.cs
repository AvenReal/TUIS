using System.Drawing;

namespace TUIS.Components.Masks;

public class ImageMask : Mask
{
    private int _imageHeight;
    private int _imageWidth;

    private Color[,] _image;


    public bool IsColored
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }


    /// <summary>
    /// Draw an image in knida ASCII art, only windows compatible :,).
    /// </summary>
    public ImageMask(Component component, string path, bool isColored = false, bool isVisible = true,
        TextColor color = TextColor.White, BackgroundColor background = BackgroundColor.None,
        TextDecoration decoration = TextDecoration.Default) : base(component, isVisible, color, background, decoration)
    {
        var image = new Bitmap(path); // Windows Only...
        _imageHeight = image.Height;
        _imageWidth = image.Width;
        IsColored = isColored;
        _image = new Color[_imageHeight, _imageWidth];
        for (int i = 0; i < _imageHeight; i++)
        {
            for (int j = 0; j < _imageWidth; j++)
            {
                _image[i, j] = image.GetPixel(j, i);
            }
        }

        image.Dispose();
    }


    protected override void Behaviour()
    {
        int height = _imageHeight / Component.Height;
        int width = _imageWidth / Component.Width;

        for (int i = 0; i < Component.Height; i++)
        {
            for (int j = 0; j < Component.Width; j++)
            {
                (float r, float g, float b, float a) = GetRGBA(i * height, j * width, height, width);
                char? c = GetChar(r, g, b, a);
                TextColor? textColor = IsColored ? GetColor(r, g, b) : null;
                DrawChar(i, j, c, textColor);
            }
        }
    }

    private (float r, float g, float b, float a) GetRGBA(int yoffset, int xoffset, int height, int width)
    {
        float r = 0;
        float g = 0;
        float b = 0;
        float a = 0;
        float nbPixels = 0;


        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Color c = _image[yoffset + i, xoffset + j];
                r += c.R;
                g += c.G;
                b += c.B;
                a += c.A;
                nbPixels++;
            }
        }

        return (r / nbPixels / 255.0f, g / nbPixels / 255.0f, b / nbPixels / 255.0f, a / nbPixels / 255.0f);
    }


    private char? GetChar(float r, float g, float b, float a)
    {
        if (a <= 0.5)
        {
            return null;
        }

        switch ((r + g + b) / 3.0f)
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

    private TextColor GetColor(float r, float g, float b)
    {
        float gray = 0.6f; //(r + g + b) / 3.0f;

        if (r < gray)
        {
            if (g < gray)
            {
                return b < gray ? TextColor.Black : TextColor.Blue;
            }

            return b < gray ? TextColor.Green : TextColor.Cyan;
        }

        if (g < gray)
        {
            return b < gray ? TextColor.Red : TextColor.Purple;
        }

        return b < gray ? TextColor.Yellow : TextColor.White;
    }
}
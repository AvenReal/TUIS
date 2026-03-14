using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TUIS.Components.Masks;

/// <summary>
/// This Mask will draw an image in format jpg, png or any other image format in ASCII art in black and white or using 8 different colors.
/// </summary>
public class ImageMask : Mask
{
    /// <summary>
    /// Holds the height of the original image.
    /// </summary>
    private int _imageHeight;

    /// <summary>
    /// Holds the width of the original image.
    /// </summary>
    private int _imageWidth;

    /// <summary>
    /// Holds all the <see cref="Rgba32"/> of each pixel of the original image.
    /// </summary>
    private Rgba32[,] _image;

    /// <summary>
    /// Holds whether the image should be drawn in color or in black and white.
    /// </summary>
    public bool IsColored
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }


    /// <summary>
    /// Draw an image in format jpg, png or any other image format in ASCII art.
    /// Warning: the original image should have a ratio than the <see cref="Component"/>. 
    /// </summary>
    /// <param name="path">The relative or absolute path of the image to draw is ASCII art</param>
    /// <param name="isColored">Whether the Image will be drawn using the 8 possible colors or in black and white (default = false).</param>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
    public ImageMask(Component component, string path, bool isColored = false, bool isVisible = true,
        Terminal.TextColor color = Terminal.TextColor.White,
        Terminal.BackgroundColor background = Terminal.BackgroundColor.None,
        Terminal.TextDecoration decoration = Terminal.TextDecoration.Default) : base(component, isVisible, color,
        background, decoration)
    {
        using Image<Rgba32> image = Image.Load<Rgba32>(path);
        _imageHeight = image.Height;
        _imageWidth = image.Width;
        IsColored = isColored;
        _image = new Rgba32[_imageHeight, _imageWidth];
        for (int i = 0; i < _imageHeight; i++)
        {
            for (int j = 0; j < _imageWidth; j++)
            {
                _image[i, j] = image[j, i];
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// This <see cref="Mask"/>'s <see cref="Behaviour"/> is to draw the <see cref="_image"/> in ascii art.
    /// </summary>
    protected override void Behaviour()
    {
        int height = _imageHeight / Component.Height;
        int width = _imageWidth / Component.Width;

        for (int i = 0; i < Component.Height; i++)
        {
            for (int j = 0; j < Component.Width; j++)
            {
                (float r, float g, float b, float a) = GetRgba(i * height, j * width, height, width);
                char? c = GetChar(r, g, b, a);
                Terminal.TextColor textColor = IsColored ? GetColor(r, g, b) : Terminal.TextColor.White;
                DrawChar(i, j, c, textColor);
            }
        }
    }

    /// <summary>
    /// Each character of the final ascii art image will correspond to a region of pixels on the original image.
    /// This method will get the average RGBA values for a region of pixels. 
    /// </summary>
    /// <param name="y">The y coordinate on the original image of the top left corner of the region to calculate RGBA values.</param>
    /// <param name="x">The x coordinate on the original image of the top left corner of the region to calculate RGBA values.</param>
    /// <param name="height">The height of the region to calculate RGBA values.</param>
    /// <param name="width">The width of the region to calculate RGBA values.</param>
    /// <returns>Returns the RGBA representing the average color of the region.</returns>
    private (float r, float g, float b, float a) GetRgba(int y, int x, int height, int width)
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
                Rgba32 c = _image[y + i, x + j];
                r += c.R;
                g += c.G;
                b += c.B;
                a += c.A;
                nbPixels++;
            }
        }

        return (r / nbPixels / 255.0f, g / nbPixels / 255.0f, b / nbPixels / 255.0f, a / nbPixels / 255.0f);
    }

    /// <summary>
    /// Returns a char based on how dark the average RGBA is.
    /// </summary>
    /// <param name="r">The red part of the color of the pixel.</param>
    /// <param name="g">The green part of the color of the pixel.</param>
    /// <param name="b">The blue part of the color of the pixel.</param>
    /// <param name="a">The alpha part of the color of the pixel.</param>
    /// <returns>A char corresponding to the darkness of the average RGBA.</returns>
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

    private Terminal.TextColor GetColor(float r, float g, float b)
    {
        float gray = 0.6f;

        if (r < gray)
        {
            if (g < gray)
            {
                return b < gray ? Terminal.TextColor.Black : Terminal.TextColor.Blue;
            }

            return b < gray ? Terminal.TextColor.Green : Terminal.TextColor.Cyan;
        }

        if (g < gray)
        {
            return b < gray ? Terminal.TextColor.Red : Terminal.TextColor.Purple;
        }

        return b < gray ? Terminal.TextColor.Yellow : Terminal.TextColor.White;
    }

    public void Export(string path = "./exported_image.png", int? exportedWidth = null, int? exportedHeight = null)
    {
        var goalHeight = (exportedHeight ?? Component.Height);
        int height = _imageHeight / goalHeight;
        var goalWidth = (exportedWidth ?? Component.Width);
        int width = _imageWidth / goalWidth;

        using StreamWriter writer = new StreamWriter(path);
        for (int i = 0; i < goalHeight; i++)
        {
            for (int j = 0; j < goalWidth; j++)
            {
                (float r, float g, float b, float a) = GetRgba(i * height, j * width, height, width);
                writer.Write(GetChar(r, g, b, a) ?? ' ');
            }

            writer.Write('\n');
        }
    }
}
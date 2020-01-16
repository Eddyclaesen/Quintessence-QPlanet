using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace Quintessence.QPlanet.Infrastructure.Web.ActionResults
{
    public class ImageResult : ActionResult
    {
        private Dictionary<ImageFormat, string> _formatMap;
        private readonly Image _image;
        private readonly ImageFormat _format;

        public ImageResult(Image image, ImageFormat format)
        {
            CreateContentTypeMap();
            _image = image;
            _format = format;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var outputMemoryStream = new MemoryStream();
            _image.Save(outputMemoryStream, _format);

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = _formatMap[_format];
            outputMemoryStream.Position = 0;
            outputMemoryStream.WriteTo(context.HttpContext.Response.OutputStream);
        }

        private void CreateContentTypeMap()
        {
            _formatMap = new Dictionary<ImageFormat, string>{
                { ImageFormat.Bmp,  "image/bmp"                },
                { ImageFormat.Gif,  "image/gif"                },
                { ImageFormat.Icon, "image/vnd.microsoft.icon" },
                { ImageFormat.Jpeg, "image/Jpeg"               },
                { ImageFormat.Png,  "image/png"                },
                { ImageFormat.Tiff, "image/tiff"               },
                { ImageFormat.Wmf,  "image/wmf"                }
            };
        }
    }
}

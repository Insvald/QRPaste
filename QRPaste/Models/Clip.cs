using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;

namespace QRPaste.Models
{
    public enum ClipType { None, URL, Text, Image, File}

    public class QRPException : Exception
    {
        public QRPException(string e) : base(e) { }
    }

    //a piece of data which needs to be shared
    public class Clip
    {
        private string textData;

        public Clip()
        {
            //simple initialization;
            Type = ClipType.None;
            Created = DateTime.Now;
            QRBitmap = null;
        }

        public Guid Id { get; set; }

        //can be null in case of anonymous upload
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public DateTime Created { get; private set; }

        public DateTime Expiring { get; set; }
        
        //the type of data
        public ClipType Type { get; private set; }

        [Url]
        public string URL
        {
            get
            {
                if (Type != ClipType.URL)
                    throw new QRPException("Invalid data");
                return textData;
            }
            set
            {
                textData = value;
                Type = ClipType.URL;
                CreateBarcode(value);
            }
        }

        public string Text
        {
            get
            {
                if (Type != ClipType.Text)
                    throw new QRPException("Invalid data");
                return textData;
            }
            set
            {
                textData = value;
                Type = ClipType.Text;
                //TODO: store text and create url for it 
                //maybe in form BaseUrl + ID
                CreateBarcode("future url");
            }
        }


        /*
         * to show:
         *  var img = new TagBuilder("img");
            img.MergeAttribute("alt", "your alt tag");
            img.Attributes.Add("src", String.Format("data:image/gif;base64,{0}", 
                Convert.ToBase64String(stream.ToArray())));
             return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
         */
        [NotMapped]
        public Bitmap QRBitmap { get; private set; }
        
        public byte[] QRData 
        { 
            get
            {
                byte[] byteArray;
                using (MemoryStream stream = new MemoryStream())
                {
                    QRBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
                    stream.Close();

                    byteArray = stream.ToArray();
                }
                return byteArray;
            }
        }

        private void CreateBarcode(string url)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 250,
                    Width = 250,
                    Margin = 0
                }
            };
            QRBitmap = barcodeWriter.Write(url);
        }
    }
}
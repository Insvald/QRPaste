using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QRPaste.ViewModels
{
    public class ClipViewModel
    {
        [DataType(DataType.Duration)]        
        public DateTime Expiring { get; set; }
        
        [DataType(DataType.Url)]
        public string URL { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [DataType(DataType.Upload)]
        public byte[] File { get; set; }

        //public byte[] Image { get; set; }
    }
}
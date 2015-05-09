using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PocRazorFromDatabase.Models
{
    public class CustomVirtualFile : VirtualFile
    {
        private readonly byte[] _viewContent;

        public CustomVirtualFile(string virtualPath, byte[] viewContent)
            : base(virtualPath)
        {
            _viewContent = viewContent;
        }

        public override Stream Open()
        {
            return new MemoryStream(_viewContent);
        }
    }
}
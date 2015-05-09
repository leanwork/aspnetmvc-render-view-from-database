using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace PocRazorFromDatabase.Models
{
    public class CustomVirtualPathProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            var view = GetFromDatabase(virtualPath);

            if (view == null)
            {
                return base.FileExists(virtualPath);
            }

            return true;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            var view = GetFromDatabase(virtualPath);

            if (view == null)
            {
                return base.GetFile(virtualPath);
            }
            else
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(view.Content);
                return new CustomVirtualFile(virtualPath, content);
            }
        }

        private DatabaseView GetFromDatabase(string virtualPath)
        {
            if (!virtualPath.Contains("Views") ||
                virtualPath.Contains("_ViewStart") ||
                virtualPath.Contains(".Mobile.cshtml") || 
                !virtualPath.Contains(".cshtml"))
            {
                return null;
            }

            virtualPath = virtualPath.Replace("~", "");

            var cache = MemoryCache.Default;
            var view = cache.Get(virtualPath) as DatabaseView;
            if (view != null)
            {
                return view;
            }

            var db = new MongoRepository<DatabaseView>();
            view = db.FirstOrDefault(x => x.Path == virtualPath);
            if (view == null)
            {
                return null;
            }

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            cache.Remove(virtualPath);
            cache.Add(virtualPath, view, policy);

            return view;
        }
    }
}
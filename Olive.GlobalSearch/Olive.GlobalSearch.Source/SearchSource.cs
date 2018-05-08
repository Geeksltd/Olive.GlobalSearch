using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Olive.GlobalSearch.Common;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Olive.GlobalSearch
{
    public class SearchSource
    {
        public virtual SearchResult Process(IUser user, string[] keywords) => new SearchResult { Url = "Some other url", Title = "Some other title", Description = "Some other description", IconUrl = "Some other url" };
    }
}
